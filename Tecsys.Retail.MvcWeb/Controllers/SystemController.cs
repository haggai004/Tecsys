using System.Web.Mvc;
using Tecsys.Retail.MvcWeb.Models;

namespace Tecsys.Retail.MvcWeb.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        [ChildActionOnly]
        public ActionResult LayoutHeader(LayoutModel layoutModel)
        {
            return PartialView(layoutModel);
        }             
    }
}
