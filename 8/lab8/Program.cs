//https://github.com/PaulineHK/ASD
using System;
using System.Collections.Generic;

namespace lab8
{
    internal enum NodePosition
    {
        left,
        right,
        center
    }
    class Node
    {
        internal int data;
        internal Node left = null;
        internal Node right = null;
        internal Node parent = null;

        internal Node(int data)
        {
            this.data = data;
        }

        private void PrintValue(string value, NodePosition nodePostion)
        {
            switch (nodePostion)
            {
                case NodePosition.left:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("L:");
                    print_value(value);
                    break;
                case NodePosition.right:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("R:");
                    print_value(value);
                    break;
                case NodePosition.center:
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
        internal void print(string indent, NodePosition nodePosition, bool end_level, bool empty)
        {
            Console.Write(indent);
            if (end_level)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }

            var stringValue = empty ? "-" : data.ToString();
            PrintValue(stringValue, nodePosition);

            if (!empty && (left != null || right != null))
            {
                if (left != null)
                    left.print(indent, NodePosition.left, false, false);
                else
                    print(indent, NodePosition.left, false, true);

                if (right != null)
                    right.print(indent, NodePosition.right, true, false);
                else
                    print(indent, NodePosition.right, true, true);
            }
        }
    };

    internal class BSTree
    {
        private Node root;

        private int count = 0;//для задания
        private int number = 0;//для задания
                              
        internal BSTree()
        {
            root = null;
        }
        internal void add(int data)
        {
            if (root == null)
            {
                root = new Node(data);
                count++;
            }
            else
                addTo(data, root);
        }
        private void addTo(int data, Node root)
        {
            if (root.data == data)
                Console.WriteLine("Такое значение уже есть");
            else if (data < root.data)
            {
                if (root.left == null)
                {
                    root.left = new Node(data);
                    root.left.parent = root;
                    count++;
                    //return;
                }
                else
                {
                    addTo(data, root.left);
                    //return;
                }
            }
            else if (root.right == null)
            {
                root.right = new Node(data);
                root.right.parent = root;
                count++;
            }
            else addTo(data, root.right);
        }
        internal bool find(int data)
        {
            if (root == null)
                Console.WriteLine("Дерево пусто");
            else
            {
                Node current = find_value(data, root);
                if (current != null)
                    Console.WriteLine("Такой элемент есть");
                else
                {
                    Console.WriteLine("Такого элемента нет");
                    return true;
                }
            }
            return false;

        }
        private Node find_value(int data, Node root)
        {
            if (root.data == data)
                return root;
            else if (data < root.data)
            {
                if (root.left != null)
                    return find_value(data, root.left);
            }
            else if (root.right != null)
                return find_value(data, root.right);

            return null;
        }
        internal void delete(int data, bool right, bool task, Node current1)
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пусто");
                return;
            }
            Node current;
            if (!task)
            {
                current = find_value(data, root);
            }
            else current = current1;      
            if (current != null)
            {
                if (current.left == null && current.right == null)
                {
                    if (current != root)
                        if (current.parent.left == current)
                            current.parent.left = null;
                        else current.parent.right = null;

                    current = null;

                }
                else if (right)
                {
                    if (current.right == null)//если если нет правого поддерева при правом удалении
                    {
                        if (current.parent != null)
                        {
                            if (current.parent.left == current)
                                current.parent.left = current.left;
                            else
                                current.parent.right = current.left;
                        }

                        current.left.parent = current.parent;

                        current = null;

                    }
                    else
                        r_del(current);
                }
                else
                {
                    if (current.left == null)//если нет левого поддерева при левом удалении
                    {
                        if (current.parent != null)
                        {
                            if (current.parent.left == current)
                                current.parent.left = current.right;

                            else current.parent.right = current.right;
                        }
                        current.right.parent = current.parent;

                        current = null;
                    }
                    else l_del(current);
                }
                Console.WriteLine("Элемент удален");               
            }
            else Console.WriteLine("Такого элемента нет");
        }          
        private void r_del(Node root)
        {
            Node current = root.right;

            while (current.left != null)
                current = current.left;

            if (current.parent != root)
            {
                if (current.right != null)//если у замены есть правая ветка
                {
                //    if (current.parent.right == current)
                        current.parent.left = current.right;
               //     else current.parent.left = current.right;

                    current.right.parent = current.parent;
                }

                current.right = root.right;
                root.right.parent = current;
                current.parent.left = null;
            }

            current.left = root.left;
            if (root.left != null)
                root.left.parent = current;

            if (root.parent != null) //переброс ссылки с родителя удаляемого
            {
                if (root.parent.left == root)
                    root.parent.left = current;
                else root.parent.right = current;
            }

            current.parent = root.parent;

            if (root == this.root)//если удаляемая вершина корень
            {
                this.root = null;
                this.root = current;
            }else
            {
                root = null;
                root = current;
            }          
        }   
        private void l_del(Node root)
        {
            Node current = root.left;
            while (current.right != null)
                current = current.right;

            if (current.parent != root)
            {
                if (current.left != null)//если у замены есть левая ветка
                {
                   // if (current.parent.right == current)
                        current.parent.right = current.right;
                   // else current.parent.left = current.right;

                    current.left.parent = current.parent;
                }

                current.left = root.left;
                root.left.parent = current;
                current.parent.right = null;
            }

            current.right = root.right;
            if (root.right != null)
                root.right.parent = current;

            if (root.parent != null)//переброс ссылки с родителя удаляемого
            {
                if (root.parent.left == root)
                    root.parent.left = current;
                else root.parent.right = current;
            }

            current.parent = root.parent;

            if (root == this.root)//если удаляемая вершина корень
            {
                this.root = null;
                this.root = current;
            }
            else
            {
                root = null;
                root = current;
            }
        }
        internal void height(int data)
        {
            if (root == null)
                Console.WriteLine("Дерево пусто");
            else
            {
                Node current = find_value(data, root);
                if (current == null)
                    Console.WriteLine("Такого элемента нет");
                else
                {
                    int count = 0;
                    count = Math.Max(sub_height(current.left), sub_height(current.right));
                    Console.WriteLine("Высота: " + count);
                }
            }
        }
        private int sub_height(Node root)
        {
            int count = 0;
            if (root != null)
            {

                count = 1 + Math.Max(sub_height(root.left), sub_height(root.right));
            }

            return count;
        }
        internal void depth(int data)
        {
            if (root == null)
                Console.WriteLine("Дерево пусто");
            else
            {
                Node current = find_value(data, root);
                if (current == null)
                    Console.WriteLine("Такого элемента нет");
                else
                {
                    int count = 0;//глубина у корня
                    if (current != root)
                        count = 1 + sub_depth(current.parent);
                    Console.WriteLine("Глубина: " + count);

                }
            }
        }
        private int sub_depth(Node root)
        {
            int count = 0;
            if (root.parent != null)
                count = 1 + sub_depth(root.parent);
            return count;
        }
        internal void level(int data)
        {
            if (root == null)
                Console.WriteLine("Дерево пусто");
            else
            {
                Node current = find_value(data, root);
                if (current == null)
                    Console.WriteLine("Такого элемента нет");
                else
                {
                    int count = 0;
                   // if (current != root)
                        count = sub_height(root) - sub_depth(current) /*- 1*/;
                    Console.WriteLine("Уровень: " + count);
                }

            }
        }
        internal void print_tree()
        {
            root.print("", NodePosition.center, true, false);
        }
        internal void print(int number)
        {
            if (number == 1)
                print_direct(root);
            else if (number == 2)
                print_reverse(root);
            else print_symmetric(root);
        }
        private void print_direct(Node root)
        {
            if (root == null)
                return;

            Console.Write(root.data + " ");
            count++;
            print_direct(root.left);
            print_direct(root.right);
        }
        private void print_symmetric(Node root)
        {
            if (root == null)
                return;

            print_symmetric(root.left);
            Console.Write(root.data + " ");
            print_symmetric(root.right);
        }
        private void print_reverse(Node root)
        {
            if (root == null)
                return;

            print_reverse(root.left);
            print_reverse(root.right);
            Console.Write(root.data + " ");
        }
        internal void task()
        {
            count = 0;
            print_direct(root);
            Console.WriteLine();
            if (count % 2 != 0)
            {

                int level = sub_height(root) - 1;
                level /= 2;
                bool ok = false;
                List<Node> list = new List<Node>();
                middle(root, list);
                number = 0;
                nodes_on_level(level, root, list);

                if (list.Count > 1)
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        if (list[0] == list[i])
                        {
                            delete(0, true, true, list[0]);
                            print_direct(root);
                            ok = true;
                        }
                    }
                    if (!ok)
                    {
                        Console.WriteLine("Найденные вершины не являются средними по значению");
                        Console.WriteLine("Средняя вершина:" + list[0].data);
                        for (int i = 1; i < list.Count; i++)
                            Console.Write("Вершины по заданию: " + list[i]);
                    }
                }
                else Console.WriteLine("Вершин, удовлетворяющих заданию, нет");
            }
            else Console.Write("Средних по значению вершин нет");
        }
        private void nodes_on_level(int level, Node root, List<Node> list)
        {
            if (root != null)
            {

                if (level == 0)
                {
                    int l, r;
                    l = sub_height(root.left);
                    r = sub_height(root.right);
                    if (l > r) list.Add(root);
                }
                else
                {
                    nodes_on_level(level - 1, root.left, list);
                    nodes_on_level(level - 1, root.right, list);
                }
            }
        }
        private void middle(Node root, List<Node> list)
        {

            if (root != null)
            {

                middle(root.left, list);
                number++;
                if (number == count / 2 + 1)
                {
                    list.Add(root);
                }
                middle(root.right, list);

            }
        }
    };

    class Program
    {
        static void Main(string[] args)
        {
            int n, data = 0, k = 0;
            BSTree tree = new BSTree();
            //task
            tree.add(30);
            tree.add(11);
            tree.add(2);
            tree.add(15);
            tree.add(13);
            tree.add(17);
            tree.add(50);
            tree.add(40);
            tree.add(37);
            tree.add(36);
            tree.add(45);
            tree.add(72);
            tree.add(92);
            tree.add(102);
            tree.add(205);
            tree.add(200);
            tree.add(1);
            Console.WriteLine("Найти высоту дерева h и удалить (правым удалением) среднюю " +
                "по значению вершину из вершин дерева на уровне [h/2], для которых количество потомков " +
                "в левом поддереве больше, чем количество потомков в правом поддереве.\nВыполнить прямой (левый) обход полученного дерева.\n");
            tree.task();

            do
            {
                Console.WriteLine("\n1)Добавить элементы\n2)Поиск\n3)Удаление\n4)Высота\n5)Глубина\n6)Уровень\n7)Обход\n8)Вывод дерева\n9)Задание\n10)Выход");
                while (!Int32.TryParse(Console.ReadLine(), out k)) Console.WriteLine("Неверно. Введите ещё раз:");
                switch (k)
                {
                    case 1:
                        Console.Write("Сколько добавить элементов: ");
                        while (!Int32.TryParse(Console.ReadLine(), out n)) ;
                        for (int i = 0; i < n; i++)
                        {
                            do
                            {
                                Console.Write((i + 1) + ": ");
                            } while (!Int32.TryParse(Console.ReadLine(), out data));
                            tree.add(data);
                        }
                        break;

                    case 2:
                        Console.Write("Введите элемент: ");
                        while (!Int32.TryParse(Console.ReadLine(), out data)) ;
                        tree.find(data);
                        break;

                    case 3:
                        Console.WriteLine("Правое - 1\nЛевое - 2");
                        do
                        {
                            while (!Int32.TryParse(Console.ReadLine(), out n)) ;
                        } while (n != 1 && n != 2);
                        Console.Write("Введите элемент: ");
                        while (!Int32.TryParse(Console.ReadLine(), out data)) ;
                        if (n == 1)
                            tree.delete(data, true, false, null);
                        else tree.delete(data, false, false, null);
                        tree.print_tree();
                        break;

                    case 4:
                        Console.Write("Введите элемент: ");
                        while (!Int32.TryParse(Console.ReadLine(), out data)) ;
                        tree.height(data);
                        break;

                    case 5:
                        Console.Write("Введите элемент: ");
                        while (!Int32.TryParse(Console.ReadLine(), out data)) ;
                        tree.depth(data);
                        break;

                    case 6:
                        Console.Write("Введите элемент: ");
                        while (!Int32.TryParse(Console.ReadLine(), out data)) ;
                        tree.level(data);
                        break;

                    case 7:
                        Console.WriteLine("Прямой - 1\nОбратный - 2\nСимметричный - 3");
                        do
                        {
                            while (!Int32.TryParse(Console.ReadLine(), out n)) ;
                        } while (n != 1 && n != 2 && n != 3);
                        Console.WriteLine();
                        tree.print(n);
                        break;

                    case 8:
                        tree.print_tree();
                        break;
                    case 9:
                        Console.WriteLine("Найти высоту дерева h и удалить (правым удалением) среднюю " +
                        "по значению вершину из вершин дерева на уровне [h/2], для которых количество потомков " +
                        "в левом поддереве больше, чем количество потомков в правом поддереве.\nВыполнить прямой (левый) обход полученного дерева.\n");
                        tree.task();
                        break;
                    case 10: break;
                    default:
                        {
                            Console.WriteLine("Такой команды нет");
                            break;
                        }
                }
            } while (k != 10);

            //Console.ReadKey();
        }
    }
}
