using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProductLib
{
    public class Dishes: Product, IDrawable
    {
        private DrawingObject visual;

        public Dishes(string name, long article, string type, Point positionOfVisualRep) : base(name, article, type)   
        {
            visual = new DrawingObject(Properties.Resources.Dishes, positionOfVisualRep);
        }

        public Dishes(Dishes Source, DrawingObject visualSource): base(Source)
        {
            this.visual = new DrawingObject(visualSource);
        }

        public RectangleF AreaOfVisualisation
        {
            get
            {
                return new RectangleF(VisualPosition, SizeOfVisual);
            }
        }

        public PointF VisualPosition
        {
            get
            {
                return visual.Point;
            }
            private set
            {
                visual.Point = value;
            }
        }

        public Image Img
        {
            get
            {
                return visual.Img; // возврат ссылки!
            }
            private set
            {
                visual.Img = value; 
            }
        }

        public Size SizeOfVisual
        {
            get { return visual.SizeOfVisual; }
            set { visual.SizeOfVisual = value; }
        }

        public void MoveVisual(float dx, float dy)
        {
            PointF p = visual.Point;
            p.X = dx; p.Y = dy;
            visual.Point = p;
        }

        public void MoveVisualTo(PointF point)
        {

        }

        //public bool MoveVisualTo(PointF point, float speed)
        //{
        //    return visual.UniformMotion(point.X, point.Y, speed);
        //}

        public override Product Clone()
        {
            return new Dishes(this, visual);
        }
    }
}
