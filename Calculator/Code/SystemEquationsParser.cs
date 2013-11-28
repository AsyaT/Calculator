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
            if (!Regex.Match(inputStr, @"(((([+-])?(\d+\.?\d*)?)([a-z]+))+=(\d+\.?\d*))+").Success)
            {
                return null;
            }

            int equationCount = 0;
            var freeMembers = new List<double>();
            var matrixTree = new List<ParsedStructure>();

            string pattern = @"(((([+-])?(\d+\.?\d*)?)([a-z]+))+=(\d+\.?\d*))+";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(inputStr);

            while (m.Success)
            {
                Group freeMember = m.Groups[7];
                CaptureCollection ccfm = freeMember.Captures;

                freeMembers.Add( Convert.ToDouble( ccfm[0].Value));

                Group coef = m.Groups[3];
                CaptureCollection cc_coef = coef.Captures;
                
                Group varname = m.Groups[6];
                CaptureCollection cc_varname = varname.Captures;
                
                for (int i = 0; i < cc_varname.Count; i++)
                {
                    ParsedStructure leaf = new ParsedStructure()
                                               {
                                                   EqualNumber = equationCount,
                                                   Coef = cc_coef[i].Value=="-" ? -1 :( cc_coef[i].Value=="+" || cc_coef[i].Value=="" ? 1 : Convert.ToDouble( cc_coef[i].Value )),
                                                   VarName = cc_varname[i].Value
                                               };
                    matrixTree.Add(leaf);
                }
                
                m = m.NextMatch();
                equationCount++;
            }

            CalculatorModel result = new CalculatorModel()
                                         {
                                             CoefficientMatrix = new double[equationCount, equationCount],
                                             FreeMembers = freeMembers,
                                             ValColumnRelation = new Dictionary<string, int>()
                                         };

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