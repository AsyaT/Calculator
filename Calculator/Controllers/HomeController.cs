using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
            var model = new CalculatorModel()
                            {
                                VariableNumber = new List<int>()
                            };
            for (int i = Constants.MinVariableCount; i <= Constants.MaxVariableCount; i++)
            {
                model.VariableNumber.Add(i);
            }
            
            return View(model);
        }

        public ActionResult Calculate(CalculatorModel incomeModel)
        {
            var result = Algoritms.InversMatrixMethod(incomeModel);

            if ( result == null )
            {
                return Content("<p>The system don't have resolution, because determinant is equal 0.</p>");
            }

            return PartialView(result);
        }
    }
}
