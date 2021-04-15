using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcClass
{
    public class Calc
    {
        //Використовується глобальна змінна:
        /// <summary>
        /// Останнє повідомлення про помилку.
        /// Поле і властивість для нього
        /// </summary>
        private static string _lastError = "";
        public static string lastError;
        public double Add(double a, double b)
        {
            if (-2147483648 > a + b || 2147483648 < a + b)
            {
                _lastError = "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від -2147483648 до 2147483647.";
                lastError = _lastError;
                throw new Exception(lastError);
            }
            return a + b;
        }
             /// <summary>
             /// функція віднімання чисел а і b
             /// </summary>
             /// <param name="a">зменшуване</param>
             /// <param name="b">від’ємне</param>
             /// <returns>різниця</returns>
        public double Sub(double a, double b)
        {
            if (-2147483648 > a - b|| 2147483648<a-b)
            {
                _lastError = "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від -2147483648 до 2147483647.";
                lastError = _lastError;
                throw new Exception(lastError);
            }
            return a-b;
        }

         /// <summary>
         /// функція множення чисел а і b
         /// </summary>
         /// <param name="a">множник</param>
         /// <param name="b">множник</param> /// <returns>добуток</returns>
        public double Mult(double a, double b)
        {
            if (-2147483648 > a * b || 2147483648 < a * b)
            {
                _lastError = "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від -2147483648 до 2147483647.";
                lastError = _lastError;
                throw new Exception(lastError);
            }
            return a * b;
        }
         /// <summary>
         /// функція знаходження частки
         /// </summary>
         /// <param name="a">ділене</param>
         /// <param name="b">дільник</param>
         /// <returns>частка</returns>
        public double Div(double a, double b)
        {
            if (b == 0)
            {
                _lastError = "Error 09 – Помилка ділення на 0.";
                lastError = _lastError;
                throw new Exception(_lastError);
            }
            return a / b;
        }

         /// <summary>
         /// функція ділення по модулю
         /// </summary>
         /// <param name="a">ділене</param>
         /// <param name="b">дільник</param>
         /// <returns>остача</returns>
        public double Mod(double a, double b)
        {
            if (-2147483648 > a % b || 2147483648 < a % b)
            {
                _lastError = "Error 06 — Дуже мале, або дуже велике значення числа для int. Числа повинні бути в межах від -2147483648 до 2147483647.";
                lastError = _lastError;
                throw new Exception(lastError);
            }
            return a % b;
        }
         /// <summary>
         /// унарний плюс 
         /// </summary>
         /// <param name="a"></param>
         /// <returns></returns>
        public double ABS(double a)
        {
            return a;
        }
         /// <summary>
         /// унарний мінус 
         /// </summary>
         /// <param name="a"></param> /// <returns></returns>
        public double IABS(double a)
        {
            return -a;
        }
    }
}
