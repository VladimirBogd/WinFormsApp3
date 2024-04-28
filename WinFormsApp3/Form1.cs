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

            // Разделяем строку на массив строк по пробелам
            string[] numberStrings = input.Split(' ');

            // Отслеживаем, был ли нажат "Пробел" или перемещен фокус
            bool spacePressed = false;
            bool focusChanged = false;

            // Проверяем каждую строку на валидность и добавляем валидные числа в список
            foreach (string numberString in numberStrings)
            {
                if (string.IsNullOrWhiteSpace(numberString))
                {
                    continue;
                }

                // Проверяем, был ли нажат "Пробел" или перемещен фокус
                if (spacePressed || focusChanged)
                {
                    if (numberString == " ")
                    {
                        // Блокируем ввод двух пробелов подряд
                        continue;
                    }
                }

                if (int.TryParse(numberString, out int number))
                {
                    if (numbersList.Contains(number))
                    {
                        // Отображаем сообщение об ошибке и возвращаем пустой набор
                        MessageBox.Show("Некорректное значение: " + numberString + " (Дублирующееся число)");
                        return new SetOfIntegers();
                    }
                    numbersList.Add(number);
                }
                else
                {
                    // Отображаем сообщение об ошибке и возвращаем пустой набор
                    MessageBox.Show("Некорректное значение: " + numberString);
                    return new SetOfIntegers();
                }

                // Проверяем, был ли нажат "Пробел"
                if (numberString == " ")
                {
                    spacePressed = true;
                }
            }

            // Создаем и возвращаем набор целых чисел
            SetOfIntegers setOfIntegers = new SetOfIntegers(numbersList);
            return setOfIntegers;
        }



        private void Calculate()
        {
            // Получаем входные наборы целых чисел
            SetOfIntegers firstSet = ProcessInputNumbers(1);
            SetOfIntegers secondSet = ProcessInputNumbers(2);

            if (firstSet.elements.Count == 0 && secondSet.elements.Count == 0)
            {
                textBoxResult.Text = "Поля ввода пустые";
            }
            else if (firstSet.elements.Count == 0 && secondSet.elements.Count != 0)
            {
                textBoxResult.Text = "Первое поле ввода пустое";
            }
            else if (firstSet.elements.Count != 0 && secondSet.elements.Count == 0)
            {
                textBoxResult.Text = "Второе поле ввода пустое";
            }
            else
            {
                textBoxResult.Text = "";
                // Проверяем, выбрана ли операция
                if (comboBoxOperation.Text.Equals(""))
                {
                    textBoxResult.Text = "";
                    return;
                }

                // Определяем результирующий набор в зависимости от выбранной операции
                SetOfIntegers resultSet;
                switch (comboBoxOperation.Text)
                {
                    case "Объединение":
                        resultSet = firstSet + secondSet;
                        break;
                    case "Пересечение":
                        resultSet = firstSet & secondSet;
                        break;
                    case "Разность":
                        resultSet = firstSet - secondSet;
                        break;
                    case "Добавить элемент":
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
                            textBoxResult.Text = "Ошибка: Один элемент должен добавляться к множеству";
                            textBoxFirst.BackColor = Color.Red;
                            textBoxSecond.BackColor = Color.Red;
                            return;
                        }
                        break;
                    case "Удалить элемент":
                        if (firstSet.elements.Count > 1 && secondSet.elements.Count == 1)
                        {
                            resultSet = firstSet - secondSet;
                        }
                        else
                        {
                            textBoxResult.Text = "Ошибка: Один элемент должен удаляться из множества";
                            textBoxFirst.BackColor = Color.Red;
                            textBoxSecond.BackColor = Color.Red;
                            return;
                        }
                        break;
                    default:
                        textBoxResult.Text = "";
                        return;
                }

                // Выводим результирующий набор
                textBoxResult.Text = resultSet.PrintSet();

                // Сбрасываем цвета фоновых заливок текстовых полей ввода
                textBoxFirst.BackColor = Color.White;
                textBoxSecond.BackColor = Color.White;
            }
        }

        private void textBoxFirst_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}