#include<stdio.h>
#include<conio.h>
//���簢�� �� ���
void main() {
	int a;//����ڷκ��� �Է�
	printf("���� �Է� : ");
	scanf("%d", &a);

	for (int i = 1; i <= a; i++)
	{
		for (int ii = 0; ii < i; ii++)
			printf("*");
		printf("\n");
	}

	for (int i = 1; i <= a; i++)
	{
		for (int ii = a; ii >= i; ii--)
			printf("*");
		printf("\n");
	}
	/*
	   i ����� �Է¹��� ���� -1-> int i=1 �����ϴ� ����ŭ ���ָ��
	  ii
	 iii
	   */
	for (int i = 0; i < a; i++) {
		for (int ii = a; ii > a - i; ii--)
			printf(" ");
		for (int ii = 0; ii < i; ii++)
			printf("*");
	}

}