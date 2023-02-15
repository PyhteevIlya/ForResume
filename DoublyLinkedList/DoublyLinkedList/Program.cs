using System.Collections.Generic;
using System.Collections;

namespace DoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<string> linkedList = new DoublyLinkedList<string>();
            // добавление элементов
            linkedList.Add("Bob");
            linkedList.Add("Bill");
            linkedList.Add("Tom");
            linkedList.Add("Frank");
            linkedList.Add("Vlad");
            linkedList.Add("Cute");
            linkedList.AddFirst("Kate");
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
            // удаление
            linkedList.Remove("Bill");
            linkedList.Remove("Tom");
            linkedList.RemoveFirst();
            linkedList.RemoveEnd();
            // перебор с последнего элемента
            foreach (var t in linkedList.BackEnumerator())
            {
                Console.WriteLine(t);
            }
        }
    }
}