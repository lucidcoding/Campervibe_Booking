using System;

namespace Campervibe.WebJob.Logging
{
    public interface ILog
    {
        string Add(int level, string message, Exception ex, object objects);
        string Add(int level, string message);
        string Add(int level, string message, Exception ex);
        string Add(int level, string message, object objects);
        string Add(Exception ex);
        string Add(string message);
        string Add(string message, object objects);
        string Add(object objects);
        string Add();
    }
}
