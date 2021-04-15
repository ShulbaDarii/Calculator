using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcClass.Tests
{
    [TestClass()]
    public class CalcTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(3, calc.Add(1,2));
        }
        [TestMethod()]
        public void SubTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(-1, calc.Sub(1, 2));
        }
        [TestMethod()]
        public void MultTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(4, calc.Mult(2, 2));
        }
        [TestMethod()]
        public void DivTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(1, calc.Div(2, 2));
        }
        [TestMethod()]
        public void ModTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(0, calc.Mod(4, 2));
        }
        [TestMethod()]
        public void ABSTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(5, calc.ABS(5));
        }
        [TestMethod()]
        public void IABSTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(-5, calc.IABS(5));
        }
        [ExpectedException(typeof(Exception), "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від - 2147483648 до 2147483647.")]
        [TestMethod()]
        public void AddExcepTest()
        {
            Calc calc = new Calc();
            calc.Add(2147483648, 1);
        }
        [ExpectedException(typeof(Exception), "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від - 2147483648 до 2147483647.")]
        [TestMethod()]
        public void SubExcepTest()
        {
            Calc calc = new Calc();
            calc.Sub(-2147483648, 1);
        }
        [ExpectedException(typeof(Exception), "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від - 2147483648 до 2147483647.")]
        [TestMethod()]
        public void MultExcepTest()
        {
            Calc calc = new Calc();
            calc.Mult(214748, 83648);
        }
        [ExpectedException(typeof(Exception), "Error 09 – Помилка ділення на 0.")]
        [TestMethod()]
        public void DivExcepTest()
        {
            Calc calc = new Calc();
            Assert.AreEqual(1, calc.Div(2, 0));
        }
    }
}