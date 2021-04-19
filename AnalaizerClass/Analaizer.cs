using System;
using System.Linq;
using System.Collections.Generic;

namespace AnalaizerClass // AnalaizerClass.dll – модуль, в якому реалізований синтаксичний розбір виразу, а також його обчислення.
{
    /* 4.3. Додаткові вимоги до вхідного виразу
     ** 4.3.1. Максимальне сумарне число операторів і чисел – 30.
     ** 4.3.2. Максимальна глибина вкладеності структури в дужках – 3.
     //* 4.3.3. Унарний мінус — «m», унарний плюс — «p». // обойдёмся - изменение требований //
     * 4.3.4. Для операції знаходження частки – «/», для знаходження залишку — «mod».
     ** 4.3.5. Між операторними дужками і числами може бути будь-яка кількість пропусків.
     ** 4.3.6. Дозволяється використовувати лише дужки вигляду «(» і «)»
     ** 4.3.7. Максимальна довжина виразу – 65535 символів.*/
    public class Analaizer
    {
        static Stack<string> operatorStack = new Stack<string>();
        static string tmp;
        static double ro;
        static double lo;
        /// <summary>
        /// позиція виразу, на якій знайдена синтаксична помилка (у випадку відловлення на рівні виконання - не визначається)
        /// </summary>
        private static int erposition = 0;
        /// <summary>
        /// Вхідний вираз
        /// </summary>
        public static string expression = "";
        /// <summary>
        /// Показує, чи є необхідність у виведенні повідомлень про помилки. У разі консольного запуску програми це значення - false.
        /// </summary>
        public static bool ShowMessage = true;
        /// <summary>
        /// Перевірка коректності структури в дужках вхідного виразу  // обойдёмся - изменение требований //
        /// </summary>
        /// <returns>true - якщо все нормально, false- якщо є помилка</returns>
        /// метод біжить по вхідному виразу, символ за символом аналізуючи його, і рахуючи кількість дужок.У разі виникнення
        /// помилки повертає false, а в erposition записує позицію, на якій виникла помилка.
        public static bool CheckCurrency()
        {
            return true;
        }
        /// <summary>
        /// Форматує вхідний вираз, виставляючи між операторами пропуски і видаляючи зайві, а також знаходить нерозпізнані оператори, стежить за кінцем рядка
        /// а також знаходить помилки в кінці рядка
        /// </summary>
        /// <returns>кінцевий рядок або повідомлення про помилку, що починаються з спец. символу &</returns>
        public static string Format()
        {
            var inputStr = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            expression = string.Empty;
            foreach (var item in inputStr) expression += item;
            if (expression.EndsWith("=")) expression = expression.Remove(expression.Length - 1);

            return expression;
        }
        /// <summary>
        /// Формує масив, в якому розташовуються оператори і символи представлені в зворотному польському записі(без дужок)
        /// На цьому ж етапі відшукується решта всіх помилок (див. код). По суті - це компіляція.
        /// </summary>
        /// <returns>массив зворотнього польського запису</returns>
        public static System.Collections.ArrayList CreateStack()
        {
            string inputStr = null;
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            operatorStack.Clear();

            int inputStrMaxCount = UInt16.MaxValue + 1;
            int listMaxCount = 30;
            int brMaxCount = 3;
            int brOpen = 0;
            int brClose = 0;
            bool isDigit = false;

            for (int i = 0; i < expression.Length; i++)
            //foreach (var c in expression)
            {
                char c = expression[i];

                if (char.IsDigit(c))
                {
                    inputStr += c;
                    isDigit = true;
                    continue;
                }
                if (isDigit)
                {
                    list.Add(inputStr.ToString());
                    inputStr = null;
                    isDigit = false;
                }
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == 'm')
                {
                    if ((expression[0] == '*' || expression[0] == '/' || expression[0] == 'm') ||
                        (i > 0 && (c == '+' || c == '-' || c == '*' || c == '/' || c == 'm') &&
                        (expression[i - 1] == '+' || expression[i - 1] == '-' || expression[i - 1] == '*' || expression[i - 1] == '/' || expression[i - 1] == 'm')) ||
                        (c == 'm' && i > expression.Length - 3) ||
                        (c == 'm' && expression[i + 1] != 'o' && expression[i + 2] != 'd')) throw new Exception("Error 03 — Невірна синтаксична конструкція вхідного виразу");

                    if (expression.Length == i + 1) throw new Exception("Error 05 — Незавершений вираз");
                    if (i > 0 && c == expression[i - 1]) throw new Exception($"Error 04 at <{i}> — Два підряд оператори на <{i}> символі");

                    if (operatorStack.Count == 0)
                    {
                        operatorStack.Push(c.ToString());
                        continue;
                    }
                    else if (operatorStack.Count != 0)
                    {
                        if (ContainerOperator.Find(operatorStack.Peek()).Priority < ContainerOperator.Find(c.ToString()).Priority)
                        {
                            operatorStack.Push(c.ToString());
                            continue;
                        }
                    }
                    if (operatorStack.Count != 0)
                    {
                        try
                        {
                            while (ContainerOperator.Find(operatorStack.Peek()).Priority >= ContainerOperator.Find(c.ToString()).Priority)
                            {
                                list.Add(operatorStack.Pop());
                                //inputStr += operatorStack.Pop();
                            }
                        }
                        catch { }
                        if (operatorStack.Count == 0)
                        {
                            operatorStack.Push(c.ToString());
                            continue;
                        }
                        else if (operatorStack.Count != 0)
                        {
                            if (ContainerOperator.Find(operatorStack.Peek()).Priority < ContainerOperator.Find(c.ToString()).Priority)
                            {
                                operatorStack.Push(c.ToString());
                                continue;
                            }
                        }
                    }
                }
                else if (c == '(')
                {
                    brOpen++;
                    if (brOpen > brMaxCount) throw new Exception("Максимальна глибина вкладеності структури в дужках – 3.");
                    operatorStack.Push(c.ToString());
                    continue;
                }
                else if (c == ')')
                {
                    brClose++;
                    if (brClose > brOpen) throw new Exception($"Error 01 at <{i + 1}> — Неправильна структура в дужках, помилка на <{i + 1}> символі.");

                    while (operatorStack.Peek() != "(")
                    {
                        list.Add(operatorStack.Pop());
                        //inputStr += operatorStack.Pop();
                    }
                    operatorStack.Pop();
                    continue;
                }
                else if (c == ' ' || c == 'o' || c == 'd') continue;
                else throw new Exception($"Error 02 at <{i + 1}> — Невідомий оператор на <{i + 1}> символі.");
            }
            if (brClose != brOpen) throw new Exception("Error 05 — Незавершений вираз");
            if (isDigit)
            {
                list.Add(inputStr.ToString());
                inputStr = null;
                isDigit = false;
            }
            while (operatorStack.Count != 0)
            {
                list.Add(operatorStack.Pop());
                //inputStr += operatorStack.Pop();
            }
            int count = 0;
            list.ToArray().ToList().ForEach(x => count += x.ToString().Length);
            if (count >= inputStrMaxCount) throw new Exception("Error 07 — Дуже довгий вираз. Максмальная довжина — 65536 символів.");
            //if (inputStr.Length >= inputStrMaxCount) throw new Exception("Error 07 — Дуже довгий вираз. Максмальная довжина — 65536 символів.");
            //expression = inputStr;
            //foreach (var item in inputStr)
            //{
            //    list.Add(item.ToString());
            //}
            if (list.Count >= listMaxCount) throw new Exception("Error 08 — Сумарна кількість чисел і операторів перевищує 30.");

            return list;
        }
        /// <summary>
        /// Обчислення зворотнього польського запису
        /// </summary>
        ///<returns>результат обчислень,або повідомлення про помилку</returns>
        public static string RunEstimate(System.Collections.ArrayList list)
        {
            CalcClass.Calc calc = new CalcClass.Calc();
            foreach (string c in list)
            {
                double num = 0;
                if (double.TryParse(c, out num))
                {
                    operatorStack.Push(c.ToString());
                    continue;
                }
                else if (!double.TryParse(c, out num))
                {
                    if (c == "+")
                    {
                        tmp = calc.Add(Convert.ToDouble(operatorStack.Pop()), Convert.ToDouble(operatorStack.Pop())).ToString();
                    }
                    else if (c == "-")
                    {
                        ro = Convert.ToDouble(operatorStack.Pop());
                        lo = Convert.ToDouble(operatorStack.Pop());
                        tmp = calc.Sub(lo, ro).ToString();
                    }
                    else if (c == "*")
                    {
                        tmp = calc.Mult(Convert.ToDouble(operatorStack.Pop()), Convert.ToDouble(operatorStack.Pop())).ToString();
                    }
                    else if (c == "/")
                    {
                        ro = Convert.ToDouble(operatorStack.Pop());
                        lo = Convert.ToDouble(operatorStack.Pop());
                        tmp = calc.Div(lo, ro).ToString();
                    }
                    operatorStack.Push(tmp);
                }
            }
            return operatorStack.Pop();
        }
        /// <summary>
        /// Метод, який організовує обчислення. По черзі запускає CheckCurrency, Format, CreateStack і RunEstimate
        /// </summary>
        /// <returns></returns>
        public static string Estimate()
        {
            string tmp = null;
            try
            {
                Format();
                tmp = RunEstimate(CreateStack());
            }
            catch (Exception ex) { tmp = ex.Message; }
            expression = string.Empty;
            //CheckCurrency();
            return tmp;
        }
    }
    #region Test
    //public class MyTest
    //{
    //    string inputStr { get; set; }
    //    Stack<string> operatorStack = new Stack<string>();
    //    string tmp;
    //    double ro;
    //    double lo;

    //    public MyTest()
    //    {
    //        ContainerOperator.Add(new Operator(1, "("));
    //        ContainerOperator.Add(new Operator(2, "+"));
    //        ContainerOperator.Add(new Operator(2, "-"));
    //        ContainerOperator.Add(new Operator(3, "*"));
    //        ContainerOperator.Add(new Operator(3, "/"));
    //    }
    //    public string GetStr(string expression)
    //    {
    //        foreach (var c in expression)
    //        {
    //            if (char.IsDigit(c))
    //            {
    //                inputStr += c;
    //                continue;
    //            }
    //            if (c == '+' || c == '-' || c == '*' || c == '/')
    //            {
    //                if (operatorStack.Count == 0)
    //                {
    //                    operatorStack.Push(c.ToString());
    //                    continue;
    //                }
    //                else if (operatorStack.Count != 0)
    //                {
    //                    if (ContainerOperator.Find(operatorStack.Peek()).Priority <
    //                        ContainerOperator.Find(c.ToString()).Priority)
    //                    {
    //                        operatorStack.Push(c.ToString());
    //                        continue;
    //                    }
    //                }
    //                if (operatorStack.Count != 0)
    //                {
    //                    try
    //                    {
    //                        while (ContainerOperator.Find(operatorStack.Peek()).Priority >=
    //                            ContainerOperator.Find(c.ToString()).Priority)
    //                        {
    //                            inputStr += operatorStack.Pop();
    //                        }
    //                    }
    //                    catch { throw new Exception("catch"); }
    //                    if (operatorStack.Count == 0)
    //                    {
    //                        operatorStack.Push(c.ToString());
    //                        continue;
    //                    }
    //                    else if (operatorStack.Count != 0)
    //                    {
    //                        if (ContainerOperator.Find(operatorStack.Peek()).Priority <
    //                            ContainerOperator.Find(c.ToString()).Priority)
    //                        {
    //                            operatorStack.Push(c.ToString());
    //                            continue;
    //                        }
    //                    }
    //                }
    //            }
    //            if (c == '(')
    //            {
    //                operatorStack.Push(c.ToString());
    //                continue;
    //            }
    //            if (c == ')')
    //            {
    //                while (operatorStack.Peek() != "(")
    //                {
    //                    inputStr += operatorStack.Pop();
    //                }
    //                operatorStack.Pop();
    //                continue;
    //            }
    //        }
    //        while (operatorStack.Count != 0)
    //        {
    //            inputStr += operatorStack.Pop();
    //        }

    //        return inputStr;
    //    }
    //    public string Count(string expression)
    //    {
    //        foreach (var c in expression)
    //        {
    //            if (char.IsDigit(c))
    //            {
    //                operatorStack.Push(c.ToString());
    //                continue;
    //            }
    //            else if (!char.IsDigit(c))
    //            {
    //                if (c == '+') tmp = (Convert.ToDouble(operatorStack.Pop()) + Convert.ToDouble(operatorStack.Pop())).ToString();
    //                else if (c == '-')
    //                {
    //                    ro = Convert.ToDouble(operatorStack.Pop());
    //                    lo = Convert.ToDouble(operatorStack.Pop());
    //                    tmp = (lo - ro).ToString();
    //                }
    //                else if (c == '*')
    //                {
    //                    tmp = (Convert.ToDouble(operatorStack.Pop()) * Convert.ToDouble(operatorStack.Pop())).ToString();
    //                }
    //                else if (c == '/')
    //                {
    //                    ro = Convert.ToDouble(operatorStack.Pop());
    //                    lo = Convert.ToDouble(operatorStack.Pop());
    //                    tmp = (lo / ro).ToString();
    //                }
    //                operatorStack.Push(tmp);
    //            }
    //        }
    //        return operatorStack.Pop();
    //    }
    //}
    ///
    //public static System.Collections.ArrayList CreateStack()
    //{
    //    string inputStr = null;
    //    System.Collections.ArrayList list = new System.Collections.ArrayList();
    //    operatorStack.Clear();

    //    int inputStrMaxCount = UInt16.MaxValue + 1;
    //    int listMaxCount = 30;
    //    int brMaxCount = 3;
    //    int brOpen = 0;
    //    int brClose = 0;

    //    for (int i = 0; i < expression.Length; i++)
    //    //foreach (var c in expression)
    //    {
    //        char c = expression[i];

    //        if (char.IsDigit(c))
    //        {
    //            inputStr += c;
    //            continue;
    //        }
    //        else if (c == '+' || c == '-' || c == '*' || c == '/' || c == 'm')
    //        {
    //            if ((expression[0] == '*' || expression[0] == '/' || expression[0] == 'm') ||
    //                (i > 0 && (c == '+' || c == '-' || c == '*' || c == '/' || c == 'm') &&
    //                (expression[i - 1] == '+' || expression[i - 1] == '-' || expression[i - 1] == '*' || expression[i - 1] == '/' || expression[i - 1] == 'm')) ||
    //                (c == 'm' && i > expression.Length - 3) ||
    //                (c == 'm' && expression[i + 1] != 'o' && expression[i + 2] != 'd')) throw new Exception("Error 03 — Невірна синтаксична конструкція вхідного виразу");

    //            if (expression.Length == i + 1) throw new Exception("Error 05 — Незавершений вираз");
    //            if (i > 0 && c == expression[i - 1]) throw new Exception($"Error 04 at <{i}> — Два підряд оператори на <{i}> символі");

    //            if (operatorStack.Count == 0)
    //            {
    //                operatorStack.Push(c.ToString());
    //                continue;
    //            }
    //            else if (operatorStack.Count != 0)
    //            {
    //                if (ContainerOperator.Find(operatorStack.Peek()).Priority < ContainerOperator.Find(c.ToString()).Priority)
    //                {
    //                    operatorStack.Push(c.ToString());
    //                    continue;
    //                }
    //            }
    //            if (operatorStack.Count != 0)
    //            {
    //                try
    //                {
    //                    while (ContainerOperator.Find(operatorStack.Peek()).Priority >= ContainerOperator.Find(c.ToString()).Priority)
    //                    {
    //                        inputStr += operatorStack.Pop();
    //                    }
    //                }
    //                catch { }
    //                if (operatorStack.Count == 0)
    //                {
    //                    operatorStack.Push(c.ToString());
    //                    continue;
    //                }
    //                else if (operatorStack.Count != 0)
    //                {
    //                    if (ContainerOperator.Find(operatorStack.Peek()).Priority < ContainerOperator.Find(c.ToString()).Priority)
    //                    {
    //                        operatorStack.Push(c.ToString());
    //                        continue;
    //                    }
    //                }
    //            }
    //        }
    //        else if (c == '(')
    //        {
    //            brOpen++;
    //            if (brOpen > brMaxCount) throw new Exception("Максимальна глибина вкладеності структури в дужках – 3.");
    //            operatorStack.Push(c.ToString());
    //            continue;
    //        }
    //        else if (c == ')')
    //        {
    //            brClose++;
    //            if (brClose > brOpen) throw new Exception($"Error 01 at <{i + 1}> — Неправильна структура в дужках, помилка на <{i + 1}> символі.");

    //            while (operatorStack.Peek() != "(")
    //            {
    //                inputStr += operatorStack.Pop();
    //            }
    //            operatorStack.Pop();
    //            continue;
    //        }
    //        else if (c == ' ' || c == 'o' || c == 'd') continue;
    //        else throw new Exception($"Error 02 at <{i + 1}> — Невідомий оператор на <{i + 1}> символі.");
    //    }
    //    if (brClose != brOpen) throw new Exception("Error 05 — Незавершений вираз");
    //    while (operatorStack.Count != 0)
    //    {
    //        inputStr += operatorStack.Pop();
    //    }
    //    if (inputStr.Length >= inputStrMaxCount) throw new Exception("Error 07 — Дуже довгий вираз. Максмальная довжина — 65536 символів.");
    //    expression = inputStr;
    //    foreach (var item in inputStr)
    //    {
    //        list.Add(item.ToString());
    //    }
    //    if (list.Count >= listMaxCount) throw new Exception("Error 08 — Сумарна кількість чисел і операторів перевищує 30.");

    //    return list;
    //}
    #endregion
}
