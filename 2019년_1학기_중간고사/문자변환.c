#include<stdio.h>
#include<stdlib.h>
#include<string.h>
char maxof(const char a[], int n) {
	int i;
	char max = a[0];
	for (i = 1; i < n; i++)
		if (a[i] > max) max = a[i];
	return max;
}
char minof(const char a[], int n) {
	int i;
	char min = a[0];
	for (i = 1; i < n; i++)
		if (a[i] < min) min = a[i];
	return min;
}
int main(void) {
	int i;
	char *height;/*�迭��ù��°�����������*/
	int number;/*�ο� �迭eight�ǿ�Ұ���*/
	printf("���ڼ� ");
	scanf("%d", &number);
	height = calloc(number, sizeof(char));/*��ڼ���umber���ι迭������*/

	printf("%d �������Է����ּ���\n", number);

	for (i = 0; i < number; i++) {
		printf("����[%d] : ", i);
		scanf(" %c", &height[i]);
	}

	printf("Ű�ִ���%c�Դϴ�\n", maxof(height, number));
	printf("Ű�ּڰ���%c�Դϴ�\n", minof(height, number));
	free(height);/*�迭height������*/

	return 0;
}
