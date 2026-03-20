using System;
using System.Text;
using ProductLib;

namespace ProgramOfLab1
{
    internal class Program
    {
        static int Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding  = Encoding.UTF8;

            List<string> componentsList = new();
            componentsList.Add("Ножки");
            componentsList.Add("Сиденье");
            componentsList.Add("Спинка");

            Furniture obj1 = new("Четырехногий мальчуган", 1, "Стул", componentsList);
            Furniture obj2 = new("ОФисный работяга", 2, "Стул", componentsList);

            List<Product> l = new(); 

            l.Add(obj1);
            l.Add(obj2);

            foreach(Product p in l)
            {
                Console.WriteLine(p.Name);
                Console.WriteLine(p.Article);
                Console.WriteLine(p.Type);

                if(p is Furniture f)
                {
                    foreach(string comp in f.Components)
                    {
                        Console.Write(comp +  "; ");
                    }
                }
                Console.WriteLine();
            }


            foreach(Product p in l)
            {
                p.PrintInfo();
            }

            //Console.WriteLine("Имя: " + product.Name);
            //Console.WriteLine("Артикль: " + product.Article);
            //Console.WriteLine("Тип: " + product.Type);
                
            //if (product is Furniture furniture)
            //{
            //    Console.Write("Комплектация: ");

            //    foreach (string s in furniture.Components) Console.Write(s + "; ");
            //}

            //Console.WriteLine();



            return 0;
        }
    }

}
