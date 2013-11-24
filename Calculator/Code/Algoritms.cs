using System;
using System.Linq;
using Calculator.Models;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Calculator.Code
{
    public static class Algoritms
    {
        public static ResultModel InversMatrixMethod(CalculatorModel input)
        {
            var size = input.FreeMembers.Count;
            Vector<double> resultVector;
            var result = new ResultModel();
            var vector = new DenseVector(input.FreeMembers);

            var tmp = new double[size,size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tmp[i, j] = input.CoefficientMatrix[i][j];
                }
            }
             
            var matrix = new DenseMatrix(tmp);

            if (matrix.Determinant() == 0.0)
            {
                return null;
            }
            else
            {
                var inverseMatrix = matrix.Inverse();

                resultVector = inverseMatrix.Multiply(vector);
            }

            result.XList = resultVector.ToArray();

            for(int i=0;i<size;i++)
            {
                result.XList[i] = Math.Round(result.XList[i], Constants.RoundValue);
            }

            return result;
        }
    }
}