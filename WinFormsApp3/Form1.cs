using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void onValueChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private SetOfIntegers ProcessInputNumbers(int choice)
        {
            List<int> numbersList = new();
            string input;
            if (choice == 1)
            {
                input = textBoxFirst.Text;
            }
            else
            {
                input = textBoxSecond.Text;
            }

            // ��������� ������ �� ������ ����� �� ��������
            string[] numberStrings = input.Split(' ');

            // �����������, ��� �� ����� "������" ��� ��������� �����
            bool spacePressed = false;
            bool focusChanged = false;

            // ��������� ������ ������ �� ���������� � ��������� �������� ����� � ������
            foreach (string numberString in numberStrings)
            {
                if (string.IsNullOrWhiteSpace(numberString))
                {
                    continue;
                }

                // ���������, ��� �� ����� "������" ��� ��������� �����
                if (spacePressed || focusChanged)
                {
                    if (numberString == " ")
                    {
                        // ��������� ���� ���� �������� ������
                        continue;
                    }
                }

                if (int.TryParse(numberString, out int number))
                {
                    if (numbersList.Contains(number))
                    {
                        // ���������� ��������� �� ������ � ���������� ������ �����
                        MessageBox.Show("������������ ��������: " + numberString + " (������������� �����)");
                        return new SetOfIntegers();
                    }
                    numbersList.Add(number);
                }
                else
                {
                    // ���������� ��������� �� ������ � ���������� ������ �����
                    MessageBox.Show("������������ ��������: " + numberString);
                    return new SetOfIntegers();
                }

                // ���������, ��� �� ����� "������"
                if (numberString == " ")
                {
                    spacePressed = true;
                }
            }

            // ������� � ���������� ����� ����� �����
            SetOfIntegers setOfIntegers = new SetOfIntegers(numbersList);
            return setOfIntegers;
        }



        private void Calculate()
        {
            // �������� ������� ������ ����� �����
            SetOfIntegers firstSet = ProcessInputNumbers(1);
            SetOfIntegers secondSet = ProcessInputNumbers(2);

            if (firstSet.elements.Count == 0 && secondSet.elements.Count == 0)
            {
                textBoxResult.Text = "���� ����� ������";
            }
            else if (firstSet.elements.Count == 0 && secondSet.elements.Count != 0)
            {
                textBoxResult.Text = "������ ���� ����� ������";
            }
            else if (firstSet.elements.Count != 0 && secondSet.elements.Count == 0)
            {
                textBoxResult.Text = "������ ���� ����� ������";
            }
            else
            {
                textBoxResult.Text = "";
                // ���������, ������� �� ��������
                if (comboBoxOperation.Text.Equals(""))
                {
                    textBoxResult.Text = "";
                    return;
                }

                // ���������� �������������� ����� � ����������� �� ��������� ��������
                SetOfIntegers resultSet;
                switch (comboBoxOperation.Text)
                {
                    case "�����������":
                        resultSet = firstSet + secondSet;
                        break;
                    case "�����������":
                        resultSet = firstSet & secondSet;
                        break;
                    case "��������":
                        resultSet = firstSet - secondSet;
                        break;
                    case "�������� �������":
                        if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        {
                            resultSet = firstSet + secondSet;
                        }
                        else if (secondSet.elements.Count > 1 && firstSet.elements.Count == 1)
                        {
                            resultSet = secondSet + firstSet;
                        }
                        else
                        {
                            textBoxResult.Text = "������: ���� ������� ������ ����������� � ���������";
                            textBoxFirst.BackColor = Color.Red;
                            textBoxSecond.BackColor = Color.Red;
                            return;
                        }
                        break;
                    case "������� �������":
                        if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        {
                            resultSet = firstSet - secondSet;
                        }
                        else
                        {
                            textBoxResult.Text = "������: ���� ������� ������ ��������� �� ���������";
                            textBoxFirst.BackColor = Color.Red;
                            textBoxSecond.BackColor = Color.Red;
                            return;
                        }
                        break;
                    default:
                        textBoxResult.Text = "";
                        return;
                }

                // ������� �������������� �����
                textBoxResult.Text = resultSet.PrintSet();

                // ���������� ����� ������� ������� ��������� ����� �����
                textBoxFirst.BackColor = Color.White;
                textBoxSecond.BackColor = Color.White;
            }
        }

        private void textBoxFirst_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}