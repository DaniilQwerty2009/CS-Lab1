using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ProductLib
{
    // Исключение, описывающее попытку создать объект мебель без компонентов или исключить все компоненты из объекта мебели
    internal class EmptyComponentsExeptions : Exception
    {
        public EmptyComponentsExeptions() : base("Комплектация товара не может быть пустой")
        {   }
        public EmptyComponentsExeptions(string message) : base(message) 
        {   }
    }

    // Исключение, описывающее неверный формат строки или диапазона чисел 
    internal class WrongFormatExeption : Exception
    {
        string? data = null;
        public WrongFormatExeption(string message, string data = null) : base(message) 
        {
            if(data != null)
                this.data = data;
        }

        public WrongFormatExeption(string? data = null) : base("Неверный формат")
        {
            if (data != null)
                this.data = data;
        }
    }

}
