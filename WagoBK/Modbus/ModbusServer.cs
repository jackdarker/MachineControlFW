using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace Modbus
{
    public class ModbusServerData
    {
        public ModbusServerData()
        {
        }

        public void SetAllInputs(byte[] Values)
        {
            m_RWL.AcquireWriterLock(Timeout.Infinite);
            try
            {
                int Length = Math.Min(Values.Length, m_InData.Length);
                for (int i = 0; i < Length; i++)
                {
                    m_InData[i] = Values[i]; 
                }
            }
            finally
            {
                m_RWL.ReleaseWriterLock();
            }
        }
        public void SetInputByte(int Adress, byte Value)
        {
            m_RWL.AcquireWriterLock(Timeout.Infinite);
            try
            {
                if (Adress < m_InData.Length)
                {
                    m_InData[Adress]=Value;
                }
            }
            finally
            {
                m_RWL.ReleaseWriterLock();
            }
        }       
        public byte GetInputByte(int Adress)
        {
            byte Value = 0;
            m_RWL.AcquireReaderLock(Timeout.Infinite);
            try
            {
                if (Adress < m_InData.Length)
                {
                    Value = m_InData[Adress];
                }
            }
            finally
            {
                m_RWL.ReleaseReaderLock();
            }
            return Value;
        }
        public void SetAllOutputs(byte[] Values)
        {
            m_RWL.AcquireWriterLock(Timeout.Infinite);
            try
            {
                int Length = Math.Min(Values.Length, m_OutData.Length);
                for (int i = 0; i < Length; i++)
                {
                    m_OutData[i] = Values[i];
                }
            }
            finally
            {
                m_RWL.ReleaseWriterLock();
            }
        }
        public void SetOutputByte(int Adress, byte Value)
        {
            m_RWL.AcquireWriterLock(Timeout.Infinite);
            try
            {
                if (Adress < m_OutData.Length)
                {
                    m_OutData[Adress] = Value;
                }
            }
            finally
            {
                m_RWL.ReleaseWriterLock();
            }
        }
        public byte GetOutputByte(int Adress)
        {
            byte Value = 0;
            m_RWL.AcquireReaderLock(Timeout.Infinite);
            try
            {
                if (Adress < m_OutData.Length)
                {
                    Value = m_OutData[Adress];
                }
            }
            finally
            {
                m_RWL.ReleaseReaderLock();
            }
            return Value;
        }
        protected ReaderWriterLock m_RWL = new ReaderWriterLock();
        private byte[] m_InData = new byte[256];    //Stores value server -> client 
        private byte[] m_OutData = new byte[256];   //stores value client -> server

    }
    /// <summary>
    /// implementation of ModbusServer
    /// client requests or sends data to server
    /// server stores data into lookup table
    /// </summary>
    public class ModbusServer
    {
        public class SyncEvents
        {
            public SyncEvents()
            {

                _newItemEvent = new AutoResetEvent(false);
                _exitThreadEvent = new ManualResetEvent(false);
                _eventArray = new WaitHandle[2];
                _eventArray[0] = _newItemEvent;
                _eventArray[1] = _exitThreadEvent;
            }

            public EventWaitHandle ExitThreadEvent
            {
                get { return _exitThreadEvent; }
            }
            public EventWaitHandle NewItemEvent
            {
                get { return _newItemEvent; }
            }
            public WaitHandle[] EventArray
            {
                get { return _eventArray; }
            }

            private EventWaitHandle _newItemEvent;
            private EventWaitHandle _exitThreadEvent;
            private WaitHandle[] _eventArray;
        }
        public enum State
        {
            Unknown = 0,
            Initialize,
            Connect,
            Idle,
            GetRequest,
            AnswerRequest
        }
        ///stores output data
        public class MemOut
        {
            public MemOut()
            {            }

            public void SetMem(int Offset,byte[] Data)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_Mem.InsertByte(Offset, Data, Data.GetLength(0));
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public byte[] GetMem(int Offset, int Length)
            {
                byte[] Value=null;  
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    Value = m_Mem.GetAsBytes(Offset, Length); 
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return Value;
            }
            protected ReaderWriterLock m_RWL = new ReaderWriterLock();
            private ModbusDataStruct m_Mem = new ModbusDataStruct();
        }
        public ModbusServer(string IP, int Port)
        {
            m_ModbusServerData = new ModbusServerData();
            m_SyncEvents = new SyncEvents();
            m_Server = new ModbusServerThread(m_SyncEvents,IP,Port,m_ModbusServerData);
            m_ServerThread = new Thread(m_Server.ThreadRun);
        }
    #region interface to application
        public delegate void OnRequestReceivedHandler(object sender, RequestReceivedData e);
        public class RequestReceivedData : EventArgs
        {
            public RequestReceivedData()
                : base()
            { }
            public RequestReceivedData(string IP, int ReceivedFC)
                : base()
            {
                FC = ReceivedFC;
                Host = IP;
            }
            public int FC = 0;
            public string Host = "";
        }
        public void ConnectToEvent(OnRequestReceivedHandler Handler)
        {
            m_Server.EventRequestReceived += Handler;
        }
        public void Start()
        {
            m_ServerThread.Start();
        }
        public void Stop(bool Abort)
        {
            m_SyncEvents.ExitThreadEvent.Set();
            m_ServerThread.Join();
        }

    #endregion
    /// <summary>
    /// Network IO is done in this thread
    /// </summary>
     private class ModbusServerThread
     {
        public ModbusServerThread(SyncEvents e, string IP, int Port, ModbusServerData IO)
        {
            m_SyncEvents = e;
            m_IP = IP;
            m_Port = Port;
            m_LastState = State.Unknown;
            m_NextState = m_LastState;
            m_ModbusServerData = IO;
        }
        private void Init()
        {
            Disconnect();
        }
     #region data formating
         protected bool CheckModbusHeader(byte[] Message, int MessageLength)
         {
            int Length = ((int)(Message[4]) << 8) + (int)(Message[5]);

            bool IsOK = false;
            if (MessageLength < 9)
            {
                //??EventMBusException(this, new ModBusException(ModBusException.ModBusExceptionCodeEnum.ResponseFormatError));
            }
            else if (Length + 6 != MessageLength)
            {
                //??EventMBusException(this, new ModBusException(ModBusException.ModBusExceptionCodeEnum.ResponseFormatError));
            }
            else if ((Message[7] & 0x80) > 0)
            {//skip non FC
                ModBusException.ModBusExceptionCodeEnum Code = (ModBusException.ModBusExceptionCodeEnum)Message[8];
                //??EventMBusException(this, new ModBusException(Code));
            }
            else
            {
                m_FC = Message[7];
                m_TransID = Message[0] << 8 + Message[1];
                IsOK = true;
            }

            return IsOK;
        }
        protected byte[] BuildModbusHeader(int TransID, byte FC, byte[] Data)
        {
            int TotalLength = Data.GetLength(0) + 8;
            byte[] Out = new byte[TotalLength];
            Out[0] = (byte)((TransID & 0xFF00) >> 8);
            Out[1] = (byte)(TransID & 0xFF);
            Out[2] = 0;//Protocol-ID
            Out[3] = 0;//Protocol-ID
            Out[4] = (byte)(((TotalLength - 6) & 0xFF00) >> 8);
            Out[5] = (byte)((TotalLength - 6) & 0xFF);
            Out[6] = 1;//unitID
            Out[7] = FC;
            Data.CopyTo(Out, 8);
            return Out;
        }
        protected byte[] BuildModbusException(Int16 TransID, byte FC, byte Code)
        {
            byte[] Out = new byte[9];
            Out[0] = (byte)((TransID & 0xFF00) >> 8);
            Out[1] = (byte)(TransID & 0xFF);
            Out[2] = 0;//Protocol-ID
            Out[3] = 0;//Protocol-ID
            Out[4] = (byte)(0);
            Out[5] = (byte)(0x3);
            Out[6] = 1;//unitID
            Out[7] = (byte)(FC & 0x80);
            Out[7] = Code;
            return Out;
        }
        protected byte[] RemoveModbusHeader(byte[] Message)
        {
            byte[] Data = new byte[Message.GetLength(0) - 8];
            for (int i = 8; i < Message.GetLength(0); i++)
            {
                Data[i - 8] = Message[i];
            }
            return Data;
        }
     #endregion
     #region network IO
        protected void Connect(string IP, int Port)
        {
            if (IP != "")
            {//
                m_Client.Connect(IPAddress.Parse(IP), Port);
            }
            else
            {
                m_Client = new UdpClient(Port);  
            }//cannot test connection?
        }
        public void Disconnect()
        {
            if (m_Client != null)
            {
                if (m_Client.Client != null)
                {
                    m_Client.Client.Shutdown(SocketShutdown.Both);
                    m_Client.Client.Disconnect(true);
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
                    IsConnected = true;// m_Client.Client.Connected; //will only be false if previous send was erronous
                }
            }
            return IsConnected;
        }
        protected void SendBytes(byte[] Message)
        {
            try
            {
                 if (IsConnected())
                {
                    if (Message.Length != m_Client.Send(Message, Message.Length,m_RemoteIpEndPoint))
                    {
                        throw new SocketException();
                    };
                };
            }
            catch (SocketException exception)
            {//connection was interupted
                Disconnect();
            }
            catch (IOException exception)
            {//connection was interupted
                Disconnect();
            }
        }
        protected int GetRequest(ModbusDataStruct Response)
        {
            int RecLength = 0;
            m_Input.Initialize();
            try
            {
                if (IsConnected())
                {
                    m_Input = m_Client.Receive(ref m_RemoteIpEndPoint); //would block on disconnect!
                    RecLength = m_Input.Length;

                    /*IAsyncResult ar = m_Client.BeginReceive(null, null);
                    System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                    //because its not possible to set TO, we work async and terminate after some seconds
                    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(5000), false))
                    {
                        m_Input = m_Client.EndReceive(ar, ref m_RemoteIpEndPoint);
                        //m_Client.Close();
                        //throw new TimeoutException();
                        ;
                    }
                    else
                    {
                        m_Input = m_Client.EndReceive(ar, ref m_RemoteIpEndPoint);
                        RecLength = m_Input.Length;
                    }*/

                    Response.SetAsBytes(m_Input, RecLength);
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
            return RecLength;
        }
      #endregion


        private bool CopyOutToBuffer(int Offset, int Length)
        {
            return false;
        }
        private bool CopyInToBuffer(int Offset, int Length)
        {//copy data into transmit buffer
            return false;
        }
        
        public event OnRequestReceivedHandler EventRequestReceived;
        protected void OnRequestReceived(string IP, int ReceivedFC)
        {
            OnRequestReceivedHandler handler = EventRequestReceived;
            RequestReceivedData e = new RequestReceivedData(IP, ReceivedFC);
            if (handler != null) handler(this, e);
        }
        private void OnMBusException(ModBusException.ModBusExceptionCodeEnum Code)
        {
        }
        private void AnswerRequest()
        {
            byte FC = m_FC;
            int TransID = m_TransID;
            int Register= (m_Input[8]<<8 + m_Input[9]) * 2;
            //threading problem durch Delegate-Ausführung auf selben Thread wie ModbusCore??
            OnRequestReceived(m_RemoteIpEndPoint.Address.ToString(), FC);
            m_Out.SetAsBytes(new byte[] { 0 }, 0);
            byte[] Message;
            switch (FC)
            {
                case 3:
                    Message = BuildModbusHeader(TransID,3,new byte[] { 4, 10, 20, 30, 40 });
                    m_Out.SetAsBytes(Message,Message.Length);
                    /*if (CopyInToBuffer(Register, m_Input[10] << 8 + m_Input[11]))
                    {
                        BuildModbusHeader(TransID,3,m_Input);
                    };*/

                    break;
                case 6:

                    break;
                case 16:

                    break;
                default:
                    break;
            }
            SendBytes(m_Out.GetAsBytes());
        }
        public void ThreadRun()
        {
            while (!m_SyncEvents.ExitThreadEvent.WaitOne(0, false))
            {
                m_LastState = m_NextState;
                Thread.Sleep(10);
                if (m_NextState == State.Unknown)
                {
                    m_NextState = State.Initialize;
                }
                else if (m_NextState == State.Initialize)
                {//initialize internal data
                    Init();
                    Connect("",m_Port);
                    m_NextState = State.Connect;
                }
                else if (m_NextState == State.Connect)
                {//try until conection established
                    
                    m_NextState = State.GetRequest;
                }
                else if (m_NextState == State.GetRequest)
                {//get app data 
                    int length = GetRequest(m_In);
                    m_NextState = IsConnected() ? State.GetRequest : State.Connect;
                    if (length > 0)
                    {
                        if (CheckModbusHeader(m_Input, length))
                        {
                            m_NextState = State.AnswerRequest;
                        };
                    };
                }
                else if (m_NextState == State.AnswerRequest)
                {//
                    AnswerRequest();
                    m_NextState = IsConnected() ? State.GetRequest : State.Connect;
                }
                
            }
            //terminate if client stopped
        }

        private State m_LastState;
        private State m_NextState;
        private SyncEvents m_SyncEvents;
        private ModbusServerData m_ModbusServerData;
        private byte[] m_Input = new byte[256];  //temporary saves received Data
        private byte[] m_Output = new byte[256];  //
        private ModbusDataStruct m_In = new ModbusDataStruct(); //last received data 
        private ModbusDataStruct m_Out = new ModbusDataStruct(); //data to send
        private string m_IP;
        private int m_Port;
        private byte m_FC;      //pending FC
        private int m_TransID;  //pending TransID
        private UdpClient m_Client = null;  //Holds Connection
        IPEndPoint m_RemoteIpEndPoint = null;
      }

        private ModbusServerThread m_Server;
        private Thread m_ServerThread;
        private SyncEvents m_SyncEvents;
        private ModbusServerData m_ModbusServerData;
    }

}
