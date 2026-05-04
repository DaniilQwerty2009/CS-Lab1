using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ProductLib
{
    public class DrawingObject
    {

        System.Drawing.PointF point;
        System.Drawing.Image img;
        Size sizeOfVisual = new Size(50,50);
        RectangleF areaOfVisualisation;


        public DrawingObject()
        {
            point = new PointF(0,0);
            img = Properties.Resources.info;
            areaOfVisualisation = new RectangleF(point, sizeOfVisual);
        }

        public DrawingObject(Image img, PointF point)
        {
           
            this.point = (PointF)point;
            if(this.point.X < 0)
                this.point.X = 0;
            if (this.point.Y < 0)
                this.point.Y = 0;

            this.img = img;                  // Присваиваем ссылку
            areaOfVisualisation = new RectangleF(point, sizeOfVisual);
        }

        public DrawingObject(DrawingObject source)
        {
            this.point = source.point;
            this.img = source.img;
            areaOfVisualisation = new RectangleF(point, sizeOfVisual);
        }


        public PointF Point
        {
            get { return point; }
            set
            {
                point.X = value.X;
                point.Y = value.Y;

                if (point.X < 0)
                    point.X = 0;
                if(point.Y < 0)
                    point.Y = 0;

                areaOfVisualisation = new RectangleF(point, sizeOfVisual);
            }
        }

        public Image Img
        {
            get { return img; }
            set { img = value; }
        }

        public Size SizeOfVisual
        {
            get {  return sizeOfVisual; }
            set 
            { 
                this.sizeOfVisual = value;
                areaOfVisualisation = new RectangleF(point, sizeOfVisual);
            }
        }

        public RectangleF AreaOfVisualisation
        {
            get { return areaOfVisualisation; }
        }
    }
}
