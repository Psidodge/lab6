using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace lab6
{
    interface IShapes
    {
        void AddShape(Point shapePoint, Color shapeColor, float shapeThickness, int shapeNumber);
        Shape GetShapeAt(int i);
        void Clear();
        int GetAmountOfShapes();
    }

    class Shapes : IShapes
    {
        private List<Shape> _listOfShapes;

        public Shapes()
        {
            _listOfShapes = new List<Shape>();
        }
        void IShapes.AddShape(Point shapePoint, Color shapeColor, float shapeThickness, int shapeNumber)
        {
            _listOfShapes.Add(new Shape(shapePoint,shapeColor,shapeThickness, shapeNumber));
        }
        Shape IShapes.GetShapeAt(int i)
        {
            return _listOfShapes[i];
        }
        void IShapes.Clear()
        {
            _listOfShapes.Clear();
        }
        int IShapes.GetAmountOfShapes()
        {
            return _listOfShapes.Count;
        }
    }

    class Shape
    {
        private Point _locationOfShape;
        private Color _shapeColor;
        private float _thickness;
        private int _shapeNumber;

        public Shape(Point point, Color color, float thickness, int shapeNumber)
        {
            this._locationOfShape = point;
            this._shapeColor = color;
            this._thickness = thickness;
            this._shapeNumber = shapeNumber;
        }

        public Point GetPoint { get => _locationOfShape; }
        public Color GetShapeColor { get => _shapeColor; }
        public float GetShapeThickness { get => _thickness; }
        public int GetShapeNumber { get => _shapeNumber; }
    }

    public class DoubleBufferdPanel : System.Windows.Forms.Panel
    {
        public DoubleBufferdPanel()
        {
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
