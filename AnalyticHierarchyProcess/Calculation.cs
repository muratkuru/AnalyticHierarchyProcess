using System;

namespace AnalyticHierarchyProcess
{
    public class Calculation
    {
        private double[,] decisionMatrix;
        private double randomIndexValue;
        private int n;

        public Calculation(double[,] decisionMatrix, string authorName = "oak ridge")
        {
            if (decisionMatrix == null)
                throw new ArgumentNullException("decisionMatrix");

            int rowCount = decisionMatrix.GetLength(0);
            int columnCount = decisionMatrix.GetLength(1);

            if (rowCount != columnCount)
                throw new Exception("Decision matrix should be a square matrix.");

            n = rowCount;

            this.decisionMatrix = decisionMatrix;

            NormalizedMatrix = new double[n, n];
            ColumnSum = new double[n];
            PriorityVector = new double[n];
            ColumnVector = new double[n];

            var randomIndex = new RandomIndex();
            randomIndexValue = randomIndex.GetValue(n, authorName);

            SetColumnSum();
            SetNormalizedMatrix();
            SetPriorityVector();
            SetColumnVector();
            SetLambdaMax();
            SetConsistencyIndex();
            SetConsistencyRatio();

            IsConsistent = ConsistencyRatio < 0.1;
        }

        public double[,] NormalizedMatrix { get; set; }

        public double[] ColumnSum { get; set; }

        public double[] PriorityVector { get; set; }

        public double[] ColumnVector { get; set; }

        public double LambdaMax { get; set; }

        public double ConsistencyIndex { get; set; }

        public double ConsistencyRatio { get; set; }

        public bool IsConsistent { get; set; }

        private void SetColumnSum()
        {
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += decisionMatrix[j, i];
                }

                ColumnSum[i] = sum;
            }
        }

        private void SetNormalizedMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    NormalizedMatrix[j, i] = decisionMatrix[j, i] / ColumnSum[i];
                }
            }
        }

        private void SetPriorityVector()
        {
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += NormalizedMatrix[i, j];
                }
                PriorityVector[i] = sum / 3;
            }
        }

        private void SetColumnVector()
        {
            for (int i = 0; i < n; i++)
            {
                double calc = 0;
                for (int j = 0; j < n; j++)
                {
                    calc += decisionMatrix[i, j] * PriorityVector[j];
                }
                ColumnVector[i] = calc;
            }
        }

        private void SetLambdaMax()
        {
            double totalLambda = 0;
            for (int i = 0; i < n; i++)
            {
                totalLambda += ColumnVector[i] / PriorityVector[i];
            }

            LambdaMax = totalLambda / n;
        }

        private void SetConsistencyIndex()
        {
            ConsistencyIndex = (LambdaMax - n) / (n - 1);
        }

        private void SetConsistencyRatio()
        {
            ConsistencyRatio = ConsistencyIndex / randomIndexValue;
        }
    }
}
