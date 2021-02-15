'''
Convert Fahrenheit to Celsius
Created on Nov 28, 2017
@author: bj29
'''

class Temperature: 
    
    def __init__ (self):
        ''' initialize temperature to zero '''
        self._temperature = 0.0
        
    def temperature(self, fahrenheit=0):
        fahrenheit = float(fahrenheit)
        celsius = float((fahrenheit-32)/1.8)
        if celsius >= -273.15:
            return ("%.1f" % celsius)
        else:
            raise ValueError("no temperature should be below absolute zero")
                
        
if __name__ == '__main__':
    temp = Temperature()
    print(temp.temperature(212))