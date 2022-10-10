
using System;
using Auth.Core.Contract;
using Auth.Core.Dao;

namespace Auth.Core.Responses.Client
{

    public class GetLogResponse : ResponseBaseObject
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string UserAgent { get; set; }
        public DateTime LogDate { get; set; }
        public string LogType { get; set; }
        public string LogMessage { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string QueryStringData { get; set; }
        public string FormData { get; set; }
        public Guid ChainId { get; set; }
        public string ExtraInfo { get; set; }
    }
    
}