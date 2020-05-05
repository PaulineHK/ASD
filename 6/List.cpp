// 
#include<iostream>
#include<random>
#include<windows.h>

using namespace std;

class Node{
	double data;
	Node* next;
	Node* prev;
public:
	Node(double data, Node* next, Node* prev) {
		this->data = data;
		this->next = next;
		this->prev = prev;
	}
	Node(double data) {
		this->data = data;
	}
	friend class List;
};

class List {
	Node* head;
	Node* tail;
public:
	List():  head(NULL), tail(NULL){ }
	void push_back(double data) {
		Node* node = new Node(data, NULL, tail);
		tail = node;
		if (head) {
			Node* current = head;
			while (current->next)
				current = current->next;
			current->next = tail;
			tail->prev = current;
			
		}
		else
			head = node;
		
		
	}
	void push_front(double data) {
		Node *node = new Node(data);
		if (head) {
			Node* temp = head;
			head = node;
			node->next = temp;
			temp->prev = node;
		}
		else {
			head = node;			
			tail = node;		
		}
	}
	
	void pop_back() {
		if(head){		
			if (tail != head) {

				tail = tail->prev;
				delete tail->next;
				tail->next = NULL;

			}
			else {
				delete head;
				tail = NULL;
				head = NULL;
			}
			
		}		
		else {
			cout << "empty!" << endl;
		}
	}
	void pop_front() {
		if (head) {
			if (head != tail) {
				
				head = head->next;
				delete head->prev;
				head->prev = NULL;
				
			}
			else {
				delete head;
				tail = NULL;
				head = NULL;
			}
		}
		else {
			cout << "empty!" << endl;
		}
	}
	void out_front() {
		if (head) {
			Node* current = head;
			while (current) {
				cout << current->data << " ";
				current = current->next;
			}
			cout << endl;
		}
		else cout << "empty" << endl;
	}
	/*
	void out_back() {
		Node* current = tail;
		while (current) {
			cout << current->data << " ";
			current = current->prev;
		}
		cout << endl;
	}
	*/

	bool empty() {
		return (head == NULL);		
	}
	double get_data_head() {
		return head->data;
	}
	double get_data_tail() {
		return tail->data;
	}
	int size() {
		int size = 0;
		Node* current = head;
		while (current) {
			size++;
			current = current->next;
		}
		return size;
	}
};

class Stack {
	
	List stack;
public:
	void push(double n) {
		stack.push_front(n);
	};
	void pop() {
		stack.pop_front();
	};
	bool empty() {
		return stack.empty();
	};
	void out() {
		stack.out_front();
	}
	double get_data_head() {
		return stack.get_data_head();
	}
	int size() {
		return stack.size();
	}
};

class Queue {
	
	List queue;
public:

	void push(double n) {
		queue.push_back(n);
	};
	void pop() {
		queue.pop_front();
	};
	bool empty() {
		return queue.empty();
	};
	void out() {
		queue.out_front();
	}
	double get_data_head() {
		return queue.get_data_head();
	}
	int size() {
		return queue.size();
	}
};

class Deque {
	
	List deque;
public:
	void push_front(double n) {
		deque.push_front(n);
	};
	void push_back(double n) {
		deque.push_back(n);
	};
	
	void pop_front() {
		deque.pop_front();
	};
	void pop_back() {
		deque.pop_back();
	};
	
	bool empty() {
		return deque.empty();
	};
	void out() {
		deque.out_front();
	}
	double get_data_head() {
		return deque.get_data_head();
	}
	double get_data_tail() {
		return deque.get_data_tail();
	}
	int size() {
		return deque.size();
	}
};

void Kitchen(int time) {
	random_device random_device; 
	mt19937 generator(random_device()); 
	uniform_int_distribution<> distribution1(1, 4);
	uniform_int_distribution<> distribution2(1, 2);

	int a, i = 0, j = 0;
	double k = -1;
	Stack table;
	Deque first_deque, second_deque;
	Deque shelter;
	while (j != time) {
		a = distribution1(generator);
		switch (a) {
		case 1:// добавляют грязную посуду
			i++;
			table.push(i);
			k = i;
			cout << "Добавили грязную тарелку на стол: " << k << endl;
			Sleep(1000);
			break;
		case 2:// ставять грязную в машину	
			if (!table.empty()) {	
				k = table.get_data_head();
				a = distribution2(generator);
				if (a == 1) {
					table.pop();
					first_deque.push_back(k);		
					cout << "Добавили тарелку в конец первой очереди: " << k << endl;
				}
				else {
					table.pop();
					second_deque.push_back(k);
					cout << "Добавили тарелку в конец второй очереди: " << k << endl;
				}
				Sleep(2000);
			}
			else
				cout << "Стол пуст!" << endl;

			break;
		case 3:// берут чистую из машины и ставят на полку(либо в начало, либо в конец)
			if (!first_deque.empty()) {
				k = first_deque.get_data_head();
				first_deque.pop_front();
				cout << "Взяли тарелку из начала первой очереди и поставили в ";
			}
			else if (!second_deque.empty()) {
				k =second_deque.get_data_head();
				second_deque.pop_front();
				cout << "Взяли тарелку из начала второй очереди и поставили в ";
			}
			else {
				cout << "Машина пуста!" << endl;
				break;
			}
			a = distribution2(generator);
			if (a == 1) {
				shelter.push_front(k);
				cout << "начало полки: " << k << endl;
			}
			else {
				shelter.push_back(k);
				cout << "конец полки: " << k << endl;
			}
			Sleep(1000);
			break;
		case 4://забираем с полки чистую и ставим на стол грязную
			if (!shelter.empty()) {		
				k = shelter.get_data_tail();
				shelter.pop_back();		
				cout << "Взяли тарелку с конца полки и поставили на стол: " << k << endl;
				table.push(k);
				Sleep(3000);
			}		
			else cout << "Полка пуста!" << endl;			
			break;		
		}
		j++;
	}
	cout << "Грязной посуды на столе: ";
	table.out();
	cout << "Посуда в машине:\n1 ряд: ";
	first_deque.out();
	cout << "2 ряд: ";
	second_deque.out();
	cout << "Посуда на полке: ";
	shelter.out();
}

int main()
{
	setlocale(LC_ALL, "rus");
	cout << "Количество операций: ";
	int time;
	cin >> time;
	Kitchen(time);

	system("pause");
	return 0;
}
