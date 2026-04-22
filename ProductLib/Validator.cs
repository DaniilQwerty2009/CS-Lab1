using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLib
{
    // Класс для валидации ввода, передаваемых в конструкторы параметров
    public class Validator
    {
        // Для строк проверка на пустую строку, только из пробелов, наличие спец. символов
        static public bool StringValidate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            foreach (char c in value)
            {
                if (!(char.IsLetter(c) || char.IsAsciiDigit(c)) && !char.IsWhiteSpace(c))
                    return false;
            }

            return true;
        }
    

        // Для чисел проверка только на неотрицательность
        static public bool NumberValidate(long number)
        {
            if (number < 0)
                return false;
            return true;
        }
}
}
