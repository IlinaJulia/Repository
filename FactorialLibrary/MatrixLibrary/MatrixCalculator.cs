using System;

namespace MatrixLibrary
{
    public class MatrixCalculator
    {
        public int[,] Matrix { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        // Конструктор класса, инициализирующий матрицу и ее размеры
        public MatrixCalculator(int[,] matrix)
        {
            Matrix = matrix;
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);
        }

        // Метод для вычисления суммы всех элементов матрицы
        public int CalculateSum()
        {
            int sum = 0;
            foreach (var item in Matrix)
            {
                sum += item; // Суммируем каждый элемент матрицы
            }
            return sum; // Возвращаем общую сумму
        }

        // Метод для вычисления произведения всех элементов матрицы
        public int CalculateProduct()
        {
            int product = 1;
            foreach (var item in Matrix)
            {
                product *= item; // Умножаем каждый элемент матрицы
            }
            return product; // Возвращаем общее произведение
        }

        // Виртуальный метод для вычисления суммы текущей матрицы и другой матрицы
        public virtual int CalculateSum(int[,] otherMatrix)
        {
            // Проверка на совпадение размеров матриц
            if (Rows != otherMatrix.GetLength(0) || Columns != otherMatrix.GetLength(1))
            {
                throw new InvalidOperationException("Размеры матриц должны совпадать для сложения.");
            }

            int sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sum += Matrix[i, j] + otherMatrix[i, j]; // Суммируем соответствующие элементы
                }
            }
            return sum; // Возвращаем общую сумму
        }

        // Перегруженный метод для вычисления суммы с дополнительным параметром verbose
        public int CalculateSum(int[,] otherMatrix, bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("Вычисление суммы матриц...");
            }
            return CalculateSum(otherMatrix); // Вызываем основной метод
        }

        // Метод для вычисления произведения текущей матрицы и другой матрицы
        public int[,] CalculateProduct(int[,] otherMatrix)
        {
            // Проверка на соответствие размеров для умножения матриц
            if (Columns != otherMatrix.GetLength(0))
            {
                throw new InvalidOperationException("Число столбцов первой матрицы должно совпадать с числом строк второй матрицы.");
            }

            int rowsB = otherMatrix.GetLength(0);
            int colsB = otherMatrix.GetLength(1);
            int[,] result = new int[Rows, colsB]; // Инициализация результирующей матрицы

            // Вычисление произведения матриц
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    result[i, j] = 0; // Инициализируем элемент результирующей матрицы
                    for (int k = 0; k < Columns; k++)
                    {
                        result[i, j] += Matrix[i, k] * otherMatrix[k, j]; // Суммируем произведения
                    }
                }
            }

            return result; // Возвращаем результирующую матрицу
        }

        // Метод для вычисления суммы всех элементов результирующей матрицы от произведения с другой матрицей
        public int CalculateSumOfProduct(int[,] otherMatrix)
        {
            var productMatrix = CalculateProduct(otherMatrix); // Вычисляем произведение матриц
            int sum = 0;
            foreach (var item in productMatrix)
            {
                sum += item; // Суммируем все элементы результирующей матрицы
            }
            return sum; // Возвращаем общую сумму
        }
    }
}
