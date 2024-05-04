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

        private void textBoxFirst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBoxSecond.Select();
        }
        private void textBoxSecond_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonResult.Select();
        }
        private void buttonResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxFirst.Select();
                Calculate();
            }
        }
        private void buttonResult_Click(object sender, EventArgs e)
        {
            Calculate();
        }


        private void Calculate()
        {
            // ���������, �������� �� ���� �����
            if (textBoxFirst.Text.Equals("") && textBoxSecond.Text.Equals(""))
            {
                textBoxFirst.BackColor = Color.Red;
                textBoxSecond.BackColor = Color.Red;
                textBoxResult.Text = "���� ����� ������";
                return;
            }
            if (textBoxFirst.Text.Equals(""))
            {
                textBoxFirst.BackColor = Color.Red;
                textBoxResult.Text = "������ ���� ����� ������";
                return;
            }
            if (textBoxSecond.Text.Equals(""))
            {
                textBoxSecond.BackColor = Color.Red;
                textBoxResult.Text = "������ ���� ����� ������";
                return;
            }
            // ���������, ������� �� ��������
            if (comboBoxOperation.SelectedIndex == -1)
            {
                textBoxResult.Text = "�� ������� ��������";
                return;
            }
            // ���������� ����������� ����
            textBoxFirst.BackColor = Color.White;
            textBoxSecond.BackColor = Color.White;
            // �������� ������ ��������
            textBoxFirst.Text = textBoxFirst.Text.Trim();
            while (textBoxFirst.Text.Contains("  "))
            {
                textBoxFirst.Text = textBoxFirst.Text.Replace("  ", " ");
            }
            textBoxSecond.Text = textBoxSecond.Text.Trim();
            while (textBoxSecond.Text.Contains("  "))
            {
                textBoxSecond.Text = textBoxSecond.Text.Replace("  ", " ");
            }
            // ������� ���������� ��� ��������
            var firstSet = new SetOfIntegers(ParseInputToSet(textBoxFirst.Text));
            var secondSet = new SetOfIntegers(ParseInputToSet(textBoxSecond.Text));
            // ���������� �������������� ����� � ����������� �� ��������� ��������
            var resultSet = new SetOfIntegers(new List<int>());
            switch (comboBoxOperation.SelectedItem)
            {
                case "�����������":
                    if (secondSet.elements.Count > 1 && firstSet.elements.Count > 1)
                        resultSet = firstSet + secondSet;
                    else
                    {
                        MessageBox.Show("������� ��������� � �� ��������� ��������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "�����������":
                    if (secondSet.elements.Count > 1 && firstSet.elements.Count > 1)
                        resultSet = firstSet & secondSet;
                    else
                    {
                        MessageBox.Show("������� ��������� � �� ��������� ��������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "��������":
                    if (secondSet.elements.Count > 1 && firstSet.elements.Count > 1)
                        resultSet = firstSet - secondSet;
                    else
                    {
                        MessageBox.Show("������� ��������� � �� ��������� ��������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "�������� �������":
                    if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        resultSet = firstSet + secondSet;
                    else if (secondSet.elements.Count > 1 && firstSet.elements.Count == 1)
                        resultSet = secondSet + firstSet;
                    else
                    {
                        MessageBox.Show("���� ������� ������ ����������� � ���������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "������� �������":
                    if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        resultSet = firstSet - secondSet;
                    else
                    {
                        MessageBox.Show("���� ������� ������ ��������� �� ���������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                default:
                    textBoxResult.Text = "";
                    return;
            }
            // ������� �������������� �����
            textBoxResult.Text = resultSet.PrintSet();
        }

        private List<int> ParseInputToSet(string input)
        {
            // ������� ���������� ��� �������� ���������
            var set = new List<int>();
            var numbers = input.Split(' ');
            // ����������� �������� �� ������� ����� � ����� ����� � ��������� �� � List<int>
            foreach (string numberString in numbers)
            {
                if (int.TryParse(numberString, out int number))
                {
                    if (!set.Contains(number))
                    {
                        set.Add(number);
                    }
                    else
                    {
                        if (input == textBoxFirst.Text)
                            textBoxFirst.BackColor = Color.Red;
                        else
                            textBoxSecond.BackColor = Color.Red;
                        MessageBox.Show($"������������� ��������: {numberString}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return new List<int>();
                    }
                }
                else
                {
                    if (input == textBoxFirst.Text)
                        textBoxFirst.BackColor = Color.Red;
                    else
                        textBoxSecond.BackColor = Color.Red;
                    MessageBox.Show($"������������ ����: {numberString}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<int>();
                }
            }
            // ���������� ������������ List<int>
            return set;
        }
    }
}
