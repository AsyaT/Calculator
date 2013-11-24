using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Calculator.Code;
using Calculator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AlgoTests
    {

        [TestMethod]
        public void InversMatrixMethodTestEasy()
        {
            var tmp = new double[][] {new double[]{4,5}, new double[]{7,23}};
            var model = new CalculatorModel {CoefficientMatrix = tmp, FreeMembers = new List<double>() {5, 23}};

            var expected = new ResultModel {XList = new double[] {0, 1}};
            var actual = Algoritms.InversMatrixMethod(model);

            Assert.AreEqual(expected.XList[0], actual.XList[0]); 
            Assert.AreEqual(expected.XList[1], actual.XList[1]); 
        }

        [TestMethod]
        public void InversMatrixMethodTestComplicated()
        {
            var tmp = new double[][] { new double[] { 45.3, 5.52 }, new double[] { 7.62, 23.6 } };
            var model = new CalculatorModel { CoefficientMatrix = tmp, FreeMembers = new List<double>() { 5.256, 2.3 } };

            var expected = new ResultModel { XList = new double[] { 0.1084164, 0.062452 } };
            var actual = Algoritms.InversMatrixMethod(model);

            Assert.AreEqual(expected.XList[0], actual.XList[0]);
            Assert.AreEqual(expected.XList[1], actual.XList[1]);
        }

        [TestMethod]
        public void InversMatrixMethodTestZeroDeterminant()
        {
            var tmp = new double[][] { new double[] { 3, 1 }, new double[] { 9, 3 } };
            var model = new CalculatorModel { CoefficientMatrix = tmp, FreeMembers = new List<double>() { 5.256, 2.3 } };

            ResultModel expected = null;
            var actual = Algoritms.InversMatrixMethod(model);

            Assert.AreEqual(expected, actual);
        }
    }
}
