#include<stdio.h>
#include<conio.h>
void main() {
	int ch;
	printf("�ƽ�Ű �ڵ�� ��ȯ�� Ű�� ��������.\n");
	printf("EnterŰ�� ������ ���α׷��� �����մϴ�.\n");
	do {
		ch = getch();
		printf("����(%c),�ƽ�Ű �ڵ�=(%d)\n", ch, ch);
	} while (ch!=13);

}