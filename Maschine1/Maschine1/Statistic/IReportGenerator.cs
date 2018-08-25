using System;
using System.Collections.Generic;
using System.Text;

namespace Maschine1.Statistic {
    /// <summary>
    /// Interface für Report-Generator
    /// </summary>
    public interface IReportGenerator {
       void AddTotalResult(WTFileBase Info);
    }
}
