using System;
//Свойства проекта - Сборка - Разрешить небезопасный код

namespace lab9
{
    class Program
    {
        internal enum NodePosition
        {
            child,
            sibling,
            head
        }
        internal class Node
        {
            internal int key;
            internal int degree;
            internal Node parent;
            internal Node child;
            internal Node sibling;

            internal Node()
            {
                parent = null;
                child = null;
                sibling = null;
            }
            internal Node(int key)
            {
                this.key = key;
            }
            /*
            internal void print(string indent, NodePosition nodePosition, bool end_level, bool empty)
            {
                Console.Write(indent);
                if (end_level)
                {
                    Console.Write("└─");
                    indent += " ";
                }
                else
                {                                       
                    Console.Write("├─");
                    indent += "| ";
                }
                var stringValue = empty ? "-" : key.ToString();
                PrintValue(stringValue, nodePosition);

                if(!empty && (child != null || sibling != null))
                {
                    if (child != null)
                        child.print(indent, NodePosition.child, false, false);

                    if (sibling != null) {
                        if (sibling.sibling != null)
                            sibling.print(indent, NodePosition.sibling, false, false);
                        else sibling.print(indent, NodePosition.sibling, true, false);
                    }
                }

            }
            private void PrintValue(string value, NodePosition nodePosition)
            {
                switch (nodePosition)
                {
                    case NodePosition.child:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("Ch:");
                        print_value(value);
                        break;
                    case NodePosition.sibling:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Sib:");
                        print_value(value);
                        break;
                    case NodePosition.head:
                        Console.WriteLine(value);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            private void print_value(string value)
            {
                Console.ForegroundColor = (value == "-") ? ConsoleColor.Red : ConsoleColor.Gray;
                Console.WriteLine(value);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        */
            private void PrintValue(string value, NodePosition nodePosition)
            {
                switch (nodePosition)
                {
                    case NodePosition.child:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("Ch:");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(value);
                        break;
                    case NodePosition.sibling:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Sib:");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(value);                      
                        break;
                    case NodePosition.head:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("H:");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(value);                       
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }         
            internal string print(string indent, NodePosition nodePosition, bool end_level, bool empty)
            {
                if (parent == null)
                    nodePosition = NodePosition.head;
                Console.Write(indent);

                if (end_level)
                {
                    Console.Write("└─");
                    indent += " ";
                }
                else
                {
                    Console.Write("├─");
                    indent += "| ";
                }
                var stringValue = empty ? "-" : key.ToString();
                PrintValue(stringValue, nodePosition);

                if (!empty && (child != null || sibling != null))
                {
     
                    if (child != null)
                    {
                        if (sibling == null && parent == null)
                            indent += " ";

                        if (child.sibling != null)
                            indent=child.print(indent, NodePosition.child, false, false);
                        else indent=child.print(indent, NodePosition.child, true, false);
                    }

                    if (sibling != null)
                    {

                        if (parent == null)
                            indent = "";

                        if (sibling.sibling != null)
                            indent = sibling.print(indent, NodePosition.sibling, false, false);
                        else indent = sibling.print(indent, NodePosition.sibling, true, false);
                    }
                }
                else
                {
                    indent = indent.TrimEnd(new Char[] {' '});
                    indent = indent.TrimEnd(new Char[] { '|'});
                }
                return indent;
            }
            
        }
        internal class BinomialHeap
        {
            private Node head;
            internal BinomialHeap()
            {
                head = null;
            }
            internal BinomialHeap(Node head)
            {
                this.head = head;
            }
            internal void clear()
            {
                head = null;
            }
            internal void insert(int key)
            {
                Node node = new Node(key);
                BinomialHeap tempHeap = new BinomialHeap(node);
                head = union(tempHeap);
            }
            private Node union(BinomialHeap Heap)
            {
                Node new_head = merge(this, Heap);
                head = null;
                Heap.head = null;
                if (new_head == null)
                    return null;

                Node prev = null;
                Node current = new_head;
                Node next = new_head.sibling;

                while (next != null)
                {
                    if (current.degree != next.degree || 
                        (next.sibling != null && next.sibling.degree == current.degree))
                    {
                        prev = current;
                        current = next;
                    }
                    else if (current.key < next.key)// если правый(следующий) корень меньше текущего
                    {// значит корнем нового дерева будет текущий корень
                        current.sibling = next.sibling;
                        build_tree(current, next);
                    }
                    else
                    {
                        if (prev == null)
                            new_head = next;
                        else
                            prev.sibling = next;

                        build_tree(next, current);

                        current = next;
                    }
                    next = current.sibling;
                }
                return new_head;
            }         
            private Node merge(BinomialHeap Heap1, BinomialHeap Heap2)
            {
                if (Heap1.head == null)
                    return Heap2.head;
                else if (Heap2.head == null)
                    return Heap1.head;
                else
                {
                    Node new_head;
                    Node heap1_next = Heap1.head;
                    Node heap2_next = Heap2.head;

                    if (Heap1.head.degree <= Heap2.head.degree)
                    {
                        new_head = Heap1.head;
                        heap1_next = heap1_next.sibling;
                    }
                    else
                    {
                        new_head = Heap2.head;
                        heap2_next = heap2_next.sibling;
                    }

                    Node end = new_head;
                    while (heap1_next != null && heap2_next != null)
                    {
                        if (heap1_next.degree <= heap2_next.degree)
                        {
                            end.sibling = heap1_next;
                            heap1_next = heap1_next.sibling;
                        }
                        else
                        {
                            end.sibling = heap2_next;
                            heap2_next = heap2_next.sibling;
                        }
                        end = end.sibling;
                    }

                    if (heap1_next != null)
                        end.sibling = heap1_next;
                    else end.sibling = heap2_next;

                    return new_head;
                }
            }
            private void build_tree(Node root, Node sub_root)
            {
                sub_root.parent = root;
                sub_root.sibling = root.child;
                root.child = sub_root;
                root.degree++;
            }
            internal void delete_min()
            {
                if (head == null)
                {
                   // Console.Write("Куча пуста");
                    return;
                }

                Node next = head.sibling;

                Node min_root = head;
                Node prev_min = null;//для связи корней после удаления
                Node prev_next = min_root;
                while (next != null)
                {
                    if (next.key < min_root.key)
                    {
                        min_root = next;
                        prev_min = prev_next;
                    }
                    prev_next = next;
                    next = next.sibling;
                }
                Console.WriteLine("Элемент " + min_root.key + " удален ");
                remove_root(min_root, prev_min);

            }
            private void remove_root(Node root, Node prev_root)
            {
                if (root == head)
                    head = root.sibling;
                else prev_root.sibling = root.sibling;

                //Найти новый корень для этого дерева и пересобрать кучу
                Node new_head = null;
                Node child = root.child;

                while (child != null)
                {
                    Node other_child = child.sibling;
                    child.sibling = new_head;
                    child.parent = null;
                    new_head = child;
                    child = other_child;
                }

                BinomialHeap new_heap = new BinomialHeap(new_head);

                head = union(new_heap);
            }
            internal void print()
            {

                if (head != null)
                {
                    if (head.sibling != null)
                        head.print("", NodePosition.head, false, false);
                    else head.print("", NodePosition.head, true, false);
                }
                else Console.WriteLine("Куча пуста");
            }
        }

        static void Main(string[] args)
        {
            BinomialHeap heap = new BinomialHeap();
            int n, data;
            do
            {
                Console.WriteLine("1)Добавить элемент\n" +
                    "2)Удалить минимальный(максимальный в приоритете)\n" +
                    "3)Вывод кучи\n4)Выход");
                while (!Int32.TryParse(Console.ReadLine(), out n)) ;
                switch (n)
                {
                    case 1:
                        do
                        {
                            Console.Write("Введите значение: ");
                        } while (!Int32.TryParse(Console.ReadLine(), out data));
                        heap.insert(data);
                        break;
                    case 2:
                        heap.delete_min();
                        heap.print();
                        break;
                    case 3:
                        heap.print();
                        break;
                    case 4: break;
                    default:
                        Console.WriteLine("Такой команды нет. Введите ещё раз");
                        break;
                }
            } while (n != 4);
        }
    }
}
