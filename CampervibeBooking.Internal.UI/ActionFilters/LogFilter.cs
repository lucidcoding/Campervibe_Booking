using Campervibe.Domain.InfrastructureContracts;
using Campervibe.Internal.UI.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campervibe.Internal.UI.ActionFilters
{
    public class LogFilter : IActionFilter
    {
        private ILog _log { get; set; }

        public LogFilter(ILog log)
        {
            _log = log;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var url = filterContext.HttpContext != null
                      && filterContext.HttpContext.Request != null
                      && filterContext.HttpContext.Request.Url != null
                          ? filterContext.HttpContext.Request.Url.AbsolutePath
                          : "";

            string[] data = null;

            if (filterContext.HttpContext != null
                && filterContext.HttpContext.Request != null
                && filterContext.HttpContext.Request.Form != null
                && filterContext.HttpContext.Request.Form.Count > 0)
            {
                data = HttpContext.Current.Server.UrlDecode(filterContext.HttpContext.Request.Form.ToString()).Split('&');
            }
            else if (filterContext.ActionParameters != null
                && filterContext.ActionParameters.Any())
            {
                data = filterContext.ActionParameters.Keys.Select(x => x + "=" + filterContext.ActionParameters[x]).ToArray();
            }

            _log.Add("URL: " + url, data);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                _log.Add(filterContext.Exception);
            }
        }
    }
}