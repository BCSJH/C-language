#include<stdio.h>
#include<stdlib.h>
#include<string.h>
int maxof(const int a[], int n) {
	int i;
	int max = a[0]; //ù��° ���� �̸� ����
	for (i = 1; i < n; i++)
		if (a[i] > max) max = a[i]; //�� ��
	return max;
}
int minof(const int a[], int n) {
	int i;
	int min = a[0];
	for (i = 1; i < n; i++)
		if (a[i] < min) min = a[i];
	return min;
}

int main(void) {
	int i;
	char a[20];

	int *height;/*�迭�� ù ��° ����� ������*/

	int number;/*�ο� = �迭 height�� ��� ����*/

	printf("���ڿ� ����: ");
	scanf("%d", &number);
	height = calloc(number, sizeof(int));/*��ڼ��� number���� �迭�� ����*/

	printf("%d ���ڿ��� �Է����ּ���.\n", number);

	for (i = 0; i < number; i++) {
		printf("���ڿ�[%d] : ", i);
		scanf("%s", a);

		height[i] = strlen(a); //�迭 ���� ���ϱ�
	}
	printf("���� �� ���� ���̴� %d�Դϴ�\n", maxof(height, number));
	printf("���� ª�� ���� ���̴� %d�Դϴ�\n", minof(height, number));

	free(height);/*�迭 height�� ����*/

	return 0;
}
