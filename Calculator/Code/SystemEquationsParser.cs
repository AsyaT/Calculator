using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Calculator.Models;

namespace Calculator.Code
{  
    public static class SystemEquationsParser
    {
        public static CalculatorModel ParserEquations(string inputStr)
        {
            if (!Regex.Match(inputStr, @"((([+-])?([\d+(\.)?\d*]*)([a-z]+))+=(\d+(\.)?\d*))+").Success)
            {
                return null;
            }

            int equationCount = inputStr.Split(' ').Count();

            CalculatorModel result = new CalculatorModel()
                                         {
                                             CoefficientMatrix = new double[equationCount, equationCount],
                                             FreeMembers = new List<double>(),
                                             ValColumnRelation = new Dictionary<string, int>()
                                         };
            var matrixTree = new List<ParsedStructure>();

            int i = 0;
            foreach (var equal in inputStr.Split(' '))
            {
                string pattern = @"(.*)=(\d+(\.)?\d*)";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                Match m = r.Match(equal);

                if (m.Success)
                {
                    Group free = m.Groups[2];
                    CaptureCollection cc = free.Captures;
                    result.FreeMembers.Add(Convert.ToDouble(cc[0].Value));
                }
                
                string patternSlag = @"([+-])?([\d+(\.)?\d*]*)([a-z]+)";
                Regex reg = new Regex(patternSlag, RegexOptions.IgnoreCase);
                Match mat = reg.Match(equal);
                while (mat.Success)
                {
                    ParsedStructure leaf = new ParsedStructure();

                    Group sign = mat.Groups[1];
                    CaptureCollection cc_sign = sign.Captures;

                    Group coef = mat.Groups[2];
                    CaptureCollection cc_coef = coef.Captures;
                    double dCoef = cc_coef[0].Value == "" ? 1.0 : Convert.ToDouble(cc_coef[0].Value);

                    Group cvarnameoef = mat.Groups[3];
                    CaptureCollection cc_cvarnameoef = cvarnameoef.Captures;

                    leaf = new ParsedStructure()
                               {
                                   EqualNumber = i,
                                   Coef = (cc_sign.Count > 0 && cc_sign[0].Value == "-") ? (-1) * dCoef : dCoef,
                                   VarName = cc_cvarnameoef[0].Value
                               };

                    matrixTree.Add(leaf);
                    mat = mat.NextMatch();
                }
                i++;
            }

            int column = 0;
            foreach (var item in matrixTree)
            {
                if (!result.ValColumnRelation.ContainsKey(item.VarName))
                {
                    result.ValColumnRelation.Add(item.VarName, column);
                    column++;
                    if (column > equationCount)
                    {
                        return null;
                    }
                }
                result.CoefficientMatrix[item.EqualNumber, result.ValColumnRelation[item.VarName]] = item.Coef;
            }

            return result;
        }
    }
}