using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Обработчик события нажатия клавиши для textBox1
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Запрет символов кириллицы
            if ((e.KeyChar >= 'А' && e.KeyChar <= 'я') || (e.KeyChar >= 'Ё' && e.KeyChar <= 'ё'))
            {
                e.Handled = true;
                return;
            }
            //Проверка ввода (Запрет ввода всех символов, знаков, кроме латиницы на нижнем регистре и операторов)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLower(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '+' && e.KeyChar != '/' && e.KeyChar != '*')
            {
                e.Handled = true;
            }

            int cursorPosition = textBox1.SelectionStart;

            //Проверка для минуса
            if (e.KeyChar == '-')
            {
                //Если минус вводится в начале строки или после оператора +, -, *, /, то он разрешён
                if (cursorPosition == 0 || "+-*/".Contains(textBox1.Text[cursorPosition - 1]))
                {
                    //Запрещаем ввод двойного минуса подряд
                    if (cursorPosition > 0 && textBox1.Text[cursorPosition - 1] == '-')
                    {
                        e.Handled = true; //Блокировка двойного минуса
                    }
                }
                else
                {
                    //Разрешаем ввод минуса после цифры
                    if (char.IsDigit(textBox1.Text[cursorPosition - 1]))
                    {
                        e.Handled = false; //Разрешаем ввод минуса
                    }
                    else
                    {
                        //Если минус введён не после оператора или цифры, запрещаем его
                        e.Handled = true;
                    }
                }
            }

            //Разрешение только одной точки в числе (для вещественных чисел)
            if (e.KeyChar == '.')
            {
                //Проверяем, если уже есть точка в числе или две точки подряд
                if ((cursorPosition > 0 && textBox1.Text[cursorPosition - 1] == '.') ||
                    (cursorPosition > 0 && !char.IsDigit(textBox1.Text[cursorPosition - 1]) && textBox1.Text[cursorPosition - 1] != '-'))
                {
                    e.Handled = true; //Блокировка второй точки или точки после символа, не являющегося цифрой или минусом
                }
                else
                {
                    //Проверяем, что перед точкой есть хотя бы одна цифра
                    bool foundDigit = false;
                    for (int i = cursorPosition - 1; i >= 0; i--)
                    {
                        if (char.IsDigit(textBox1.Text[i]))
                        {
                            foundDigit = true;
                            break;
                        }
                        //Если встретился оператор, значит это новое число, и точка разрешена
                        if ("+-*/".Contains(textBox1.Text[i])) break;
                    }
                    //Если не нашли цифру до текущей позиции, запрещаем ввод точки
                    if (!foundDigit) e.Handled = true;
                }
            }
        }

        //Обработчик события нажатия кнопки button1
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text; //Передача текста из textBox1 в textBox2
        }

        //Метод для выполнения арифметических операций
        private void PerformArithmeticOperation()
        {
            string input = textBox1.Text;
            string pattern = @"([-]?\d+(\.\d+)?)[\s]*([+\-*/])[\s]*([-]?\d+(\.\d+)?)"; //Регулярное выражение для поиска двух чисел и знака операции
            Match match = Regex.Match(input, pattern); //Поиск совпадений по регулярному выражению

            if (match.Success)
            {
                try
                {
                    //Используем double и CultureInfo.InvariantCulture для правильной обработки вещественных чисел
                    double num1 = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                    string operation = match.Groups[3].Value;
                    double num2 = double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);

                    double result = 0; //Переменная для хранения результата 

                    switch (operation)
                    {
                        case "+":
                            result = num1 + num2;
                            break;
                        case "-":
                            result = num1 - num2;
                            break;
                        case "*":
                            result = num1 * num2;
                            break;
                        case "/":
                            if (num2 != 0)
                                result = (double)num1 / num2;
                            else
                            {
                                textBox2.Text = "Ошибка: деление на 0!";
                                return;
                            }
                            break;
                    }
                    //Вывод результата в textBox2
                    textBox2.Text = Math.Round(result, 5).ToString(CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    textBox2.Text = "Ошибка ввода!";
                }
            }
            else
            {
                textBox2.Text = "Неверный формат!";
            }
        }

        // Обработчик события отпускания клавиш в textBox1
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //Проверка, была ли нажата клавиша Enter
            if (e.KeyCode == Keys.Enter)
            {
                PerformArithmeticOperation();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Простой калькулятор";
            this.MaximizeBox = true;
        }

        //Обработчик события изменения размера формы
        private void Form1_Resize(object sender, EventArgs e)
        {
            textBox1.Width = this.Width / 2;
            textBox2.Width = this.Width / 2;
            button1.Width = this.Width / 2;
            button1.Height = this.Height / 3;
        }
    }
}