using System;

namespace Lab4_DoubleLinkedList
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
        public int Count { get; private set; }

        public void PushBack(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (Head == null) Head = newNode;
            else
            {
                Node<T> current = Head;
                while (current.Next != null) current = current.Next;
                current.Next = newNode;
                newNode.Prev = current;
            }
            Count++;
        }

        public void PushFront(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (Head != null)
            {
                newNode.Next = Head;
                Head.Prev = newNode;
            }
            Head = newNode;
            Count++;
        }

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

        public void RemoveNode(Node<T> node)
        {
            if (node == null || Head == null) return;
            if (node == Head)
            {
                Head = node.Next;
                if (Head != null) Head.Prev = null;
            }
            else
            {
                node.Prev.Next = node.Next;
                if (node.Next != null) node.Next.Prev = node.Prev;
            }
            Count--;
        }

        public void Print()
        {
            Node<T> current = Head;
            if (current == null)
            {
                Console.WriteLine("Список порожній.");
                return;
            }
            while (current != null)
            {
                Console.Write(current.Data + "  ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    // Головна програма з меню
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
                Console.WriteLine("1. Додати елемент до списку (цілі числа)");
                Console.WriteLine("2. Вивести список (цілі числа)");
                Console.WriteLine("3. Виконати Завдання 1 (Вставити 25 перед додатними, видалити негативні)");
                Console.WriteLine("4. Додати елемент до списку (дійсні числа)");
                Console.WriteLine("5. Вивести список (дійсні числа)");
                Console.WriteLine("6. Виконати Завдання 2 (Середнє <= 15, видалити > 25)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть ціле число: ");
                        if (int.TryParse(Console.ReadLine(), out int intVal))
                        {
                            intList.PushBack(intVal);
                            Console.WriteLine("Елемент додано.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "2":
                        Console.Write("Список цілих чисел: ");
                        intList.Print();
                        break;

                    case "3":
                        Node<int> currentInt = intList.Head;
                        while (currentInt != null)
                        {
                            if (currentInt.Data > 0)
                                intList.AddBefore(currentInt, 25);
                            currentInt = currentInt.Next;
                        }
                        currentInt = intList.Head;
                        while (currentInt != null)
                        {
                            Node<int> nextNode = currentInt.Next;
                            if (currentInt.Data < 0)
                                intList.RemoveNode(currentInt);
                            currentInt = nextNode;
                        }
                        Console.WriteLine("Завдання 1 виконано! Виведіть список (пункт 2), щоб перевірити результат.");
                        break;

                    case "4":
                        Console.Write("Введіть дійсне число (через кому, наприклад 10,5): ");
                        if (double.TryParse(Console.ReadLine(), out double doubleVal))
                        {
                            doubleList.PushBack(doubleVal);
                            Console.WriteLine("Елемент додано.");
                        }
                        else Console.WriteLine("Помилка вводу.");
                        break;

                    case "5":
                        Console.Write("Список дійсних чисел: ");
                        doubleList.Print();
                        break;

                    case "6":
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
                        double average = count > 0 ? sum / count : 0;
                        Console.WriteLine($"Середнє значення елементів <= 15: {average:F2}");

                        currentDouble = doubleList.Head;
                        while (currentDouble != null)
                        {
                            Node<double> nextNode = currentDouble.Next;
                            if (currentDouble.Data > 25)
                                doubleList.RemoveNode(currentDouble);
                            currentDouble = nextNode;
                        }
                        Console.WriteLine("Завдання 2 виконано (елементи > 25 видалено)!");
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