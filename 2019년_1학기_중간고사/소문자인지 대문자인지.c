#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include<ctype.h>

void main() {
	char a;
	printf("소문자인지 대문자인지 :  ");
	scanf("%c", &a);
	if (a >= 'a'&&a <= 'z')
		printf("소문자\m");
	else if (a >= 'A'&&a <= 'Z')
		printf("대문자\m");
	else
		printf("영어가 아님\m");

}
