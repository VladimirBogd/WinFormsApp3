using System;
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
            if (elements == null || elements.Contains(0))
            {
                this.elements = new List<int>();
            }
            else
            {
                this.elements = elements;
            }
        }
        public SetOfIntegers()
        {
            this.elements = new List<int>();
        }

        public string PrintSet()
        {
            return string.Join(" ", this.elements);
        }

        // Перегрузка оператора "+" для выполнения операции объединения множеств (union)
        public static SetOfIntegers operator +(SetOfIntegers oneSet, SetOfIntegers otherSet)
        {
            // Создаем новый список, чтобы избежать изменения исходных множеств
            List<int> resultSet = new List<int>(oneSet.elements);
            // Добавляем элементы из второго множества, если они отсутствуют в первом
            foreach (int element in otherSet.elements)
            {
                if (!resultSet.Contains(element))
                {
                    resultSet.Add(element);
                }
            }
            // Сортируем результирующий список для удобства
            resultSet.Sort();
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
            // Сортируем результирующий список для удобства
            resultSet.Sort();
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
            // Сортируем результирующий список для удобства
            resultSet.Sort();
            return new SetOfIntegers(resultSet);
        }
    }
}
