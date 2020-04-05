#include <sys/types.h>
#include <unistd.h>
#include <fcntl.h>
#include <stdio.h>
#include <string.h>

int main(){

    int filedes, fdnew1, fdnew2;
    ssize_t nread;
    off_t newpos;
    char buffer[1024];
    char content[1024] = "Hello my friend!!\n";

    filedes = open("data.txt", O_RDWR); //  file open
    nread = read(filedes, buffer, 1024); // file read
    printf("%s", buffer); //file print

    write(filedes, content, strlen(content)); 

    newpos = lseek(filedes, (off_t)0, SEEK_SET); // number 0 skip
    nread = read(filedes, buffer, 1024);
    printf("%s", buffer);
    
    // ASK -> bonic help me
    for ( int i = 0; i < nread; i++){
        printf("%c = %d    ", buffer[i], buffer[i]);
        if(i != 0 && i%3==0){ 
            printf("\n");
        }
    }

    close(filedes);

    fdnew1 = open("newdata1.txt", O_RDWR | O_CREAT, 0644);
    fdnew2 = creat("newdata2.txt", 0644);

    close(fdnew1);
    close(fdnew2);
    unlink("newdata2.txt");

    return 0;
}