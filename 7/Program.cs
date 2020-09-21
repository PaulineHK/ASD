using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace lab7
{
    class Program
    {
        static long[] hash_table = new long[10000019];
        const int c = 72;

        static int hash_function(int data)
        {
            return data % hash_table.Length;
        }
        static void add_data(int data)
        {
            int address = hash_function(data);
            int temp = address;
            int i = 0;

            while (true)
            {
                if (hash_table[temp] == 0)
                {
                    hash_table[temp] = data;
                    break;
                }
                i++;
                temp = address + c * i;
            }
        }
        static int find(int data)
        {
            int address = hash_function(data);
            int temp = address;
            int i = 0;
            while (true)
            {              
                    if (hash_table[temp] == data)
                        return temp;
                i++;
                temp = address + c * i;
                if (temp > hash_table.Length) return -1;
            }
        }
        static void Main(string[] args)
        {
            int n = 0, i = 0;
            Stopwatch time = new Stopwatch();

            Console.Write("Введите количество элементов: ");
            while (!Int32.TryParse(Console.ReadLine(), out n)) ;
            while (n > hash_table.Length)
            {
                Console.WriteLine("Количесво элементов больше ячеек таблицы - 1000033");
                Console.Write("Введите количество элементов: ");
                while (!Int32.TryParse(Console.ReadLine(), out n)) ;
            }
            HashSet<int> array = new HashSet<int>();
            Random rnd = new Random();
            while (i < n)
            {
                if (array.Add(rnd.Next(1, 1100000)))
                    i++;
            }
            IEnumerator<int> iterator = array.GetEnumerator();
            time.Start();
            for (i = 0; i < 100; i++)
            {
                iterator.MoveNext();
                add_data(iterator.Current);
            }
            time.Stop();
            Console.WriteLine("Время добавляения 100 первых элементов: "
                + time.ElapsedMilliseconds + " миллисекунд");

            while (i < array.Count - 100)
            {
                iterator.MoveNext();
                add_data(iterator.Current);
                i++;
            }
            time.Reset();
            time.Start();
            for (i = n - 100; i < array.Count; i++)
            {
                iterator.MoveNext();
                add_data(iterator.Current);
            }
            time.Stop();
            Console.WriteLine("Время добавляения 100 последних элементов: "
                + time.ElapsedMilliseconds + " миллисекунд");

            iterator.Reset();
            int data;
            time.Reset();
            while (true)
            {
                data = rnd.Next(1, 10000000);
                if (array.Contains(data))
                {
                    time.Start();
                    data = find(data);
                    time.Stop();
                    break;
                }
            }
            Console.WriteLine("Время поиска элемента: "
                + time.ElapsedMilliseconds + " миллисекунд");
            time.Reset();

            Console.Write("Введите элемент: ");
            while (!Int32.TryParse(Console.ReadLine(), out data)) ;
            time.Start();
            data = find(data);
            time.Stop();

            if (data > -1) Console.WriteLine("Элемент найден");
            else Console.WriteLine("Такого элемента нет");
            Console.WriteLine("Время поиска элемента: "
                + time.ElapsedMilliseconds + " миллисекунд");

            Console.ReadKey();
        }
    }
}
