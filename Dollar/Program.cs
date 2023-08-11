using System.Text;

namespace Dollar
{
    /// <summary>
    /// Моя программа
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главный метод
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region Ввод числа и проверка на корректность вводимых данных

            string input = "223323432.2";
            string integerPartText = string.Empty;
            string fractionalPartText = string.Empty;

            if (decimal.TryParse(input, out decimal value))
            {
                if (value < 0)
                    throw new Exception("Введено отрицательное число.");
                if (value > 2000000000)
                    throw new Exception("Число превышает двух миллиардов.");

                string validValueText = value.ToString();
                string[] list = validValueText.Split('.');
                integerPartText = list[0];
                if (list.Length > 1)
                {
                    fractionalPartText = list[1];
                    if (fractionalPartText.Length == 1)
                        fractionalPartText += "0";
                }
            }

            if (!string.IsNullOrEmpty(integerPartText) && !int.TryParse(integerPartText, out int integerPart))
                throw new Exception("Введена некорректная целая часть.");

            if (int.TryParse(fractionalPartText, out int fractPart))
            {
                if (fractPart > 99)
                    throw new Exception("Указан недопустимый цент.");
            }
            else
            {
                fractionalPartText = "0";
            }

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

            if (!integerPartList.Any()) integerPartList.Add(NumUtils.NumDict[0]);

            #endregion

            #region Вывод результата

            StringBuilder resultText = new StringBuilder();

            if (integerPartList.Any())
            {
                integerPartList.Reverse();
                resultText.AppendJoin(", ", integerPartList);
                resultText.Append(" DOLLARS");
            }

            int fractionalPart = int.Parse(fractionalPartText);
            if (fractionalPart > 0)
            {
                resultText.Append(" AND ");
                resultText.Append(fractionalPart.GetRestHundredText());
                resultText.Append(" CENTS");
            }

            Console.WriteLine(resultText);

            #endregion
        }
    }
}