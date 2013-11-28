using System;
using System.Collections.Generic;
using Calculator.Models;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Calculator.Code
{
    public static class Algoritms
    {
        public static ResultModel InversMatrixMethod(CalculatorModel input)
        {
            Vector<double> resultVector;
            var result = new ResultModel(){ResultList = new Dictionary<string, double>()};
            var vector = new DenseVector(input.FreeMembers);
             
            var matrix = new DenseMatrix(input.CoefficientMatrix);

            if (matrix.Determinant() == 0.0)
            {
                return null;
            }
            else
            {
                var inverseMatrix = matrix.Inverse();

                resultVector = inverseMatrix.Multiply(vector);
            }

            foreach(var value in input.ValColumnRelation)
            {
                result.ResultList.Add(value.Key, Math.Round(resultVector[value.Value],Constants.RoundValue));
            }

            return result;
        }
    }
}