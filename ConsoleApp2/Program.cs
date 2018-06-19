using System;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static Random rand = new Random();

        static ListNode AddNode(ListNode prev)
        {
            ListNode result = new ListNode();
            result.Prev = prev;
            result.Next = null;
            result.Data = rand.Next(0, 100).ToString();
            prev.Next = result;

            return result;
        }

        static void DisplayWrite(ListRand first)
        {
            ListNode temp = first.Head;

            while (temp != null)
            {
                Console.WriteLine("[" + temp.Data.ToString() + " => Rand Data:" + temp.Rand.Data.ToString() + "]");
                temp = temp.Next;
            }
        }

        static int СountElement()
        {
            Console.Clear();
            Console.WriteLine("Введите колличество элементов:");
            int count;
            bool result = Int32.TryParse(Console.ReadLine(), out count);

            if (result)
            {
                return count;
            }
            else
            {
                return СountElement();
            }
        }

        static void Main(string[] args)
        {
            ListNode head = new ListNode();
            ListNode tail = new ListNode();
            ListNode temp = new ListNode();
            ListRand first = new ListRand();
            int length = СountElement();
            ListNode[] List = new ListNode[length];

            head.Data = rand.Next(0, 1000).ToString();
            tail = head;
            List[0] = head;

            for (int i = 1; i < length; i++)
            {
                tail = AddNode(tail);
                List[i] = tail;
            }

            temp = head;

            for (int i = 0; i < length; i++)
            {
                int index = rand.Next(0, length - 1);
                temp.Rand = List[index];
                temp = temp.Next;
            }

            first.Head = head;
            first.Tail = tail;
            first.Count = length;

            Console.WriteLine("Вывод созданных данных:");
            DisplayWrite(first);
          
            FileStream fs = new FileStream("data.dat", FileMode.Create);
            first.Serialize(fs);

            Console.ReadLine();

            ListRand second = new ListRand();

            try
            {
                fs = new FileStream("data.dat", FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }

            second.Deserialize(fs);

            if (second.Tail.Data == first.Tail.Data)
            {
                Console.WriteLine("Вывод Загруженых данных данных:");
                DisplayWrite(second);
                Console.WriteLine("Success");
            }

            Console.Read();
        }
    }
}
