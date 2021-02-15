/* 
 * bakeshop.c
 * Goal: explore concurrency and semaphores by writing a C console
 * 		 program that runs several different threads.
 * 
 * @author: Cora Boyoung Jung (bj29)
 * @date: March 14th, 2020
 */

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>

#include <pthread.h>    /* required for pthreads */
#include <semaphore.h>  /* required for semaphores */

// declaring semaphores
sem_t semBaker;
sem_t semCust;
sem_t semStore;

// initializing global variables
int loavesBaked = 0;
int loavesAvail = 0;
int loavesSold = 0;
int checkoutCust = 0;
long threadCheckoutReady;

/* bake()
 * @function: implement bake thread
 * @param: none
 */
void* bake() {
	while (loavesBaked < 10) {
		sem_wait(&semBaker);										// catch semaphore baker
		loavesAvail++;
		loavesBaked++;											
		fprintf(stderr, "A LOAF OF BREAD IS READY FOR SALE.\n");	// announce that a loaf of bread is baked
		sem_post(&semBaker);										// release semaphore baker
		usleep(1000000);											// one second delay
	}
	fprintf(stderr, "ALL BAKING FOR TODAY IS DONE.\n");				// anounce that all baking is done
}

/* customer()
 * @functino: implement customer thread; 
 * @param: custID (customer ID)
 */
void* customer(void* custID) {
	// convert void argument into a long int
	long id = (long)custID;

	sem_wait(&semStore);											// catch semaphore store
	fprintf(stderr, "CUSTOMER%ld ENTERED THE STORE.\n", id);		// announce that a customer entered the store
	sem_wait(&semCust);												// catch semaphore customer

	while (loavesAvail == 0) {
		usleep(1000000); 											// one second delay
	}

	fprintf(stderr, "CUSTOMER%ld HAS A LOAF OF BREAD.\n", id);		// announce that the customer got the bread
	sem_post(&semCust);												// release semaphore customer
	loavesAvail--;													
	loavesSold++;
	threadCheckoutReady = id;
	usleep(1000000);												// one second delay
	sem_wait(&semCust);												// catch semaphore customer
	fprintf(stderr, "CUSTIMER%ld HAS LEFT THE STORE.\n", id);		// announce that the customer left the store
	sem_post(&semCust);												// release semaphore customer
	sem_post(&semStore);											// release semaphore store
}

/* checkout()
 * @function: implement checkout thread
 * @param: none
 */
void* checkout() {
	while (checkoutCust < 10) {
		if (loavesSold > checkoutCust) {
			sem_wait(&semCust);																		// catch semaphore customer						
			fprintf(stderr, "CUSTOMER%ld IS WAITING TO CHECK OUT.\n", threadCheckoutReady);			// announce that the customer is waiting to be checked out
			sem_wait(&semBaker);																	// catch semaphore baker
			usleep(1000000);																		// one second delay
			fprintf(stderr, "CUSTOMER%ld PAID FOR THE BREAD.\n", threadCheckoutReady);				// announce that the customer paid
			fprintf(stderr, "THE BAKER HANDED THE RECEIPT TO CUSTOMER%ld.\n", threadCheckoutReady);	// announce that the receipt is given to the customer
			fprintf(stderr, "CUSTOMER%ld GOT THE RECEIPT.\n", threadCheckoutReady);					// announce that the customer got the receipt
			checkoutCust++;
			sem_post(&semBaker);																	// release semaphore baker
			sem_post(&semCust);																		// release semaphore customer
		}
	}
}

/* main()
 * @function: 	initializes semaphores
 *				creates baker, customer, checkout threads
 * 				waits untill all threads are done and terminates program				
 * @param: none
 */
int main (int argc, char * argv[]) {
	fprintf(stderr, "+++++ THE BAKERY IS STARTING UP +++++\n");

	// initializing semaphores
	sem_init(&semBaker, 0, 1);
	sem_init(&semCust, 0, 1);
	sem_init(&semStore, 0, 5);

	// declaring integer thread returns values for errors
	int bakerReturnVal;
	int checkoutReturnVal;
	int custReturnVal;

	// creating threads
	pthread_t bakerThread;			
	pthread_t checkoutThread;		
	pthread_t customerThreads[10];	

	// baker thread
	bakerReturnVal = pthread_create(&bakerThread, NULL, bake, NULL);
	if (bakerReturnVal) {
		fprintf(stderr, "ERROR: return value from baker thread pthread_create is %d\n", bakerReturnVal);
		exit(-1);
	}

	// checkout thread
	checkoutReturnVal = pthread_create(&checkoutThread, NULL, checkout, NULL);
	if (checkoutReturnVal) {
		fprintf(stderr, "ERROR: return value from checkout thread pthread_create is %d\n", checkoutReturnVal);
		exit(-1);
	}

	// customer thread
	for (long i = 0; i < 10; i++) {
		custReturnVal = pthread_create(&(customerThreads[i]), NULL, customer, (void*) i);
		
		if (custReturnVal) {
			fprintf(stderr, "ERROR: return code from customer thread pthread_create is %d\n", custReturnVal);
			exit(-1);
		}
	}
	pthread_exit(NULL);	// terminates program
}
