#include<stdio.h>
#include<string.h>
int main() {
	int n;//html
	int a[60] = {0, };
	int c=0;
	printf("���� : ");
	scanf("%d", &n);
	while (1)
	{
		a[c] = n % 2;//������
		n = n / 2;//��
		c++;
		if (n ==0)
			break;
	}

	for (int ii =c-1; ii>=0; ii--)
	{
	printf("%d", a[ii]);
	}
}
