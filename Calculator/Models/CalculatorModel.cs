using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Models
{
    public class CalculatorModel
    {
        public double[,] CoefficientMatrix { get; set; }

        public List<double> FreeMembers { get; set; }

        public IDictionary<string, int> ValColumnRelation { get; set; } 
    }
}