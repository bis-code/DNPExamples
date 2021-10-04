using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Data
{
    public class LogsJSONData : ILogsData
    {
        private IList<Log> logs;
        private string logsFile = "logs.json";

        private static LogsJSONData instance;
        private static readonly object lockObj = new object();
        

        public LogsJSONData()
        {
            if (!File.Exists(logsFile))
            {
                WriteLogsToFile();
            }
            else
            {
                string content = File.ReadAllText(logsFile);
                logs = JsonSerializer.Deserialize<IList<Log>>(content);
            }
        }

        private void WriteLogsToFile()
        {
            string logAsJson = JsonSerializer.Serialize(logs);
            File.WriteAllText(logsFile, logAsJson);
        }

        public void AddLog(Log log)
        {
            int max = logs.Max(log => log.Id);
            log.Id = (++max);
            logs.Add(log);
            WriteLogsToFile();
        }

        public IList<Log> GetLogs()
        {
            List<Log> tmp = new List<Log>(logs);
            return tmp;
        }
        
        public static LogsJSONData Instance()
        {
            if (instance == null)
                {
                    lock (lockObj)
                    {
                        instance = new LogsJSONData();
                    }
                }
            return instance;
        }
    }
}