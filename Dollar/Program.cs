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

            //string input = "1020040500.00";

            // На входе должно быть десятичное число, где после точки обязательно идут 2 цифры, а перед точкой должна быть хотя бы одна цифра
            string? input = Console.ReadLine();
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

            int numClass = 1;

            // Начиниая от класса единиц, двигаемся до класса миллиард
            for (int i = integerPartText.Length - 1; i >= 0; i -= 3)
            {
                string convertedNumText = string.Empty;

                // Последующая тройка чисел
                string numText = i >= 2 ?
                    integerPartText.Substring(i - 2, 3) : integerPartText.Substring(0, i + 1);

                int num = int.Parse(numText);
                int hundred = num / 100;
                int restHundred = num % 100;

                // Сотня
                if (hundred > 0) convertedNumText += hundred.GetHundredText();

                // Меньше сотни
                if (restHundred > 0)
                {
                    if (!string.IsNullOrEmpty(convertedNumText)) convertedNumText += " and ";
                    convertedNumText += restHundred.GetRestHundredText();
                }

                if (numClass > 1 && !string.IsNullOrEmpty(convertedNumText)) convertedNumText += " " + numClass.GetNumClassText();
                numClass *= 1000;

                if (!string.IsNullOrEmpty(convertedNumText))
                    integerPartList.Add(convertedNumText);
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
}