'''
Created on Nov 28, 2017

@author: bj29
'''

from tkinter import *
from temperature import *

class Gui:

    def __init__(self, window):
        '''
        Create label and button for temperature conversion
        '''
        self._window = window
        self._temp = Temperature()
        self._fahrenheit = StringVar()
        self._result = StringVar()
        
        # create title for window
        window.title("Temperature Conversion")
        
        # create label for temperature in fahrenheit
        fahrenheit_label = Label(window, text="Temperature (in Fahrenheit):")
        fahrenheit_label.grid(row=0, column=0, sticky=E)
        fahrenheit_entry = Entry(window, width=6, textvariable=self._fahrenheit)
        fahrenheit_entry.grid(row=0, column=1, sticky=W)
        
        # create conversion button 
        conv_button = Button(text='Convert to Celsius', command = self.do_conversion)
        conv_button.grid(row = 1, column = 0, sticky = E)

        # create result
        self.result_label = Label(window, textvariable=self._result, width=6)        
        self.result_label.grid(row=1, column=1, sticky=W)
        
        # generate message
        self._message = StringVar()
        self._message.set("Welcome!")
        message_label = Label(window, textvariable = self._message )
        message_label.grid(row=2, column=0, sticky=W)

        
    def do_conversion(self):
        try:
            result = self._temp.temperature(self._fahrenheit.get())
            self._result.set(result)
            self._message.set("Welcome!")
        except Exception as e:
            self._message.set(str(e))
        
if __name__ == '__main__':
    root = Tk()
    root.title('Calculator')
    app = Gui(root)
    root.mainloop()

