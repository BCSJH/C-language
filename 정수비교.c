#include<stdio.h>
#include<stdlib.h>
int maxof(const int a[], int n) {//최댓값
	int i;
	int max = a[0];
	for (i = 1; i < n; i++)
		if (a[i] > max) max = a[i];
	return max;
}
int minof(const int a[], int n) {//최솟값
	int i;
	int min = a[0];
	for (i = 1; i < n; i++)
		if (a[i] < min) min = a[i];
	return min;
}
int main(void) {
	int i;
	int *height;/*배열의 첫 번째 요소의 포인터*/
	int number;/*인원 = 배열 height의 요소 개수*/
	printf("사람 수: ");
	scanf("%d", &number);
	height = calloc(number, sizeof(int));/*요솟수가 number개인 배열을 생성*/
	printf("%d 사람의 몸무게를 입력하세요.\n", number);

	for (i = 0; i < number; i++) {
		printf("height[%d] : ", i);
		scanf("%d", &height[i]);
	}
	printf("키 최댓값은 %d입니다.\n", maxof(height, number));
	printf("키 최솟값은 %d입니다.\n", minof(height, number));
	free(height);/*배열 height를 해제*/

	return 0;
}


