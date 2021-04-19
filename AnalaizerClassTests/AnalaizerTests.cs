using AnalaizerClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace AnalaizerClass.Tests
{
    [TestClass()]
    public class AnalaizerTests
    {
        [TestMethod()]
        public void Format_EqualString_EqualReturned()
        {
            // arrange 
            Analaizer.expression = "6+(5-2)*3+2";
            string expected = "6+(5-2)*3+2";
            // action
            var actual = Analaizer.Format();
            // assert
            Assert.AreEqual(expected, actual, $"{Analaizer.expression} should have been {expected}!");
        }
        [TestMethod()]
        public void Format_SpaceAndEqual_EqualReturned()
        {
            // arrange 
            Analaizer.expression = "6 + ( 5 - 2 ) *    3+ 2     =";
            string expected = "6+(5-2)*3+2";
            // action
            var actual = Analaizer.Format();
            // assert
            Assert.AreEqual(expected, actual, $"{Analaizer.expression} should have been {expected}!");
        }
        [TestMethod()]
        public void Format_Equal_EqualReturned()
        {
            // arrange 
            Analaizer.expression = "6+(5-2)*3+2=";
            string expected = "6+(5-2)*3+2";
            // action
            var actual = Analaizer.Format();
            // assert
            Assert.AreEqual(expected, actual, $"{Analaizer.expression} should have been {expected}!");
        }

        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_m_Exception()
        {
            // arrange 
            Analaizer.expression = "m6+(5-2)*3+2";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_plusAndMinus_Exception()
        {
            // arrange 
            Analaizer.expression = "6+-(5-2)*3+2";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_6omd_Exception()
        {
            // arrange 
            Analaizer.expression = "6omd";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_6odm_Exception()
        {
            // arrange 
            Analaizer.expression = "6odm";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_doubleMult_Exception()
        {
            // arrange 
            Analaizer.expression = "6+(5-2)**3+2";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_4brackets_Exception()
        {
            // arrange 
            Analaizer.expression = "(6+((5-2)*3)+2)*(12-8)";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_closeBracketsMoreThanOpen_Exception()
        {
            // arrange 
            Analaizer.expression = "(6+(5-2)*3)+2)*(12-8)";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_incompleteBracketsExpression_Exception()
        {
            // arrange 
            Analaizer.expression = "(6+((5-2)*3)+2";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_incompleteSymbolExpression_Exception()
        {
            // arrange 
            Analaizer.expression = "6+((5-2)*3)+2/";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_otherSymbol_Exception()
        {
            // arrange 
            Analaizer.expression = "6+<(5-2)*3)+2/";
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_65536symbol_Exception()
        {
            // arrange 
            string tmp = new string('1', 65536);
            Analaizer.expression = tmp;
            // action
            var actual = Analaizer.CreateStack();
        }
        [ExpectedException(typeof(Exception), "Exception was not thrown")]
        [TestMethod()]
        public void CreateStack_31NumbersAndOperators_Exception()
        {
            // arrange 
            Analaizer.expression = "1+3-5*7+9/11+13+15+1+3-5*7+9/11+13+15";
            // action
            var actual = Analaizer.CreateStack();
        }

        [TestMethod()]
        public void CreateStackCollectionEqual_EqualReturned()
        {
            // arrange 
            Analaizer.expression = "6+(5-2)*3+2";
            ArrayList expected = new ArrayList() { "6", "5", "2", "-", "3", "*", "+", "2", "+" };
            // action
            var actual = Analaizer.CreateStack();
            // assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RunEstimate_sum5and2_retyrned17()
        {
            // arrange 
            Analaizer.expression = "6+(5-2)*3+2";
            string expected = "17";
            // action
            Analaizer.CreateStack();
            var actual = Analaizer.RunEstimate(Analaizer.CreateStack());
            // assert
            Assert.AreEqual(expected, actual, $"{Analaizer.expression} should have been {expected}!");
        }
    }
}