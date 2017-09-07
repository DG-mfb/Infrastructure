using System.Web.Mvc;

namespace AssetManagement.Controllers
{
    [Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}
	}
}