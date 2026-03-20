using System;
using Interfaces;

namespace ProductLib
{
    // Класс-изделие
    abstract public class Product : IValidate
    {
        private string name;    // название
        private long article;   // код
        private string type;    // тип

        protected Product(string name, long article, string type)
        {
            //Проверка на формат строк и чисел (только буквенные знаки, пробелы и натуральные числа)
            if(!StringValidate(name))
            {
                throw new WrongFormatExeption(name);
            }
            else if(!StringValidate(type))
            {
                throw new WrongFormatExeption(type);
            }
            else if(!NumberValidate(article))
            {
                throw new WrongFormatExeption(article.ToString());
            }

            this.name = name;
            this.article = article;
            this.type = type;
        }

        // Свойства
        public string Name
        {
            get { return name; }
        }
        public long Article
        {
            get { return article; }
        }

        public string Type
        {
            get { return type; }
        }

        // Проверка: только символы букв для строк 
        public bool StringValidate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            
            foreach(char c in value)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return false;
            }
            
            return true;
        }

        // Проверка: только натуральные числа для чисел
        public bool NumberValidate(long number)
        {
            if(number < 0) 
                return false;
            return true;
        }

        // Вывод информации об объекте в терминал
        public virtual void PrintInfo()
        {
            Console.Write(name + '/' + article + '/' + type);
        }
    }
    
}
