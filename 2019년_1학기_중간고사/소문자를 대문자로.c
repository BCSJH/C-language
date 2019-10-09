#include<stdio.h>
#include<stdlib.h>
#include<conio.h>
#include<ctype.h>

void main() {
	char a[300];
	printf("소문자를 대문자로 : ");
	scanf("%s", &a);
	for (int i = 0; i < a[i]; i++)
		a[i] = toupper(a[i]);
	printf("%s", a);

}
