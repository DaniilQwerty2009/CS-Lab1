using System;

namespace ProductLib
{
    public class Furniture : Product
    {
        private List<string> components = new();

        public Furniture(string name, long article, string type, List<string> components)
        : base(name, article, type)
        {
            if(components != null)
            {
                foreach (string comp in components)
                {
                    if (!StringValidate(comp))
                        throw new WrongFormatExeption();
                    
                    this.components.Add(comp);
                }
            }
            else
            {
                throw new EmptyComponentsExeptions();
            }
        }

        public List<string> Components
        {
            get { return components; }
        }

        // Инексатор на список components
        public string this[int index]
        {
            get
            {   if(index < 0 || index >= components.Count) 
                    throw new IndexOutOfRangeException();
            
                return components[index]; 
            }
        }

        // Вывод информации об объекте в терминал
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.Write('/');
            foreach(string i in  components)
            {
                Console.Write(i + ';');
            }
        }

        // Расширить комплектацию
        public void AddComponent(string component)
        {
            ArgumentNullException.ThrowIfNull(component);

            if (!StringValidate(component))
                throw new WrongFormatExeption(component);

            components.Add(component);
        }
        
        // Сузить комплектацию
        public void PopComponent(string component)
        {
            if(components.Count == 1)
                throw new EmptyComponentsExeptions();

            components.Remove(component);
        }
    }
     
    
}
