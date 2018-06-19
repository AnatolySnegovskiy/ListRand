using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; // произвольный элемент внутри списка
        public string Data;
    }

    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            Dictionary<ListNode, int> DictionaryListRand = new Dictionary<ListNode, int>();
            ListNode temp = new ListNode();
            int i = 0;
            temp = Head;

            do
            {
                DictionaryListRand.Add(temp, i);
                temp = temp.Next;
                i++;
            } while (temp != null);

            using (StreamWriter stringWrite = new StreamWriter(s))
            {
                temp = Head;
                stringWrite.WriteLine(Count);

                do
                {
                    stringWrite.WriteLine(temp.Data.ToString());
                    stringWrite.WriteLine(DictionaryListRand[temp.Rand].ToString());
                    temp = temp.Next;
                } while (temp != null);
            }
        }

        public void Deserialize(FileStream s)
        {
            Dictionary<ListNode, int> DictionaryListRand = new Dictionary<ListNode, int>();
            ListNode[] List = null;
            ListNode temp = new ListNode();
            ListNode tempRandom = new ListNode();

            Count = 0;
            Head = temp;

            using (StreamReader stringRead = new StreamReader(s))
            {
                string line;
                int CountItems = 0;
                int j = 0;

                while (!string.IsNullOrWhiteSpace(line = stringRead.ReadLine()))
                {
                    if (List == null)
                    {
                        bool result = Int32.TryParse(line, out CountItems);

                        if (result)
                        {
                            List = new ListNode[CountItems];
                            continue;
                        }
                        else
                        {
                            throw new Exception("Неверный формат данных!!!");
                        }
                    }

                    if (j % 2 == 0)
                    {
                        temp.Data = line;
                        tempRandom = temp;
                        ListNode next = new ListNode();
                        temp.Next = next;
                        List[Count] = temp;
                        next.Prev = temp;
                        temp = next;
                        Count++;
                    }
                    else
                    {
                        int indexList;
                        bool result = Int32.TryParse(line, out indexList);

                        if (result)
                        {
                            DictionaryListRand.Add(temp.Prev, indexList);
                        }
                    }
                    j++;
                }
            }

            Tail = temp.Prev;
            Tail.Next = null;

            for (int i = 0; i < List.Length; i++)
            {
                List[i].Rand = List[DictionaryListRand[List[i]]];
            }
        }
    }
}
