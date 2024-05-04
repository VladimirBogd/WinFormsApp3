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
            // Проверяем, непустые ли поля ввода
            if (textBoxFirst.Text.Equals("") && textBoxSecond.Text.Equals(""))
            {
                textBoxFirst.BackColor = Color.Red;
                textBoxSecond.BackColor = Color.Red;
                textBoxResult.Text = "Поля ввода пустые";
                return;
            }
            if (textBoxFirst.Text.Equals(""))
            {
                textBoxFirst.BackColor = Color.Red;
                textBoxResult.Text = "Первое поле ввода пустое";
                return;
            }
            if (textBoxSecond.Text.Equals(""))
            {
                textBoxSecond.BackColor = Color.Red;
                textBoxResult.Text = "Второе поле ввода пустое";
                return;
            }
            // Проверяем, выбрана ли операция
            if (comboBoxOperation.SelectedIndex == -1)
            {
                textBoxResult.Text = "Не выбрана операция";
                return;
            }
            // Возвращаем изначальный цвет
            textBoxFirst.BackColor = Color.White;
            textBoxSecond.BackColor = Color.White;
            // Удаление лишних пробелов
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
            // Заводим переменные для множеств
            var firstSet = new SetOfIntegers(ParseInputToSet(textBoxFirst.Text));
            var secondSet = new SetOfIntegers(ParseInputToSet(textBoxSecond.Text));
            // Определяем результирующий набор в зависимости от выбранной операции
            var resultSet = new SetOfIntegers(new List<int>());
            switch (comboBoxOperation.SelectedItem)
            {
                case "Объединение":
                    if (secondSet.elements.Count > 1 && firstSet.elements.Count > 1)
                        resultSet = firstSet + secondSet;
                    else
                    {
                        MessageBox.Show("Введите множества а не отдельные элементы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "Пересечение":
                    if (secondSet.elements.Count > 1 && firstSet.elements.Count > 1)
                        resultSet = firstSet & secondSet;
                    else
                    {
                        MessageBox.Show("Введите множества а не отдельные элементы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "Разность":
                    if (secondSet.elements.Count > 1 && firstSet.elements.Count > 1)
                        resultSet = firstSet - secondSet;
                    else
                    {
                        MessageBox.Show("Введите множества а не отдельные элементы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "Добавить элемент":
                    if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        resultSet = firstSet + secondSet;
                    else if (secondSet.elements.Count > 1 && firstSet.elements.Count == 1)
                        resultSet = secondSet + firstSet;
                    else
                    {
                        MessageBox.Show("Один элемент должен добавляться к множеству", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case "Удалить элемент":
                    if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        resultSet = firstSet - secondSet;
                    else
                    {
                        MessageBox.Show("Один элемент должен удаляться из множества", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                default:
                    textBoxResult.Text = "";
                    return;
            }
            // Выводим результирующий набор
            textBoxResult.Text = resultSet.PrintSet();
        }

        private List<int> ParseInputToSet(string input)
        {
            // Заводим переменную для текущего множества
            var set = new List<int>();
            var numbers = input.Split(' ');
            // Преобразуем значения из массива строк в целые числа и добавляем их в List<int>
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
                        MessageBox.Show($"Дублирующиеся значения: {numberString}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return new List<int>();
                    }
                }
                else
                {
                    if (input == textBoxFirst.Text)
                        textBoxFirst.BackColor = Color.Red;
                    else
                        textBoxSecond.BackColor = Color.Red;
                    MessageBox.Show($"Некорректный ввод: {numberString}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<int>();
                }
            }
            // Возвращаем получившийся List<int>
            return set;
        }
    }
}
