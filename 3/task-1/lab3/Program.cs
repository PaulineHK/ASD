using System;

namespace lab3
{
    class Program
    {
        static int[] find(int[] array, int start, int end)
        {
            if (start == end)
                return new int[] { start, end, array[start] };

            int middle = (start + end) / 2;

            int[] left = find(array, start, middle);
            int[] rigth = find(array, middle + 1, end);
            int[] centre = find_centre(array, start, middle, end);

            if (left[2] >= rigth[2] && left[2] >= centre[2])
                return left;
            else if (rigth[2] >= left[2] && rigth[2] >= centre[2])
                return rigth;
            else return centre;
        }

        static int[] find_centre(int[] array, int start, int middle, int end)
        {
            int left = 0, left_sum = 0, rigth = 0, rigth_sum = 0, sum = 0, i;

            for (i = middle; i >= left; i--)
            {
                sum += array[i];
                if (sum > left_sum)
                {
                    left_sum = sum;
                    left = i;
                }
            }
            sum = 0;
            for (i = middle + 1; i <= end; i++)
            {
                sum += array[i];
                if (sum > rigth_sum)
                {
                    rigth_sum = sum;
                    rigth = i;
                }
            }
            return new int[] { left, rigth, left_sum + rigth_sum };
        }

        static void Main(string[] args)
        {
            int n = 17, i;
            int[] price = new int[n];
            Console.WriteLine("Использовать заданное количество дней и цен акций?" +
                "\n1)Да\n2)Нет");
            do
            {
                Int32.TryParse(Console.ReadLine(), out n);
            } while (n != 1 && n != 2);

            if (n == 2)
            {
                Console.Write("Количество дней: ");
                while (!Int32.TryParse(Console.ReadLine(), out n)) ;
                price = new int[n];
                for (i = 0; i < price.Length; i++)
                {
                    Console.Write("day " + i + ": ");
                    while (!Int32.TryParse(Console.ReadLine(), out price[i])) ;
                }
            }
            else
            {
                int[] price1 = { 100, 113, 110, 85, 105, 102, 86, 63, 81, 101, 94, 106, 101, 79, 94, 90, 97 };
                for (i = 0; i < price.Length; i++)
                    price[i] = price1[i];
            }
            Console.Write("Цена: ");
            for (i = 0; i < price.Length; i++)
                Console.Write(price[i] + " ");
            Console.WriteLine("");

            int[] change = new int[price.Length - 1];

            for (i = 1; i < price.Length; i++)
                change[i - 1] = price[i] - price[i - 1];
            Console.Write("Изменение: ");
            for (i = 0; i < change.Length; i++)
                Console.Write(change[i] + " ");
            Console.WriteLine("");
            int start = 0, end = change.Length - 1;
            int[] result = find(change, start, end);
            Console.WriteLine("Купить на " + result[0] + " день. " +
                "Продать на " + (result[1] + 1) + " день." +
                "\nПрибыль: " + result[2]);
            Console.ReadKey();
        }
    }
}
