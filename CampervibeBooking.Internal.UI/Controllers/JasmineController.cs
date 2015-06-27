using System;
using System.Web.Mvc;

namespace Campervibe.Internal.UI.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
