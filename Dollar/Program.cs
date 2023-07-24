using System.Text.RegularExpressions;

namespace Dollar
{
    /// <summary>
    /// Моя программа
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            #region Ввод числа и проверка на корректность вводимых данных

            //string input = "1357256.32";
            //string input = "2147483647.32";
            string input = "1020040500.00";
            //string input = "0.00";
            Match match = Regex.Match(input, "^\\d+\\.\\d\\d$");

            if (!match.Success) throw new Exception("Введено некорректное число.");
            if (input.Length > 13) throw new Exception("Введено слишком длинное число.");

            string[] list = input.Split('.');
            string integerPartText = list[0];
            string fractionalPartText = list[1];

            if (!string.IsNullOrEmpty(integerPartText) && !int.TryParse(integerPartText, out int integerPart))
                throw new Exception("Введена некорректная целая часть.");

            if (int.Parse(integerPartText) > 2000000000)
                throw new Exception("Число превышает двух миллиардов.");

            #endregion

            #region Преобразование числа в текст

            List<string> integerPartList = new List<string>();

            int j = 1;
            for (int i = integerPartText.Length - 1; i >= 0; i -= 3)
            {
                string numText = string.Empty;

                string nextNum = i >= 2 ?
                    integerPartText.Substring(i - 2, 3) : integerPartText.Substring(0, i + 1);

                int num = int.Parse(nextNum);
                int hundred = num / 100;
                int restHundred = num % 100;

                // Сотня
                if (hundred > 0) numText += hundred.GetHundredText();

                // Меньше сотни
                if (restHundred > 0)
                {
                    if (!string.IsNullOrEmpty(numText)) numText += " and ";
                    numText += restHundred.GetRestHundredText();
                }

                if (j > 1 && !string.IsNullOrEmpty(numText)) numText += " " + j.GetNumClassText();
                j *= 1000;

                if (!string.IsNullOrEmpty(numText))
                    integerPartList.Add(numText);
            }

            #endregion

            #region Вывод результата

            List<string> resultTexts = new List<string>();

            if (integerPartList.Any())
            {
                integerPartList.Reverse();
                resultTexts.Add(string.Join(", ", integerPartList) + " DOLLARS");
            }

            int fractionalPart = int.Parse(fractionalPartText);
            if (fractionalPart > 0)
                resultTexts.Add(fractionalPart.GetRestHundredText() + " CENTS");

            Console.WriteLine(string.Join(" AND ", resultTexts));

            #endregion
        }
    }

    /// <summary>
    /// Вспомогательный класс для работы с числами
    /// </summary>
    public static class NumUtils
    {
        /// <summary>
        /// Словарь текста чисел
        /// </summary>
        public static Dictionary<int, string> NumDict { get; } = new Dictionary<int, string>
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },


                { 10, "ten" },
                { 11, "eleven" },
                { 12, "twelve" },
                { 13, "thirteen" },
                { 14, "fourteen" },
                { 15, "fifteen" },
                { 16, "sixteen" },
                { 17, "seventeen" },
                { 18, "eighteen" },
                { 19, "nineteen" },


                { 20, "twenty" },
                { 30, "thirty" },
                { 40, "fourty" },
                { 50, "fifty" },
                { 60, "sixty" },
                { 70, "seventy" },
                { 80, "eighty" },
                { 90, "ninety" },
                { 100, "hundred" },
                { 1000, "thousand" },
                { 1000000, "million" },
                { 1000000000, "billion" },
            };

        /// <summary>
        /// Получение текстового представления сотни
        /// </summary>
        /// <param name="num">Сотня</param>
        /// <returns>Текстовое представление числа</returns>
        public static string GetHundredText(this int num) => $"{NumDict[num]} {NumDict[100]}";

        /// <summary>
        /// Получение текстового представления остатка от 100
        /// </summary>
        /// <param name="num">Число, являющимся остатком от 100</param>
        /// <returns>Текстовое представление числа</returns>
        public static string GetRestHundredText(this int num)
        {
            if (num >= 1 && num <= 19) return NumDict[num];

            int tens = num / 10;
            int leftTens = num % 10;
            string restHundredText = NumDict[tens * 10];

            if (leftTens > 0)
                restHundredText += " " + NumDict[leftTens];

            return restHundredText;
        }

        /// <summary>
        /// Получение текстового представления класса числа
        /// </summary>
        /// <param name="numClass">Номер класса числа</param>
        /// <returns>Текстовое представление класса числа</returns>
        public static string GetNumClassText(this int numClass) => NumDict[numClass];
    }
}