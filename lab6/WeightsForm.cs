using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class WeightsForm : Form
    {
        private int[,] _weights;
        private int _columns, _rows;
        public WeightsForm()
        {
            InitializeComponent();
        }

        public WeightsForm(int[,] weights,int columns, int rows)
        {
            InitializeComponent();
            this._weights = weights;
            this._columns = columns;
            this._rows = rows;
        }

        private void WeightsForm_Load(object sender, EventArgs e)
        {
            textBox1.Clear();
            for (int col = 0; col < _columns; col++)
            {
                for (int row = 0; row < _rows; row++)
                    textBox1.Text += _weights[col, row] + " ";
                textBox1.Text += Environment.NewLine;
            }
        }
    }
}
