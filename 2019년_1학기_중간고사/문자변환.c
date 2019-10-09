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
	char *height;/*배열의첫번째요소의포인터*/
	int number;/*인원 배열eight의요소개수*/
	printf("문자수 ");
	scanf("%d", &number);
	height = calloc(number, sizeof(char));/*요솟수가umber개인배열을생성*/

	printf("%d 문자을입력해주세요\n", number);

	for (i = 0; i < number; i++) {
		printf("문자[%d] : ", i);
		scanf(" %c", &height[i]);
	}

	printf("키최댓값은%c입니다\n", maxof(height, number));
	printf("키최솟값은%c입니다\n", minof(height, number));
	free(height);/*배열height를해제*/

	return 0;
}
