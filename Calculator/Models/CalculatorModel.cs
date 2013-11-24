using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Models
{
    public class CalculatorModel
    {
        public List<int> VariableNumber { get; set; }

        public double[][] CoefficientMatrix { get; set; }

        public List<double> FreeMembers { get; set; }
    }
}