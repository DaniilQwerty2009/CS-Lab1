using Interfaces;
using System;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

namespace ProductLib
{
    // Класс-изделие
    abstract public class Product
    {
        private string name;    // название
        private long article;   // код
        private string type;    // тип

        protected Product(string name, long article, string type)
        {
            //Проверка на формат строк и чисел (только буквенные знаки, пробелы и натуральные числа)
            if(!Validator.StringValidate(name))
            {
                var ex = new WrongFormatExeption();
                ex.Value = name;
                throw ex;
            }
            else if(!Validator.StringValidate(type))
            {
                var ex = new WrongFormatExeption();
                ex.Value = type;
                throw ex;

            }
            else if(!Validator.NumberValidate(article))
            {
                var ex = new WrongFormatExeption();
                ex.Value = article.ToString();
                throw ex;
            }

            this.name = name;

            this.article = article;
            this.type = type.ToLower();
        }

        protected Product(Product source) : this(source.Name, source.Article, source.Type)
        {        }
        // Свойства
        public string Name
        {
            get { return name; }
            set 
            {
                if (!Validator.StringValidate(value))
                {
                    var ex = new WrongFormatExeption();
                    ex.Value = value;
                    throw ex;
                }
                name =  value;
            }
        }

        public long Article
        {
            get { return article; }
            set 
            {
                if (!Validator.NumberValidate(value))
                {
                    var ex = new WrongFormatExeption();
                    ex.Value = value.ToString();
                    throw ex;
                }
                article = value; 
            }
        }

        public string Type
        {
            get { return type; }
            set 
            {
                if (!Validator.StringValidate(value))
                {
                    var ex = new WrongFormatExeption();
                    ex.Value = value;
                    throw ex;

                }
                type = value; 
            }
        }


        // Вывод информации об объекте в терминал
        public virtual void PrintInfo()
        {
            Console.Write(name + '/' + article + '/' + type);
        }

        public override string ToString()
        {
            return name + ", " + type;
        }
    
        public virtual void CopyFrom(Product source)
        {
            this.name = source.name;
            this.article = source.article;
            this.type = source.type;
        }

        public abstract Product Clone();

    }
    
}
