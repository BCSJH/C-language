#include<stdio.h>
#include<stdlib.h>
#include<string.h>
int maxof(const int a[], int n) {
	int i;
	int max = a[0]; //첫번째 값을 미리 저장
	for (i = 1; i < n; i++)
		if (a[i] > max) max = a[i]; //값 비교
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

	int *height;/*배열의 첫 번째 요소의 포인터*/

	int number;/*인원 = 배열 height의 요소 개수*/

	printf("문자열 개수: ");
	scanf("%d", &number);
	height = calloc(number, sizeof(int));/*요솟수가 number개인 배열을 생성*/

	printf("%d 문자열을 입력해주세요.\n", number);

	for (i = 0; i < number; i++) {
		printf("문자열[%d] : ", i);
		scanf("%s", a);

		height[i] = strlen(a); //배열 길이 구하기
	}
	printf("제일 긴 값의 길이는 %d입니다\n", maxof(height, number));
	printf("제일 짧은 값의 길이는 %d입니다\n", minof(height, number));

	free(height);/*배열 height를 해제*/

	return 0;
}
