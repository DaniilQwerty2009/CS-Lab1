using Interfaces;
using ProductLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace ProductLib
{
    abstract public class Mover
    {
        protected PointF destination;

        protected IDrawable visual;

        protected int speed;

        protected bool finish;


        public Mover(IDrawable visual, PointF destination, int speed)
        {
            this.visual = visual;
            this.destination = destination;
            this.speed = speed;

            if (DistanceBetweenPoints(CurrentPosition, destination) > speed)
            {
                finish = false;
            }
            else
                finish = true;
        }

        public IDrawable Visual
        {
            get { return visual; }
        }

        public PointF Destination
        {
            get { return destination; }
            protected set
            {
                destination = value;


                if (DistanceBetweenPoints(visual.VisualPosition, destination) < speed)
                    finish = true;
                else
                    finish = false;
            }
        }

        public bool Finish
        {
            get { return finish; }
        }

        public float Speed
        {
            get { return speed; }
        }


        public PointF CurrentPosition
        {
            get { return visual.VisualPosition; }
        }

        public RectangleF AreaOfVisualisation
        {
            get { return visual.AreaOfVisualisation; }
        }

        public virtual void Step(float dt)
        {

            if (DistanceBetweenPoints(visual.VisualPosition, destination) < speed * dt)
                finish = true;

            if (finish)
                return;

            float x1 = visual.VisualPosition.X;
            float y1 = visual.VisualPosition.Y;
            float x2 = destination.X;
            float y2 = destination.Y;

            float dx = (x2 - x1);
            float dy = (y2 - y1);
            float l = MathF.Sqrt(dx * dx + dy * dy);

            if (l == 0)
            {
                finish = true;
                return;
            }

            dx /= l;
            dy /= l;

            x1 += dx * speed * dt;
            y1 += dy * speed * dt;

            visual.MoveVisual(x1, y1);

        }

        static public float DistanceBetweenPoints(PointF point1, PointF point2)
        {
            float dx = (point2.X - point1.X);
            float dy = (point2.Y - point1.Y);
            float l = MathF.Sqrt(dx * dx + dy * dy);
            return l;
        }

        static public Point GetRandomPoint(Point border)
        {
            int x = Random.Shared.Next(border.X);
            int y = Random.Shared.Next(border.Y);

            return new Point(x, y);
        }

    }

    public class LineralMover : Mover // добавим свойства, получим разностароннее движение
    {

        PointF beginPosition;


        public LineralMover(IDrawable visual, PointF destination, int speed) : base(visual, destination, speed)
        {
            beginPosition = visual.VisualPosition;
        }


        //public PointF BeginPosition
        //{
        //    get { return beginPosition; }
        //}

        public override void Step(float dt)
        {
            base.Step(dt);
            if (finish)
            {
                TurnBack();
            }
        }


        private void TurnBack()
        {
            PointF temp = beginPosition;
            beginPosition = destination;
            destination = temp;

            if (DistanceBetweenPoints(CurrentPosition, destination) > speed)
            {
                finish = false;
            }
            else
                finish = true;
        }
    }

    public class RandomMover : Mover
    {
        Point border;
        float movingTimer;

        enum TargetBorder { left, top, right, bottom }; // очередное направление движения

        public RandomMover(IDrawable visual, int speed, Point border) : base(visual, new Point(0,0), speed)
        {
            this.border = border;
            RenewDirection();
            movingTimer = 0;
        }

        //public float MovingTimer
        //{
        //    get { return movingTimer; }
        //}

        public override void Step(float dt)
        {
            while (finish)
                RenewDirection();

            base.Step(dt);

            movingTimer += dt;

            if (movingTimer >= 5)    // 5 по условию задачи
            {
                finish = true;
            }
            if (visual.VisualPosition.X >= border.X || visual.VisualPosition.Y >= border.Y)
            {
                finish = true;
            }
            if (visual.VisualPosition.X <= 0 || visual.VisualPosition.Y <= 0)
            {
                finish = true;
            }

        }

        private void RenewDirection()
        {
            int targetingBorder = Random.Shared.Next(4); // Выбираем одну из четырех сторон

            int X, Y;   // координаты нового направления

            switch (targetingBorder)
            {
                case ((int)TargetBorder.left):
                    X = 0;
                    Y = Random.Shared.Next(border.Y);
                    Destination = new Point(X, Y);
                    break;

                case ((int)TargetBorder.top):
                    Y = 0;
                    X = Random.Shared.Next(border.X);
                    Destination = new Point(X, Y);
                    break;

                case ((int)TargetBorder.right):
                    X = border.X;
                    Y = Random.Shared.Next(border.Y);
                    Destination = new Point(X, Y);
                    break;

                case ((int)TargetBorder.bottom):
                    Y = border.Y;
                    X = Random.Shared.Next(border.X);
                    Destination = new Point(X, Y);
                    break;
            }


            finish = false;
            movingTimer = 0;
        }
    }
}



    
        
    

