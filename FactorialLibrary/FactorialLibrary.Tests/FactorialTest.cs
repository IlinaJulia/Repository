using Xunit;
using FactorialLibrary;

namespace FactorialLibrary.Tests
{
    public class FactorialCalculatorTests
    {
        [Fact] // јтрибут, указывающий, что это тестовый метод
        public void CalculateFactorial_ValidInput_ReturnsCorrectFactorial()
        {

            var calculator = new FactorialCalculator(5);
            int result = calculator.FactorialResult;
            Assert.Equal(120, result);
        }

        [Fact] // “ест дл€ проверки обработки отрицательного числа
        public void CalculateFactorial_NegativeInput_ReturnsErrorMessage()
        {
            var calculator = new FactorialCalculator(-1);
            string message = calculator.Message;
            Assert.Equal("‘акториал дл€ отрицательных чисел не определен.", message);
        }

        [Fact] // “ест дл€ проверки перегруженного метода
        public void CalculateFactorial_OverloadedMethod_ReturnsCorrectFactorial()
        {
            var calculator = new FactorialCalculator(4);
            int result = calculator.CalculateFactorial(4);
            Assert.Equal(24, result);
        }

        [Fact]
        public void CalculateFactorial_OverloadedMethodVerbose_ReturnsCorrectFactorial()
        {
            var calculator = new FactorialCalculator(3);
            int result = calculator.CalculateFactorial(3, true);
            Assert.Equal(6, result);
        }
    }
}
