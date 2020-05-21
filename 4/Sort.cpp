#include<iostream>
#include<random>
#include <ctime>
#include<math.h>
#include<vector>
#include <deque>
using namespace std;

class Sort {
	int n;
	int m = 0;
	deque<int> arr;
	deque<int> arr2;
	unsigned int stime;

	void compAndSwap(int i, int j, int dir) {
		if (dir == (arr[i] > arr[j]))
			swap(arr[i], arr[j]);
	}
	void bitonicMerge(int low, int count, int dir) {
		if (count > 1) {
			int k = count / 2;
			for (int i = low; i < low + k; i++) 
				compAndSwap(i, i + k, dir);
			if (k > 1) {
				bitonicMerge(low, k, dir);
				bitonicMerge(low + k, k, dir);
			}
		}
	}
	void BSort(int low, int count, int dir) {
		if (count > 1) {
			int k = count / 2;
			if (k > 1) {
				BSort(low, k, 1);
				BSort(low + k, k, 0);
			}
			bitonicMerge(low, count, dir);
		}
	}
public:
	
	Sort() {
		cout << "Count of elements:";
		cin >> n;
		arr.resize(n);
		arr2.resize(n);
	}
	int get_n() {
		return n;
	}
	void set_time(unsigned int clock) {
		this->stime = clock;
	}
	unsigned int get_time() {
		return stime;
	}
	void Sorting() {
		int j, i = 0;
		if (m != 4) {
			cout << "What is the elements?\n1)Increase\n2)Decrease\n3)Random" << endl;
			cin >> m;
			while (m != 1 && m != 2 && m != 3 && m != 4) {
				cout << "Error. Input another one" << endl;
				cin >> m;
			}

			if (m == 2) {
				j = n;
				for (i; i < n; i++) {
					arr[i] = arr2[i] = j;
					j--;
				}
				m = 4;
			}
			else if (m == 1) {
				j = 0;
				for (i; i < n; i++) {
					arr[i] = arr2[i] = j;
					j++;
				}
				m = 4;
			}
			else if (m == 3) {
				mt19937 gen(time(0));
				uniform_int_distribution<> uid(-10000, 10000);
				for (i; i < n; i++) {
					arr[i] = arr2[i] = uid(gen);
				}
				m = 4;
			}
		}		
		else
			for (i = 0; i < n; i++)
				arr[i] = arr2[i];
				
	}
	void SelectionSort() {
		stime = clock();
		int count, min, j, i;
		for (i = 0; i < n - 1; i++) {

			min = i;
			for (j = i + 1; j < n; j++) {
				if (arr[min] > arr[j])
					min = j;
			}
			if (min != i) {
				count = arr[i];
				arr[i] = arr[min];
				arr[min] = count;
			}
		}
		stime = clock() - stime;
	}
	void BubbleSort() {
		stime = clock();
		int temp;
		bool NeedIteration = true;
		while (NeedIteration) {
			NeedIteration = false;
			for (int i = 1; i < n; i++) {
				if (arr[i - 1] > arr[i]) {
					temp = arr[i];
					arr[i] = arr[i - 1];
					arr[i - 1] = temp;
					NeedIteration = true;
				}
			}
		}
		stime = clock() - stime;
	}
	void InsertionSort() {
		stime = clock();
		int temp, i, j;
		for (i = 1; i < n; i++) {
			temp = arr[i];
			for (j = i - 1; j >= 0 && arr[j] > temp; j--) {
				arr[j + 1] = arr[j];
			}
			arr[j + 1] = temp;
		}
		stime = clock() - stime;
	}
	void QuickSort(int first, int last) {
		int left = first, right = last, middle = arr[(first + last) / 2], temp;
		do {
			while (arr[left] < middle)left++;
			while (arr[right] > middle)right--;
			if (left <= right) {
				if (arr[left] > arr[right]) {
					temp = arr[right];
					arr[right] = arr[left];
					arr[left] = temp;
				}
				left++;
				right--;
			}
		} while (left <= right);
		if (left < last) {
			QuickSort(left, last);
		}
		if (first < right) {
			QuickSort(first, right);
		}
	}
	void BitonicSort() {
		stime = clock();
		int i, two = 2;
		while (n > two) {
			two *= 2;
		}

		if (n != two) {//нужно точно знать какого элемента нет
			n = two - n;
			for (i = 0; i < n; i++)
				arr.push_back(10001);
		}
		n = two - n;
		BSort(0, two, 1);
		arr.resize(n);
		stime = clock() - stime;
		}
	void out() {
		for (int i = 0; i < n; i++) {
			cout << arr[i] << " ";
		}
		cout << endl;
		cout <<"TIME:"<< stime << endl << endl;
		system("pause");
	}
};

int main() {
	setlocale(LC_ALL, "rus");

	Sort answer;
	answer.Sorting();
	answer.SelectionSort();
	cout << "TIME SelectionSort: " << (float)answer.get_time()/CLOCKS_PER_SEC << endl;
	answer.Sorting();
	answer.BubbleSort();
	cout << "TIME BubbleSort: " << (float)answer.get_time() / CLOCKS_PER_SEC << endl;
	answer.Sorting();
	answer.InsertionSort();
	cout << "TIME InsertionSort: " << (float)answer.get_time() / CLOCKS_PER_SEC << endl;
	answer.Sorting();
	answer.set_time(clock());
	answer.QuickSort(0, answer.get_n() - 1);
	answer.set_time(clock() - answer.get_time());
	cout << "TIME QuickSort: " << (float)answer.get_time() / CLOCKS_PER_SEC << endl;
	answer.Sorting();
	answer.BitonicSort();
	cout << "TIME BitonicSort: " << (float)answer.get_time() / CLOCKS_PER_SEC << endl;
	system("pause");
	return 0;
}