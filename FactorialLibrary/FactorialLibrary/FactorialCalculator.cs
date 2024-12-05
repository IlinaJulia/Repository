using System;

namespace FactorialLibrary
{
    public class FactorialCalculator
    {
        public int Number { get; set; }
        public int FactorialResult { get; private set; }
        public string Message { get; private set; }

        // Конструктор, принимающий число и вызывающий расчет факториала
        public FactorialCalculator(int number)
        {
            Number = number;
            CalculateFactorial();
        }

        // Метод для вычисления факториала числа
        public void CalculateFactorial()
        {
            if (Number < 0)
            {
                Message = "Факториал для отрицательных чисел не определен.";
                FactorialResult = -1;
                return;
            }

            FactorialResult = 1;
            for (int i = 1; i <= Number; i++)
            {
                FactorialResult *= i;
            }
            Message = "Успешно рассчитано.";
        }

        // Перегруженный виртуальный метод для вычисления факториала с передачей числа
        public virtual int CalculateFactorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Факториал для отрицательных чисел не определен.");
            }

            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }

        // Перегруженный метод для вычисления факториала с параметром verbose
        public int CalculateFactorial(int number, bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine($"Calculating factorial for {number}...");
            }
            return CalculateFactorial(number);
        }
    }
}
