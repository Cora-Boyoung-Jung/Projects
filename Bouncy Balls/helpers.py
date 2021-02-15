'''
Helper functions for lab 12
Created Fall 2014
Updated Summer, 2015

@author: smn4
@author: kvlinden
'''
from random import randint

def get_random_color():
    ''' Generate random color intensities for red, green & blue and convert them to hex. '''
    return '#{:02X}{:02X}{:02X}'.format(randint(0,255), randint(0,255), randint(0,255))

def distance(x1, y1, x2, y2):
    ''' Compute the distance between two points. '''
    return ((x1 - x2) ** 2 + (y1 - y2) ** 2)**0.5
