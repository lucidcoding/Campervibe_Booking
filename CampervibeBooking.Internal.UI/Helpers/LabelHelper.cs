using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campervibe.Internal.UI.Helpers
{
    public class LabelHelper : IDisposable
    {
        protected HtmlHelper _helper;
        private string _labelText;

        public LabelHelper(HtmlHelper helper, string labelText)
        {
            _helper = helper;
            _labelText = labelText;
        }

        public void Dispose()
        {
            _helper.ViewContext.Writer.Write("<span style=\"padding-left:5px\"></span>");
            _helper.ViewContext.Writer.Write(_labelText);
            _helper.ViewContext.Writer.Write("</label>");
        }
    }
}