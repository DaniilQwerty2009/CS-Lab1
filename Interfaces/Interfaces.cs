using System.Drawing;

namespace Interfaces
{
    // Для продуктов, состоящих из нескольких компонентов, т.е. имеющих комплектацию
    public interface IIncludeComponents
    {
        IEnumerable<string> Components {  get; }

        public void AddComponent(string value);
        public bool RemoveComponent(string value);

        public bool RemoveComponent(int index);

        public bool CloneComponents(IEnumerable<string> siurce);
        public string this[int index] { get; }
    }

    public interface IDrawable
    {
        public System.Drawing.PointF VisualPosition
        {
            get;
            set;
        }

        public System.Drawing.Image Img
        {
            get;
        }

        public Size SizeOfVisual
        {
            get;
            set;
        }

        public RectangleF AreaOfVisualisation
        {
            get;
        }
    }

}
