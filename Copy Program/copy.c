/* 
* @author: Cora Boyoung Jung 
* @date: 17 Feb, 2020
* CS 232: Homework02: Writing a copy program in C 
*/ 

#include <stdio.h> 
#include <stdlib.h> 
#include <unistd.h> 
#include <sys/stat.h> 
#include <sys/types.h>


int main(int argc, char * argv[])  
{ 
    // file pointers 
    FILE *srcPtr, *destPtr; 

    // variables 
    char ch; 
    char * srcFile; 
    char * destFile; 
    srcFile = argv[1]; 
    destFile = argv[2]; 

    // checking stats for source file
    struct stat srcStats; 
    stat(srcFile, &srcStats);

    // check if the number of arguments is correct 
    if (argc != 3)  
    { 
        printf("Enter exactly 2 arguments.\n"); 
        printf("Write in this format: ./copy src dest\n"); 
        exit(-1); 
    } 

    // proceed when the number of arguments is correct 
    else 
    { 
        // check if src file exits 
        if (access(srcFile, F_OK) != 0)  
        { 
            perror("Error: Cannot open source file"); 
            exit(-1); 
        } 
        // checks if dest file already exist 
        else if (access(destFile, F_OK) == 0)  
        { 
            printf("Error: Destination file name already exists\n"); 
            exit(-1); 
        } 
        // checks if src file is readable 
        else if (access(srcFile, R_OK) != 0) 
        { 
            perror("Error: Cannot read the source file"); 
            exit(-1); 
        } 
        // checks if src file is a regular file 
        else if (S_ISREG(srcStats.st_mode) == 0)
        { 
            printf("Error: Source file is not a regular file\n"); 
            exit(-1); 
        } 
        // proceed when there is no error 
        else  
        { 
		    // open source file in read mode and 
		    // create destination file in write mode 
		    srcPtr = fopen(srcFile, "r"); 
		    destPtr = fopen(destFile, "w");

		    ch = fgetc(srcPtr);
            // copy the content from src to dest 
            while (ch != EOF)  
            { 
                fputc(ch, destPtr); 
        		ch = fgetc(srcPtr);

            } 
        } 
    } 

    // close connection 
    fclose(srcPtr); 
    fclose(destPtr);   

    // print successful message 
    printf("Content of %s copied to %s successfully.\n", srcFile, destFile); 
    return 0; 

} 