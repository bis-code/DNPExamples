using System.Collections.Generic;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Data
{
    public interface ILogsData
    {
        void AddLog(Log log);
        IList<Log> GetLogs();
    }
}