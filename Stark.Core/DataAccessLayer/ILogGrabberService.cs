using Stark.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stark.DataAccessLayer
{
    public interface ILogGrabberService
    {
        Task<List<LogModel>> GetEventLogDataAsync(DateTime start, DateTime end, string messagequeryvalue, LogTypeEnum logType);
        Task<List<LogModel>> GetRemoteEventLogDataAsync(DateTime start, DateTime end, string serverFQDN, string messagequeryvalue, string username, string password, LogTypeEnum logType);
    }
}