#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
//���簢�� �� ���
void main() {
	int a;
	long binary;

	printf("�� �Է� : ");
	scanf("%d", &a);
	two(a);
}

int two(int n) {
	if (n == 0 || n == 1)
		printf("%d", n);
	else{
		two(n / 2);
		printf("%d", n % 2);
	}
}