using System;

namespace LabDoubleLinkedList
{
    // Клас вузла
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }

    // Клас двозв'язного списку
    public class DoublyLinkedList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; } // Додано Tail для зручності
        public int Count { get; private set; }

        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        // Додавання в кінець списку
        public void PushBack(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
            Count++;
        }

        // Додавання на початок списку
        public void PushFront(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Prev = newNode;
                Head = newNode;
            }
            Count++;
        }

        // Вставка перед заданим вузлом
        public void AddBefore(Node<T> node, T data)
        {
            if (node == null) return;
            if (node == Head)
            {
                PushFront(data);
                return;
            }
            Node<T> newNode = new Node<T>(data);
            newNode.Next = node;
            newNode.Prev = node.Prev;
            node.Prev.Next = newNode;
            node.Prev = newNode;
            Count++;
        }

        // [ОНОВЛЕНО] Пошук за ключем
        // Метод приймає значення (ключ) і повертає перший знайдений вузол, або null
        public Node<T> Find(T value)
        {
            Node<T> current = Head;
            while (current != null)
            {
                // Використовуємо Default.Equals для коректного порівняння узагальнених типів
                if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(current.Data, value))
                {
                    return current; // Знайдено
                }
                current = current.Next;
            }
            return null; // Не знайдено
        }

        // Видалення конкретного вузла
        public void RemoveNode(Node<T> node)
        {
            if (node == null || Head == null) return;

            // Якщо видаляємо голову
            if (node == Head)
            {
                Head = node.Next;
                if (Head != null) Head.Prev = null;
                else Tail = null; // Список став порожнім
            }
            // Якщо видаляємо хвіст
            else if (node == Tail)
            {
                Tail = node.Prev;
                if (Tail != null) Tail.Next = null;
                else Head = null; // Список став порожнім
            }
            // Якщо видаляємо посередині
            else
            {
                node.Prev.Next = node.Next;
                if (node.Next != null)
                {
                    node.Next.Prev = node.Prev;
                }
            }
            Count--;
        }

        //  Загальний метод вилучення елемента зі списку за значенням
        // Видаляє перше входження елемента. Повертає true, якщо успішно.
        public bool Remove(T value)
        {
            Node<T> nodeToRemove = Find(value);
            if (nodeToRemove != null)
            {
                RemoveNode(nodeToRemove);
                return true;
            }
            return false;
        }

        // Друк списку
        public void Print()
        {
            Node<T> current = Head;
            if (current == null)
            {
                Console.WriteLine("Список порожній");
                return;
            }
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DoublyLinkedList<int> intList = new DoublyLinkedList<int>();
            DoublyLinkedList<double> doubleList = new DoublyLinkedList<double>();

            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ (Варіант 4) ===");
                Console.WriteLine("--- Цілі числа ---");
                Console.WriteLine("1. Додати елемент в кінець");
                Console.WriteLine("2. Додати елемент на початок");
                Console.WriteLine("3. Вивести список");
                Console.WriteLine("4. ПОШУК елемента за ключем ");
                Console.WriteLine("5. ВИДАЛЕННЯ елемента за ключем ");
                Console.WriteLine("6. Виконати Завдання 1.4 (Вставка 25 перед дод., видалення відємн.)");

                Console.WriteLine("--- Дійсні числа ---");
                Console.WriteLine("7. Додати елемент");
                Console.WriteLine("8. Вивести список");
                Console.WriteLine("9. Виконати Завдання 2.4 (Середнє <=15, видалення >25)");

                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть ціле число: ");
                        if (int.TryParse(Console.ReadLine(), out int intValBack))
                        {
                            intList.PushBack(intValBack);
                            Console.WriteLine("Елемент додано.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "2":
                        Console.Write("Введіть ціле число: ");
                        if (int.TryParse(Console.ReadLine(), out int intValFront))
                        {
                            intList.PushFront(intValFront);
                            Console.WriteLine("Елемент додано.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "3":
                        Console.Write("Список цілих чисел: ");
                        intList.Print();
                        break;

                    case "4": // Кейс для демонстрації пошуку
                        Console.Write("Введіть ціле число для пошуку (ключ): ");
                        if (int.TryParse(Console.ReadLine(), out int searchVal))
                        {
                            var foundNode = intList.Find(searchVal);
                            if (foundNode != null)
                                Console.WriteLine($"Елемент {searchVal} знайдено у списку.");
                            else
                                Console.WriteLine($"Елемент {searchVal} НЕ знайдено.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "5": //  Кейс для демонстрації загального видалення
                        Console.Write("Введіть ціле число для видалення (ключ): ");
                        if (int.TryParse(Console.ReadLine(), out int removeVal))
                        {
                            if (intList.Remove(removeVal))
                                Console.WriteLine($"Елемент {removeVal} успішно видалено.");
                            else
                                Console.WriteLine($"Елемент {removeVal} не знайдено для видалення.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "6":
                        // Логіка Завдання 1.4 
                        Node<int> currentInt = intList.Head;
                        while (currentInt != null)
                        {
                            if (currentInt.Data > 0)
                            {
                                intList.AddBefore(currentInt, 25);
                            }
                            currentInt = currentInt.Next;
                        }

                        currentInt = intList.Head;
                        while (currentInt != null)
                        {
                            Node<int> nextNode = currentInt.Next;
                            if (currentInt.Data < 0)
                            {
                                intList.RemoveNode(currentInt);
                            }
                            currentInt = nextNode;
                        }
                        Console.WriteLine("Завдання 1.4 виконано. Виведіть список для перевірки.");
                        break;

                    case "7":
                        Console.Write("Введіть дійсне число (через кому): ");
                        if (double.TryParse(Console.ReadLine(), out double doubleVal))
                        {
                            doubleList.PushBack(doubleVal);
                            Console.WriteLine("Елемент додано.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "8":
                        Console.Write("Список дійсних чисел: ");
                        doubleList.Print();
                        break;

                    case "9":
                        // Логіка Завдання 2.4 
                        Node<double> currentDouble = doubleList.Head;
                        double sum = 0;
                        int count = 0;
                        while (currentDouble != null)
                        {
                            if (currentDouble.Data <= 15)
                            {
                                sum += currentDouble.Data;
                                count++;
                            }
                            currentDouble = currentDouble.Next;
                        }

                        if (count > 0)
                        {
                            double average = sum / count;
                            Console.WriteLine($"Середнє значення елементів <=15: {average:F2}");
                        }
                        else
                        {
                            Console.WriteLine("Елементів <=15 немає.");
                        }

                        currentDouble = doubleList.Head;
                        while (currentDouble != null)
                        {
                            Node<double> nextNode = currentDouble.Next;
                            if (currentDouble.Data > 25)
                            {
                                doubleList.RemoveNode(currentDouble);
                            }
                            currentDouble = nextNode;
                        }
                        Console.WriteLine("Завдання 2.4 виконано (елементи >25 видалено).");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірна команда. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}