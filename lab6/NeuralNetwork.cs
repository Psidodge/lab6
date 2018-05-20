using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab6
{
    class NeuralNetwork
    {
        Neuron _neuron;

        public NeuralNetwork()
        {
            this._neuron = new Neuron();
        }

        public bool GetResult(int[,] input)
        {
            return _neuron.GetResult(input);
        }
        public int[,]GetMatrixWeights()
        {
            return _neuron.weights;
        }
        public void Study(int[,] input,bool isZero)
        {
            _neuron.ChangeWeights(input, isZero);
        }
        public int GetRows { get => _neuron.GetRows; }
        public int GetColumns { get => _neuron.GetColumns; }
    }


    //weights 80 100
    class Neuron
    {
        public int[,] weights;
        private int _rows = 40, _columns = 50;
        private double threshold = 0.8;
        private Random random;

        public Neuron()
        {
            this.weights = new int[_columns, _rows];
            random = new Random();
            RondomizeWeights();
        }

        public bool GetResult(int[,] inputBytes)
        {
            int resSum = 0;
            for (int i = 0; i < _columns; i++)
                for (int j = 0; j < _rows; j++)
                    resSum += inputBytes[i, j] * weights[i, j];
            double _out = 1 / (1 + Math.Exp(-resSum));
            return _out >= threshold ? true : false;
        }

        public int[,] GetResultMatrix(int[,] inputBytes)
        {
            int[,] resultMatrix = new int[_columns, _rows];
            for (int i = 0; i < _columns; i++)
                for (int j = 0; j < _rows; j++)
                    resultMatrix[i,j] = (inputBytes[i, j] * weights[i, j]);
            return resultMatrix;
        }

        public void ChangeWeights(int[,]siganl,bool isZero)
        {
            int[,] resultMatrix = GetResultMatrix(siganl);
            for (int col = 0; col < _columns; col++)
                for (int row = 0; row < _rows; row++)
                    if (!isZero)
                    {
                        weights[col, row] = weights[col, row] + (siganl[col, row] - resultMatrix[col, row]) * siganl[col, row];
                    }else
                    {
                        weights[col, row] = weights[col, row] - (siganl[col, row] - resultMatrix[col, row]) * siganl[col, row];
                    }
        }
       
        private void RondomizeWeights()
        {
            for (int i = 0; i < _columns; i++)
                for (int j = 0; j < _rows; j++)
                    weights[i, j] = 0;// (byte)random.Next(0, 10);
        }
        public int GetRows { get => _rows; }
        public int GetColumns { get => _columns; }
    }
}
