using Interfaces;
using ProductLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace ProductLib
{
    /// <summary>
    /// Класс, обновляющий координаты визуальной сотовляющей объекта IDrawable
    /// </summary>
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


        public PointF CurrentPosition
        {
            get { return visual.VisualPosition; }
        }

        public RectangleF AreaOfVisualisation
        {
            get { return visual.AreaOfVisualisation; }
        }


        /// <summary>
        /// Основной метод обновления координат
        /// </summary>
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


            visual.VisualPosition = new PointF(x1, y1);

        }

        /// <summary>
        /// Рассчитьывает расстояние между точками в пикселях и возвращает результат вычисления
        /// </summary>
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

    /// <summary>
    /// Мувер,  специализирующийся на прямолинейном движении между двумя точками: начала пути и точки назначения
    /// </summary>
    public class LineralMover : Mover 
    {

        PointF beginPosition;


        public LineralMover(IDrawable visual, PointF destination, int speed) : base(visual, destination, speed)
        {
            beginPosition = visual.VisualPosition;
        }

        public override void Step(float dt)
        {
            base.Step(dt);
            if (finish)
            {
                TurnBack();
            }
        }

        /// <summary>
        /// Поворачивает к предыдущей точке
        /// </summary>
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

    /// <summary>
    /// Мувер,  специализирующийся на прямолинейном движении в случайном направлении. Направление менятеся раз в пять секунд или по достижении границы отрисовки
    /// </summary>
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

        public override void Step(float dt)
        {
            while (finish)
                RenewDirection();

            base.Step(dt);

            movingTimer += dt;

            if (movingTimer >= 5)    
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

        /// <summary>
        /// Изменяет направление
        /// </summary>
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



    
        
    

