'''
Model a single particle
Created Fall 2014
Updated Summer, 2015
Updated Fall, 2017

@author: smn4
@author: kvlinden
@editor: bj29
'''

import math
from helpers import *


DAMPENING_FACTOR = 0.88
 

class Particle:
    ''' Particle models a single particle that may be rendered to a canvas '''

    def __init__(self, x = 50, y = 50, velX = 10, velY = 15, radius = 15, color = '#663977'):
        ''' Constructor '''
        self._x = x
        self._y = y
        self._velX = velX
        self._velY = velY
        self._radius = radius
        self._color = color
        
    def render(self, canvas):
        '''Receive a canvas and draw circle '''
        canvas.create_oval(self._x - self._radius,
                        self._y - self._radius,
                        self._x + self._radius,
                        self._y + self._radius,
                        fill = self._color)
        
    def move(self, canvas):
        ''' Receive a canvas and move the particle '''
        self._x += self._velX
        self._y += self._velY
        if (self._x + self._radius > canvas.winfo_reqwidth() and self._velX > 0) or (self._x - self._radius < 0 and self._velX < 0):
            self._velX = -self._velX
        if (self._y + self._radius > canvas.winfo_reqwidth() and self._velY > 0) or (self._y - self._radius < 0 and self._velY < 0):
            self._velY = -self._velY
            
    def hits(self, other):
        if (self == other):
            # I can't collide with myself.
            return False
        else:
            # Determine if I overlap with the target particle.
            return (self._radius + other.get_radius()) >= distance(self._x, self._y, other.get_x(), other.get_y())
        
 
    def bounce(self, target):
        ''' This method modifies this Particle object's velocities based on its
            collision with the given target particle. It modifies both the magnitude
            and the direction of the velocities based on the interacting magnitude
            and direction of particles. It only changes the velocities of this
            object; an additional call to bounce() on the other particle is required
            to implement a complete bounce interaction.
      
            The collision algorithm is based on a similar algorithm published by K.
            Terzidis, Algorithms for Visual Design.
      
            target  the other particle
         '''
        if self.hits(target):
            angle = math.atan2(target.get_y() - self._y, target.get_x() - self._x)
            targetX = self._x + math.cos(angle) * (self._radius + target.get_radius())
            targetY = self._y + math.sin(angle) * (self._radius + target.get_radius())
            ax = targetX - target.get_x()
            ay = targetY - target.get_y()
            self._velX = (self._velX - ax) * DAMPENING_FACTOR
            self._velY = (self._velY - ay) * DAMPENING_FACTOR
            
    def get_x(self):
        ''' Return x '''
        return self._x
    
    def get_y(self):
        ''' Return y '''
        return self._y
    
    def get_radius(self):
        ''' Return radius '''
        return self._radius