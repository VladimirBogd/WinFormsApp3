﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WinFormsApp3
{
    public class SetOfIntegers
    {
        public List<int> elements;
        public SetOfIntegers(List<int> elements)
        {
            this.elements = elements;
        }
        // Преобразование элементов типа SetOfIntegers в строку
        public string PrintSet()
        {
            return string.Join(" ", this.elements);
        }
        // Перегрузка оператора "+" для выполнения операции объединения множеств (union)
        public static SetOfIntegers operator +(SetOfIntegers oneSet, SetOfIntegers otherSet)
        {
            List<int> resultSet = oneSet.elements;

            // Добавляем элементы из второго множества, если они отсутствуют в первом
            foreach (int element in otherSet.elements)
            {
                if (!resultSet.Contains(element))
                {
                    resultSet.Add(element);
                }
            }
            return new SetOfIntegers(resultSet);
        }
        // Перегрузка оператора "&" для выполнения операции пересечения множеств (intersection)
        public static SetOfIntegers operator &(SetOfIntegers oneSet, SetOfIntegers otherSet)
        {
            List<int> resultSet = new List<int>();
            foreach (int element in oneSet.elements)
            {
                if (otherSet.elements.Contains(element))
                {
                    resultSet.Add(element);
                }
            }
            return new SetOfIntegers(resultSet);
        }
        // Перегрузка оператора "-" для выполнения операции разности множеств (difference)
        public static SetOfIntegers operator -(SetOfIntegers oneSet, SetOfIntegers otherSet)
        {
            List<int> resultSet = new List<int>();
            foreach (int element in oneSet.elements)
            {
                if (!otherSet.elements.Contains(element))
                {
                    resultSet.Add(element);
                }
            }
            return new SetOfIntegers(resultSet);
        }
    }
}
