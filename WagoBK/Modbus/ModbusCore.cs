﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Threading;

namespace Modbus
{
    public class ModBusException : EventArgs
        {
            public enum ModBusExceptionCodeEnum
            {
                IllegalFunction = 1,
                IllegalDataAdress = 2,
                IllegalDataValue =3,
                SlaveDeviceFailure = 4,
                Acknowledge = 5,
                ServerBusy =6,
                MemoryParityError =8,
                GatewayPathUnavailable = 10,
                FailedToRespond = 11,

                // added some own, non ModBuscodes
                NoNetworkConnection = 512,
                ResponseTimeout = 513,
                NetworkError = 514,
                ResponseFormatError = 515
            }

            public ModBusException(ModBusExceptionCodeEnum Code)
            {
                ModBusExceptionCode = Code;
            }

            public ModBusExceptionCodeEnum ModBusExceptionCode;

        }
    public class ModbusDataStruct
        {
            public ModbusDataStruct()
            { }
            public void SetData(UInt16 W0, UInt16 W1)
            {
                m_Length = 4;
                SplitWord(W0, ref m_Data[0], ref m_Data[1]);
                SplitWord(W1, ref m_Data[2], ref m_Data[3]);
            }
            public UInt16[] GetAsWords()
            {
                UInt16[] Words = new UInt16[GetWordCount()];
                for(int i=0;i<Words.Length;i++)
                {
                    ToWord(m_Data[i*2],m_Data[i*2+1],ref Words[i]);
                }
                return Words;
            }
            public byte[] GetAsBytes()
            {
                byte[] Value = new byte[m_Length];
                for (int i = 0; i < m_Length; i++)
                {
                    Value[i] = m_Data[i];
                }
                return Value;
            }
            public byte[] GetAsBytes(int offset, int length)
            {
                byte[] temp = new byte[Math.Min(length,m_Length-offset)];
                for(int i = 0; i<temp.Length; i++)
                {
                    temp[i] = m_Data[i+offset];
                }
                return temp;
            }
            /*public void SetAsBytes(byte[] Bytes)
            {
                m_Data = null;
                m_Data = new byte[Bytes.Length];
                Bytes.CopyTo(m_Data, 0);
            }*/
            public void SetAsBytes(byte[] Bytes, int Length)
            {
                m_Length = Length;
                for (int i = 0; i < m_Length; i++)
                {
                    m_Data[i] = Bytes[i];
                }
            }
            public void RemoveBytes(int Offset, int Length)
            {
                byte[] temp = new byte[m_Data.Length];
                int newLength = m_Length- Length;
                int Pointer = 0;
                for (int i = 0; i < m_Data.Length; i++)
                {
                    if (i < Offset || i > (Offset+Length-1))
                    {
                        temp[Pointer] = m_Data[i];
                        Pointer++;
                    }
                }
                m_Length = newLength;
                m_Data = temp;
            }
            public void InsertByte(int Offset, byte[] Value, int Length)
            {
                byte[] temp = new byte[m_Data.Length];
                int newLength = m_Length + Length;
                int PointerOld = 0;
                int PointerNew = 0;
                for (int i = 0; i < m_Data.Length; i++)
                {
                    if (i < Offset || i > (Offset + Length - 1))
                    {//copy old Data
                        temp[i] = m_Data[PointerOld];
                        PointerOld++;
                    }
                    else
                    {
                        temp[i] = Value[PointerNew];
                        PointerNew++;
                    }
                }
                m_Length = newLength;
                m_Data = temp;
            }
            public int GetByteCount()
            {
                return m_Length;
            }
            public int GetWordCount()
            {     
                return (GetByteCount()/2);
            }
            private static void SplitWord(UInt16 In, ref byte HiB, ref byte LoB)
            {
                HiB = (byte)(In >> 8);
                LoB = (byte)(In & 0xFF);
            }
            private static void ToWord(byte HiB, byte LoB, ref UInt16 Out)
            {
                Out = ((UInt16)(((int)HiB) << 8)) ;
                Out = (UInt16)(Out + LoB);
            }
            private byte[] m_Data = new byte[256];
            private int m_Length=0;
        }
    public class ModbusCore
    {
        public ModbusCore()
        {
            RequestTO = 1000;
        }
            private string m_IP;
        public string IP
        {
            get { return m_IP; }
            //set { m_IP=value ; }
        }
            private int m_Port;
        public int Port
        {
            get { return m_Port; }
            //set { m_Port = value; }
        }
            private int m_ConnTO;
        public int ConnectionTO
        {
            get { return m_ConnTO; }
            set { m_ConnTO = value; }
        }
            private int m_ReqTO;
        public int RequestTO
        {
            get { return m_ReqTO; }
            set { m_ReqTO = value; }
        }

        private void Init()
        {
            Disconnect();
            ConnectionTO = 5000;
            RequestTO = 2000;
        }
        public delegate void OnRequestReceivedHandler(object sender, RequestReceivedData e);
        public class RequestReceivedData : EventArgs
        {
            public RequestReceivedData()
                : base()
            { }
            public RequestReceivedData(string IP, Modbus.ModbusDataStruct Received)
                : base()
            {
                Data = Received;
                Host = IP;
            }
            public Modbus.ModbusDataStruct Data = null;
            public string Host = "";
        }
        public void ConnectToEvent(OnRequestReceivedHandler Handler)
        {
            this.EventRequestReceived += Handler;
        }
        public event OnRequestReceivedHandler EventRequestReceived;
        protected void OnRequestReceived(string IP, Modbus.ModbusDataStruct Received)
        {
            OnRequestReceivedHandler handler = EventRequestReceived;
            RequestReceivedData e = new RequestReceivedData(IP, Received);
            if (handler != null) handler(this, e);
        }
        public delegate void OnRequestSendHandler(object sender, RequestReceivedData e);
        public void ConnectToEvent(OnRequestSendHandler Handler)
        {
            this.EventRequestSend += Handler;
        }
        public event OnRequestSendHandler EventRequestSend;
        protected void OnRequestSend(string IP, Modbus.ModbusDataStruct Received)
        {
            OnRequestSendHandler handler = EventRequestSend;
            RequestReceivedData e = new RequestReceivedData(IP, Received);
            if (handler != null) handler(this, e);
        }
        public void Connect(string RemoteIP, int RemotePort)
        {
            m_IP = RemoteIP;
            m_Port = RemotePort;

            m_Client = new UdpClient(); //because TCP reacts slowly on disconnect (takes up to 1min), using UDP
            m_RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            m_Client.ExclusiveAddressUse = true;
            try
            {
                m_Client.Connect(IPAddress.Parse(IP), Port);
                if (!ExecFC3(0, 0, ref m_In))
                { //test connection by sending data
                    throw new TimeoutException();
                };
            }
                /*m_Client.SendTimeout = 500;    
            m_Client.ReceiveTimeout = RequestTO;
                m_Client.LingerState.LingerTime = 10;
                m_Client.LingerState.Enabled = true;
                m_Client.SendBufferSize = m_Input.Length * 2; //limit buffersize to get faster response on disconnect??
                //m_Client.BeginConnect(IPAddress.Parse(m_BKSetup.GetIP()),m_BKSetup.GetPort(),
                //     ConnectCallback,m_Client);
               // m_Client.Connect(IPAddress.Parse(IP), Port);
            IAsyncResult ar = m_Client.BeginConnect(IPAddress.Parse(IP), Port, null, null);
            System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
            try
            {//because its not possible to set Connect-TO, we work async
                //and terminate after some seconds
                if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    m_Client.Close();
                    throw new TimeoutException();
                }
                m_Client.EndConnect(ar);
                m_Stream = m_Client.GetStream();
                m_Stream.ReadTimeout = m_Client.ReceiveTimeout;
                m_Stream.WriteTimeout = m_Client.SendTimeout;
            }
            */
            catch (TimeoutException exception)
            {
                OnMBusException(ModBusException.ModBusExceptionCodeEnum.NoNetworkConnection);
            }
            catch (SocketException exception)
            {
                if (exception.ErrorCode == 10061 | exception.ErrorCode == 10060)
                {//10061 "No connection could be made because the target machine actively refused it" 
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.NoNetworkConnection);
                }
                else
                {
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.NetworkError);
                    Console.WriteLine(exception.ToString() + " " + exception.ErrorCode.ToString()); //??
                }
            }
            finally
            {
                //    wh.Close();
            } 
        }
        public void Disconnect()
        {
            if (m_Client != null)
            {
                if (m_Client.Client != null)
                {
                    m_Client.Client.Shutdown(SocketShutdown.Both);
                    //m_Client.Client.Disconnect(true); no connection because UDP
                    m_Client.Close();
                };
            };
            m_Client = null;
        }
        public bool IsConnected()
        {
            bool IsConnected = false;
            if (m_Client != null)
            {
                if (m_Client.Client != null)
                {
                    IsConnected = m_Client.Client.Connected; //will only be false if previous send was erronous
                    if (!IsConnected)
                        IsConnected = IsConnected;
                }
            }
            return IsConnected;
        }
        public event MBusException EventMBusException;
        public delegate void MBusException(object sender, ModBusException e);
        protected virtual void OnMBusException(ModBusException.ModBusExceptionCodeEnum Code)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            MBusException handler = EventMBusException;
            ModBusException e = new ModBusException(Code);
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }
        public bool ExecFC(byte FC, ModbusDataStruct DataToSend, ref ModbusDataStruct DataReceived)
        {
            bool ResponseOK = false;
            Modbus.ModbusDataStruct Response = new ModbusDataStruct();
            byte[] Message = BuildModbusHeader(0, FC, DataToSend.GetAsBytes());
            SendBytes(Message);
            OnRequestSend(IP, DataToSend);

            int ResponseLength = GetResponse(Response);
            ResponseOK = CheckModbusHeader(0, FC, Response.GetAsBytes(), ResponseLength);
            if (ResponseOK & DataReceived!= null)
            {
                OnRequestReceived(m_RemoteIpEndPoint.Address.ToString(), Response);
                RemoveModbusHeader(Response);
                switch (FC)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 23:
                        DataReceived.SetAsBytes(Response.GetAsBytes(1,
                            Response.GetByteCount() - 1), Response.GetByteCount() - 1);//cut off  byte length and return only values
                        break;
                    default:
                        DataReceived.SetAsBytes(Response.GetAsBytes(),Response.GetByteCount());
                        break;
                }
            };
            return ResponseOK;
        }
        /// <summary>
        /// FC3 read multiple registers
        /// </summary>
        /// <param name="Register"></param>
        /// <param name="Words"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool ExecFC3( UInt16 Register, UInt16 Words, ref ModbusDataStruct Data)
        {
            bool IsOK = false;
            ModbusDataStruct Request = new ModbusDataStruct();
            Request.SetData(Register, Words);
            IsOK = ExecFC(3, Request, ref Data);
            return IsOK;
        }
        /// <summary>
        /// FC6 Write single register
        /// </summary>
        /// <param name="Register"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool ExecFC6(UInt16 Register, UInt16 Data)
        {
            bool IsOK = false;
            byte[] Response = new byte[0];
            ModbusDataStruct Request = new ModbusDataStruct();
            ModbusDataStruct Answer = new ModbusDataStruct();
            Request.SetData(Register, Data);
            IsOK = ExecFC(6, Request, ref Answer);
            return IsOK;
        }
        /// <summary>
        /// FC16 Write multiple register
        /// FC16-Lengthfields will be added
        /// </summary>
        /// <param name="Register">= StartRegisterOffset</param>
        /// <param name="Data">2*n byte RegisterData</param>
        /// <returns></returns>
        public bool ExecFC16(UInt16 Register, ModbusDataStruct Data)
        {
            bool IsOK = false;
            Modbus.ModbusDataStruct Request = new ModbusDataStruct();
            Request.SetAsBytes(Data.GetAsBytes(),Data.GetByteCount());
            byte[] FCLengthFields = new byte[5];
            FCLengthFields[0] = (byte)((Register >> 8) & 0xFF);
            FCLengthFields[1] = (byte)((Register >> 8));
            FCLengthFields[2] = (byte)((Request.GetWordCount()>>8) & 0xFF);
            FCLengthFields[3] = (byte)((Request.GetWordCount()) & 0xFF);
            FCLengthFields[4] = (byte)Request.GetByteCount();  
            //insert Byte and Word count between RegisterAddr and Data
            Request.InsertByte(0, FCLengthFields, FCLengthFields.Length);
            ModbusDataStruct Answer = new ModbusDataStruct();
            IsOK = ExecFC(16, Request, ref Answer);
            return IsOK;
        }
        protected void SendBytes(byte[] Message)
        {
            try
            {
                if (IsConnected())
                {
                    if (Message.Length != m_Client.Send(Message, Message.Length))
                    {
                        throw new SocketException();
                    };
                    //m_Stream.Write(Message, 0, Message.Length);
                };
            }
            catch (SocketException exception)
            {
                //connection was interupted
                Disconnect();
            }
            catch (IOException exception)
            {
                //connection was interupted
                Disconnect();
            }
        }
        protected int GetResponse(ModbusDataStruct Response)
        {
            int RecLength = 0;
            int Timer = 0;
            bool RecSomeData = false;
            bool NoMoreData = false;
            m_Input.Initialize();
            try
            {
                if (IsConnected())
                {
                    //m_Input = m_Client.Receive(ref m_RemoteIpEndPoint); //would block on disconnect!
                    IAsyncResult ar = m_Client.BeginReceive(null, null);
                    System.Threading.WaitHandle wh = ar.AsyncWaitHandle;

                    //because its not possible to set TO, we work async and terminate after some seconds
                    /*while (!ar.IsCompleted)
                    {
                        Thread.Sleep(10);
                        Timer += 10;
                        if (Timer > RequestTO)
                        {
                            m_Client.Close();
                            throw new TimeoutException();
                        }
                    }*/
                    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(RequestTO), false))
                    {
                        m_Client.Close();
                        throw new TimeoutException();
                    }
                    m_Input = m_Client.EndReceive(ar,ref m_RemoteIpEndPoint);
                    RecLength = m_Input.Length;
                    Response.SetAsBytes(m_Input, RecLength);
                    /*while (Timer < RequestTO && !NoMoreData)
                    {
                        System.Threading.Thread.Sleep(10);
                        Timer = Timer + (10);
                        if (m_Stream.DataAvailable)
                        {
                            RecLength = RecLength + m_Stream.Read(m_Input, RecLength, m_Input.Length);
                            RecSomeData = true;
                        }
                        else
                        {
                            NoMoreData = RecSomeData;   //quit Timeout if some Data was received and no more is following
                        }
                    };*/
                };
            }
            catch (TimeoutException exception)
            {
                OnMBusException(ModBusException.ModBusExceptionCodeEnum.NoNetworkConnection);
            }
            catch (SocketException exception)
            {
                if (exception.ErrorCode == 10061 | exception.ErrorCode == 10060)
                {//10061 "No connection could be made because the target machine actively refused it" 
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.NoNetworkConnection);
                }
                else
                {
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.NetworkError);
                    Console.WriteLine(exception.ToString() + " " + exception.ErrorCode.ToString()); //??
                }
            }
            finally
            {
                //    wh.Close();
            }
            
            return RecLength;
        }
        protected bool CheckModbusHeader(Int16 TransID, byte FC, byte[] Message, int MessageLength)
        {
            byte TransId0 = (byte)((TransID & 0xFF00) >> 8);
            byte TransId1 = (byte)(TransID & 0xFF);
            

            bool IsOK = false;
            if (MessageLength < 9)
            {
                OnMBusException(ModBusException.ModBusExceptionCodeEnum.ResponseFormatError);
            }
            else
            {
                int Length = ((int)(Message[4]) << 8) + (int)(Message[5]);
                if (Message[0] != TransId0 || Message[1] != TransId1)
                {
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.ResponseFormatError);
                }
                else if (Message[7] != FC)
                {
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.ResponseFormatError);
                }
                else if (Length + 6 != MessageLength)
                {
                    OnMBusException(ModBusException.ModBusExceptionCodeEnum.ResponseFormatError);
                }
                else if ((Message[7] & 0x80) > 0)
                {
                    ModBusException.ModBusExceptionCodeEnum Code = (ModBusException.ModBusExceptionCodeEnum)Message[8];
                    OnMBusException(Code);
                }
                else
                {
                    IsOK = true;
                }
            }
            return IsOK;
        }
        protected byte[] BuildModbusHeader(Int16 TransID, byte FC, byte[] Data)
        {
            int TotalLength = Data.GetLength(0) + 8;
            byte[] Out = new byte[TotalLength];
            Out[0] = (byte)((TransID & 0xFF00)>>8);
            Out[1] = (byte)(TransID & 0xFF);
            Out[2] = 0;//Protocol-ID
            Out[3] = 0;//Protocol-ID
            Out[4] = (byte)(((TotalLength-6) & 0xFF00) >> 8);
            Out[5] = (byte)((TotalLength - 6) & 0xFF);
            Out[6] = 1;//unitID
            Out[7] = FC;
            Data.CopyTo(Out, 8);
            return Out;
        }
        protected void RemoveModbusHeader(ModbusDataStruct Message)
        {
            int Length = Message.GetByteCount()-8;
            Message.RemoveBytes(0, 8);
        }
        private byte[] m_Input = new byte[256];  //temporary saves received Data
        private ModbusDataStruct m_In = new ModbusDataStruct();
        private UdpClient m_Client = null;  //Holds Connection to BK
        IPEndPoint m_RemoteIpEndPoint = null;
    }
}
