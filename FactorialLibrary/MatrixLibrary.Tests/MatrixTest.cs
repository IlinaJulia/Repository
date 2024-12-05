using System;
using Xunit;
using MatrixLibrary;

namespace MatrixLibrary.Tests
{
    public class MatrixCalculatorTests
    {
        private MatrixCalculator _calculator;

        public MatrixCalculatorTests()
        {
            int[,] matrix = { { 1, 2 }, { 3, 4 } };
            _calculator = new MatrixCalculator(matrix);
        }

        [Fact] // Тест возвращения правильной суммы матрицы
        public void CalculateSum_ShouldReturnCorrectSum()
        {
            int expectedSum = 10; // 1 + 2 + 3 + 4
            int actualSum = _calculator.CalculateSum();
            Assert.Equal(expectedSum, actualSum);
        }

        [Fact] // Тест возвращения правильного произведения элементов матрицы
        public void CalculateProduct_ShouldReturnCorrectProduct()
        {
            int expectedProduct = 24; // 1 * 2 * 3 * 4
            int actualProduct = _calculator.CalculateProduct();
            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact] // Тест сложения матрицы с другой матрицей и проверки правильной суммы
        public void CalculateSum_WithOtherMatrix_ShouldReturnCorrectSum()
        {
            int[,] otherMatrix = { { 5, 6 }, { 7, 8 } };
            int expectedSum = 36; // (1 + 5) + (2 + 6) + (3 + 7) + (4 + 8)
            int actualSum = _calculator.CalculateSum(otherMatrix);
            Assert.Equal(expectedSum, actualSum);
        }

        [Fact] // Тест перемножения матрицы с другой матрицей и проверки правильного результата
        public void CalculateProduct_WithOtherMatrix_ShouldReturnCorrectProduct()
        {
            int[,] otherMatrix = { { 5, 6 }, { 7, 8 } };
            int[,] expectedProduct = { { 19, 22 }, { 43, 50 } }; // Ожидаемая результирующая матрица
            var actualProduct = _calculator.CalculateProduct(otherMatrix);

            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact] // Тест сложения матрицы с матрицей другого размера и проверки выбрасывания исключения
        public void CalculateSum_WithDifferentSizeMatrix_ShouldThrowException()
        {
            int[,] otherMatrix = { { 5, 6 } }; // 1x2 матрица
            var exception = Assert.Throws<InvalidOperationException>(() => _calculator.CalculateSum(otherMatrix));
            Assert.Equal("Размеры матриц должны совпадать для сложения.", exception.Message);
        }

        [Fact] // Тест перемножения матрицы с несовместимой матрицей и проверки выбрасывания исключения
        public void CalculateProduct_WithIncompatibleMatrix_ShouldThrowException()
        {
            int[,] otherMatrix = { { 5 } }; // 1x1 матрица
            var exception = Assert.Throws<InvalidOperationException>(() => _calculator.CalculateProduct(otherMatrix));
            Assert.Equal("Число столбцов первой матрицы должно совпадать с числом строк второй матрицы.", exception.Message);
        }
    }
}
