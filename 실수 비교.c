#include<stdio.h>
#include<stdlib.h>
float maxof(const float a[], int n) {
	int i;
	float max = a[0];
	for (i = 1; i < n; i++)
		if (a[i] > max) max = a[i];
	return max;
}
float minof(const float a[], int n) {
	int i;
	float min = a[0];
	for (i = 1; i < n; i++)
		if (a[i] < min) min = a[i];
	return min;
}
int main(void) {
	int i;
	float *height;/*�迭�� ù ��° ����� ������*/
	int number;/*�ο� = �迭 height�� ��� ����*/
	printf("��� ��: ");
	scanf("%d", &number);
	height = calloc(number, sizeof(float));/*��ڼ��� number���� �迭�� ����*/
	printf("%d ����� Ű�� �Է��ϼ���.\n", number);

	for (i = 0; i < number; i++) {
		printf("height[%d] : ", i);
		scanf("%f", &height[i]);
	}
	printf("Ű �ִ��� %f�Դϴ�.\n", maxof(height, number));
	printf("Ű �ּڰ��� %f�Դϴ�.\n", minof(height, number));
	free(height);/*�迭 height�� ����*/

	return 0;
}
