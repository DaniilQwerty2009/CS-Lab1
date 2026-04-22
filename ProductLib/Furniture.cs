using Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProductLib
{
    public class Furniture : Product, IIncludeComponents, IDrawable
    {
        private List<string> components = new();

        private DrawingObject visual;
        public Furniture(string name, long article, string type, IEnumerable<string> components, Point positionOfVisual)
        : base(name, article, type)
        {
            if(components != null)
            {
                foreach (string comp in components)
                {
                    if (!Validator.StringValidate(comp))
                        throw new WrongFormatExeption();

                    this.components.Add(comp.ToLower());
                }
            }
            else
            {
                throw new EmptyComponentsExeptions();
            }

            visual = new DrawingObject(Properties.Resources.Furniture, positionOfVisual);
        }

        public Furniture(Furniture source, DrawingObject visualSource) : base(source)
        {
            foreach(string comp in source.Components)
            {
                this.components.Add(comp);
            }

            this.visual = new DrawingObject(visualSource);
        }

        public  IEnumerable<string> Components
        {
            get { return components; }
        }

        public PointF VisualPosition
        {
            get { return visual.Point; }
            private set { visual.Point = value; }
        }

        public Size SizeOfVisual
        {
            get { return visual.SizeOfVisual; }
            set { visual.SizeOfVisual = value; }
        }

        public RectangleF AreaOfVisualisation
        {
            get
            {
                return new RectangleF(VisualPosition, SizeOfVisual);
            }
        }

        public Image Img
        {
            get { return visual.Img; }
            private set
            { visual.Img = value; }
        }

        string IIncludeComponents.this[int index]
        {
            get
            {
                if (index < 0 || index >= components.Count)
                    throw new IndexOutOfRangeException();

                return components[index];
            }
        }

        // Вывод информации об объекте в терминал
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.Write('/');
            foreach(string i in components)
            {
                Console.Write(i + ';');
            }
        }

        // Расширить комплектацию
        public void AddComponent(string component)
        {
            ArgumentNullException.ThrowIfNull(component);

            if (!Validator.StringValidate(component))
            {
                WrongFormatExeption ex = new();
                ex.Value = component;
                throw ex;
            }

            components.Add(component);
        }
        
        // Сузить комплектацию
        public bool RemoveComponent(string component)
        {
            if(components.Count == 1)
                throw new EmptyComponentsExeptions();

            return components.Remove(component);
        }

        public bool RemoveComponent(int index)
        {
            if(index < 0 || index >= components.Count)
                throw new IndexOutOfRangeException();
            if (components.Count == 1)
                throw new EmptyComponentsExeptions();

            components.RemoveAt(index);

            return true;
        }

        public bool CloneComponents(IEnumerable<string> source)
        {
            
            foreach(string s in source)
            {
                if(!Validator.StringValidate(s))
                {
                    WrongFormatExeption ex = new();
                    ex.Value = s;
                    throw ex;
                }
            }


            components.Clear();

            components.AddRange(source);

            return true;
        }

        public override void CopyFrom(Product source)
        { 
            base.CopyFrom(source);
            if(source is Furniture furniture)
            {
                components = new List<string>(furniture.components);
            }
        }

        public override Product Clone()
        {
            return new Furniture(this, visual);
        }
   
        public void MoveVisual(float dx, float dy)
        {
            PointF p = visual.Point;
            p.X = dx; p.Y = dy;
            visual.Point = p;
        }

        public void MoveVisualTo(PointF point)
        { }

        //public bool MoveVisualTo(PointF point, float speed)
        //{
        //    return visual.UniformMotion(point.X, point.Y, speed);
        //}
    }
     
}
