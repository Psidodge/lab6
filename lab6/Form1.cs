using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab6
{
    public partial class Form1 : Form
    {
        private Shapes ListOfShapes;
        private IShapes _iShapes;
        private Point _lastPosition;
        private int shapeNum;
        private bool onPaint, isZero;
        private int[,] signal;
        private NeuralNetwork neuralNetwork;

        public Form1()
        {
            InitializeComponent();
            ListOfShapes = new Shapes();
            _iShapes = ListOfShapes;
            _lastPosition = new Point(0, 0);
            shapeNum = 0;
            onPaint = false;
            neuralNetwork = new NeuralNetwork();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            onPaint = true;
            _lastPosition = new Point(0, 0);
            shapeNum++;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(onPaint && _lastPosition != e.Location)
            {
                _lastPosition = e.Location;
                _iShapes.AddShape(e.Location, Color.Black, 7, shapeNum); 
            }
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for(int i = 0; i < _iShapes.GetAmountOfShapes()-1; i++)
            {
                Shape shape1 = _iShapes.GetShapeAt(i);
                Shape shape2 = _iShapes.GetShapeAt(i + 1);
                if (shape1.GetShapeNumber == shape2.GetShapeNumber)
                {
                    Pen p = new Pen(shape1.GetShapeColor, shape1.GetShapeThickness)
                    {
                        StartCap = System.Drawing.Drawing2D.LineCap.Round,
                        EndCap = System.Drawing.Drawing2D.LineCap.Round
                    };
                    e.Graphics.DrawLine(p, shape1.GetPoint, shape2.GetPoint);
                    p.Dispose();
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            onPaint = false;
        }

        private void studyButton_Click(object sender, EventArgs e)
        {
            neuralNetwork.Study(signal, isZero);
        }
       
        private void sendButton_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = GetBitmapFromPanel();
            if(GetRawBytes(bitmap))
            {
                isZero = neuralNetwork.GetResult(signal);
                textBox.Text = isZero ? "It is 4":"It isnt 4";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WeightsForm form = new WeightsForm(neuralNetwork.GetMatrixWeights(),neuralNetwork.GetColumns,neuralNetwork.GetRows);
            form.ShowDialog();
        }


        private bool GetRawBytes(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                signal = new int[bitmap.Height,bitmap.Width];
                for (int i = 0; i < bitmap.Height; i++)
                    for (int j = 0; j < bitmap.Width; j++)
                        if (bitmap.GetPixel(j, i).R + bitmap.GetPixel(j, i).G + bitmap.GetPixel(j, i).B != 0)
                        {
                            signal[i,j] = 0;
                        }
                        else
                        {
                            signal[i,j] = 1;
                        }
                return true;
            }
            return false;
        }

        private Bitmap GetBitmapFromPanel()
        {
            int size = panel1.Width * panel1.Height;
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bitmap, new Rectangle(0,0, panel1.Width, panel1.Height));//new Rectangle(panel1.Location, new Size(panel1.Width, panel1.Height)));
            panel1.Controls.Clear();
            _iShapes.Clear();
            panel1.Refresh();
            return bitmap;
        }
    }
}
