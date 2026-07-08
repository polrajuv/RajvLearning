using Microsoft.AspNetCore.Mvc;

namespace RajvLearning.API.Controllers
{
    public class LearningTopicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
