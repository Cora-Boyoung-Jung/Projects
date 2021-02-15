''' Homework for CS108 Homework108
created Oct. 3, 2017
Homework04
@author: Cora Jung (bj29)
'''

#create variables
month = int(input('Enter a month number: '))
day = int(input('Enter a day: '))
year = int(input('Enter a year: '))

#if-else statement for leap year
if ((year%400 == 0) or ((year%4 == 0) and (year%100 != 0))):
    print(year, "is a leap year")
    leap_year = True
else:
    print(year, "is not a leap year")
    leap_year = False
  
#legal dates
message1 = str(month)+'/'+str(day)+'/'+str(year) + " is a legal date."
message2 = str(month)+'/'+str(day)+'/'+str(year) + " is not a legal date."
if (month != 2):
    if (month in [1,3,5,7,8,10,12]):
        if (1 <= day) and (day <= 31):
            print(message1)
        else:
            print(message2)
    elif (month in [4,6,9,11]):
        if (1 <= day) and (day <= 30):
            print(message1)
        else:
            print(message2)
elif(month == 2):
    if leap_year ==True:
        if (1 <= day) and (day <= 29):
            print(message1) 
        else:
            print(message2)
    else:
        if (1 <= day) and (day <= 28):
            print(message1)
        else:
            print(message2)

#calculating the day number for given date
dayNum = (31*(month -1) + day)
if month <= 2:
    if month == 1 and 1<=day<=31:
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))
    elif month ==2 and leap_year == False and 1<=day<=28:
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))
    elif month ==2 and leap_year == True and 1<=day<=29:
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))
elif month >2:
    if (month in [1,3,5,7,8,10,12]) and (1 <= day) and (day <= 31) and (leap_year == False):
        dayNum = dayNum -((4*month+23)/10)
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))
    elif (month in [1,3,5,7,8,10,12]) and (1 <= day) and (day <= 31) and (leap_year == True):
        dayNum = dayNum -((4*month+23)/10) +1
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))
    elif ((month in [4,6,9,11]) and 1 <= day) and (day <= 30) and (leap_year == False):
        dayNum = dayNum -((4*month+23)/10)
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))
    if ((month in [4,6,9,11]) and 1 <= day) and (day <= 30) and (leap_year == True):
        dayNum = dayNum -((4*month+23)/10) +1
        print("The date", str(month)+'/'+str(day)+'/'+str(year), "has day number", round(dayNum))



    

    