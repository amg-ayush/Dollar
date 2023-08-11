namespace Dollar
{
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
                { 0, "zero" },
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
