#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include<ctype.h>

void main() {
	char a;
	printf("�ҹ������� �빮������ :  ");
	scanf("%c", &a);
	if (a >= 'a'&&a <= 'z')
		printf("�ҹ���\m");
	else if (a >= 'A'&&a <= 'Z')
		printf("�빮��\m");
	else
		printf("��� �ƴ�\m");

}
