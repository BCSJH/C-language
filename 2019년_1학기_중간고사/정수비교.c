#include<stdio.h>
#include<stdlib.h>
int maxof(const int a[], int n) {//�ִ�
	int i;
	int max = a[0];
	for (i = 1; i < n; i++)
		if (a[i] > max) max = a[i];
	return max;
}
int minof(const int a[], int n) {//�ּڰ�
	int i;
	int min = a[0];
	for (i = 1; i < n; i++)
		if (a[i] < min) min = a[i];
	return min;
}
int main(void) {
	int i;
	int *height;/*�迭�� ù ��° ����� ������*/
	int number;/*�ο� = �迭 height�� ��� ����*/
	printf("��� ��: ");
	scanf("%d", &number);
	height = calloc(number, sizeof(int));/*��ڼ��� number���� �迭�� ����*/
	printf("%d ����� �����Ը� �Է��ϼ���.\n", number);

	for (i = 0; i < number; i++) {
		printf("height[%d] : ", i);
		scanf("%d", &height[i]);
	}
	printf("Ű �ִ��� %d�Դϴ�.\n", maxof(height, number));
	printf("Ű �ּڰ��� %d�Դϴ�.\n", minof(height, number));
	free(height);/*�迭 height�� ����*/

	return 0;
}


