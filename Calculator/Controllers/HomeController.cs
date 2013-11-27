using System.Web.Mvc;
using Calculator.Code;
using Calculator.Models;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Calculate(FrontendModel incomeModel)
        {
            if (!incomeModel.EquationSystem.Contains("="))
            {
                return Content("<p>This is not system of equations.</p>");
            }
            incomeModel.EquationSystem = incomeModel.EquationSystem.Replace("\r\n", " ");

            CalculatorModel parsedModel = SystemEquationsParser.ParserEquations(incomeModel);

            if(parsedModel==null)
            {
                return Content("<p>System is incorrect.</p>");
            }

            var result = Algoritms.InversMatrixMethod(parsedModel);

            if ( result == null )
            {
                return Content("<p>The system don't have resolution, because determinant is equal 0.</p>");
            }

            return PartialView(result);
        }
    }
}
