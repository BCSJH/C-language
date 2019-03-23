#include<stdio.h>
#include<conio.h>
//직사각형 별 출력
void main() {
	int a;//사용자로부터 입력
	printf("높이 입력 : ");
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
	   i 띄어쓰기는 입력받은 것의 -1-> int i=1 증가하는 값만큼 빼주면됨
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