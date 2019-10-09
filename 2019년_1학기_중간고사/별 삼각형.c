#include <stdio.h>;
/*왼쪽 아래가 직각인 이등변 삼각형을 출력*/
//n은 높이
void triangleLB(int n) {
	for (int i = 1; i <= n; i++) {
		for (int j = 1; j <= i; j++)
			printf("*");
		printf("\n");
	}
}


/*왼쪽 위가 직각인 이등변 삼각형 출력*/
void triangleLU(int n) {
	for (int i = n; i >= 1; i--) {
		for (int j = 1; j <= i; j++)
			printf("*");
		printf("\n");
	}
}


/*오른쪽 위가 직각인 이등변 삼각형 출력*/
void triangleRB(int n) {
	for (int i = 1; i <= n; i++) {
		for (int j = 1; j < i; j++)
			printf(" ");
		for (int jj = n-i+1; jj >= 1; jj--)
			printf("*");
		printf("\n");
	}
}

/*오른쪽 아래가 직각인 이등변 삼각형 출력*/
void triangleRU(int n) {
	for (int i = 1; i <= n; i++) {
		for (int jj = n - i + 1; jj > 1; jj--)
			printf(" ");
		for (int j = 1; j <= i; j++)
			printf("*");
		printf("\n");
	}
}
void spira(int n) {
	for (int i = 1; i <= n; i++) {
		for (int jj = n - i + 1; jj > 1; jj--)
			printf(" ");
		for (int j = 1; j <= i*2-1; j++)
			printf("*");
		printf("\n");
	}
}

void nrpira(int n) {
	for (int i = 1; i <= n; i++) {
		for (int j = 1; j <= i; j++)
			printf(" ");
		for (int jj = (n-i+1)*2-1; jj >= 1; jj--)
			printf("%d", i);
		printf("\n");
	}
}
void main()
{
	int n;
	printf("입력 : ");
	scanf("%d", &n);
	triangleLB(n);
	triangleLU(n);
	triangleRB(n);
	triangleRU(n);
	spira(n);
	nrpira(n);
}