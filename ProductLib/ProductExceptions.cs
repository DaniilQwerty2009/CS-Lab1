using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ProductLib
{
    // Исключение, описывающее попытку создать объект мебель без компонентов или исключить все компоненты из объекта мебели
    public class EmptyComponentsExceptions : Exception
    {
        public EmptyComponentsExceptions(string message = "Комплектация товара не может быть пустой") : base(message) 
        {   }
    }

    // Исключение, описывающее неверный формат строки или диапазона чисел 
    public class WrongFormatExсeption : Exception
    {
        string? value = null;
        public WrongFormatExсeption(string message = "Неверный формат") : base(message)
        {        }

        public string? Value
        {
            get { return value; }
            set { this.value = value; }
        }

    }

}
