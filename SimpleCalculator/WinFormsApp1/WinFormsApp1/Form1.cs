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

        //���������� ������� ������� ������� ��� textBox1
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //������ �������� ���������
            if ((e.KeyChar >= '�' && e.KeyChar <= '�') || (e.KeyChar >= '�' && e.KeyChar <= '�'))
            {
                e.Handled = true;
                return;
            }
            //�������� ����� (������ ����� ���� ��������, ������, ����� �������� �� ������ �������� � ����������)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLower(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '+' && e.KeyChar != '/' && e.KeyChar != '*')
            {
                e.Handled = true;
            }

            int cursorPosition = textBox1.SelectionStart;

            //�������� ��� ������
            if (e.KeyChar == '-')
            {
                //���� ����� �������� � ������ ������ ��� ����� ��������� +, -, *, /, �� �� ��������
                if (cursorPosition == 0 || "+-*/".Contains(textBox1.Text[cursorPosition - 1]))
                {
                    //��������� ���� �������� ������ ������
                    if (cursorPosition > 0 && textBox1.Text[cursorPosition - 1] == '-')
                    {
                        e.Handled = true; //���������� �������� ������
                    }
                }
                else
                {
                    //��������� ���� ������ ����� �����
                    if (char.IsDigit(textBox1.Text[cursorPosition - 1]))
                    {
                        e.Handled = false; //��������� ���� ������
                    }
                    else
                    {
                        //���� ����� ����� �� ����� ��������� ��� �����, ��������� ���
                        e.Handled = true;
                    }
                }
            }

            //���������� ������ ����� ����� � ����� (��� ������������ �����)
            if (e.KeyChar == '.')
            {
                //���������, ���� ��� ���� ����� � ����� ��� ��� ����� ������
                if ((cursorPosition > 0 && textBox1.Text[cursorPosition - 1] == '.') ||
                    (cursorPosition > 0 && !char.IsDigit(textBox1.Text[cursorPosition - 1]) && textBox1.Text[cursorPosition - 1] != '-'))
                {
                    e.Handled = true; //���������� ������ ����� ��� ����� ����� �������, �� ����������� ������ ��� �������
                }
                else
                {
                    //���������, ��� ����� ������ ���� ���� �� ���� �����
                    bool foundDigit = false;
                    for (int i = cursorPosition - 1; i >= 0; i--)
                    {
                        if (char.IsDigit(textBox1.Text[i]))
                        {
                            foundDigit = true;
                            break;
                        }
                        //���� ���������� ��������, ������ ��� ����� �����, � ����� ���������
                        if ("+-*/".Contains(textBox1.Text[i])) break;
                    }
                    //���� �� ����� ����� �� ������� �������, ��������� ���� �����
                    if (!foundDigit) e.Handled = true;
                }
            }
        }

        //���������� ������� ������� ������ button1
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text; //�������� ������ �� textBox1 � textBox2
        }

        //����� ��� ���������� �������������� ��������
        private void PerformArithmeticOperation()
        {
            string input = textBox1.Text;
            string pattern = @"([-]?\d+(\.\d+)?)[\s]*([+\-*/])[\s]*([-]?\d+(\.\d+)?)"; //���������� ��������� ��� ������ ���� ����� � ����� ��������
            Match match = Regex.Match(input, pattern); //����� ���������� �� ����������� ���������

            if (match.Success)
            {
                try
                {
                    //���������� double � CultureInfo.InvariantCulture ��� ���������� ��������� ������������ �����
                    double num1 = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                    string operation = match.Groups[3].Value;
                    double num2 = double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);

                    double result = 0; //���������� ��� �������� ���������� 

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
                                textBox2.Text = "������: ������� �� 0!";
                                return;
                            }
                            break;
                    }
                    //����� ���������� � textBox2
                    textBox2.Text = Math.Round(result, 5).ToString(CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    textBox2.Text = "������ �����!";
                }
            }
            else
            {
                textBox2.Text = "�������� ������!";
            }
        }

        // ���������� ������� ���������� ������ � textBox1
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //��������, ���� �� ������ ������� Enter
            if (e.KeyCode == Keys.Enter)
            {
                PerformArithmeticOperation();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "������� �����������";
            this.MaximizeBox = true;
        }

        //���������� ������� ��������� ������� �����
        private void Form1_Resize(object sender, EventArgs e)
        {
            textBox1.Width = this.Width / 2;
            textBox2.Width = this.Width / 2;
            button1.Width = this.Width / 2;
            button1.Height = this.Height / 3;
        }
    }
}