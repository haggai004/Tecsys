using System.Web.Mvc;
using Tecsys.Retail.MvcWeb.Models;

namespace Tecsys.Retail.MvcWeb.Controllers
{
    public class RetailController : Controller
    {
        public RetailController()
        {
            LayoutModel = new LayoutModel();
            LayoutModel.CartItemsCount = 0;
            this.ViewData["LayoutModel"] = LayoutModel;
            this.ViewBag.LayoutModel = LayoutModel;
        }

        public  LayoutModel LayoutModel { get; set; }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            //Log the error!!

            //Redirect or return a view, but not both.
            //filterContext.Result = RedirectToAction("Index", "ErrorHandler");
            // OR 
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
        }
    }
}