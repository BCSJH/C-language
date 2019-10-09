/* 덧셈표와 곱셈표 */
#include <stdio.h>

/*--- x1과 x2의 합을 구합니다. ---*/
int sum(int x1, int x2)
{
	return x1 + x2;
}

/*--- x1과 x2의 곱을 구합니다. ---*/
int mul(int x1, int x2)
{
	return x1 * x2;
}

/*--- 덧셈, 곱셈표를 출력 ---*/
void kuku(int(*calc)(int, int))
{
	int i, j;
	
	for (i = 1; i <= 9; i++) {
		for (j = 1; j <= 9; j++)
			printf("%3d", (*calc)(i, j));
		putchar('\n');
	}
		printf(" 반대로\n");
		 
		for (i = 9; i >= 1; i--) {
			for (j = 9; j >= 1; j--)
			printf("%3d", (*calc)(i, j));
		putchar('\n');
	}
}

int main(void)
{
	puts("덧셈표");
	kuku(sum);
	
	puts("\n 곱셈표");
	kuku(mul);

	return 0;
}
