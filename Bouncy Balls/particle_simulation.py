'''
GUI controller for a particle simulation
Created Fall 2014
Updated Summer, 2015
Updated Fall, 2017

@author: smn4
@author: kvlinden
@editor: bj29
'''

from tkinter import *
from random import randint
from particle import *
from helpers import *

class ParticleSimulation:
    ''' Creates particle simulator '''
    
    def __init__(self, window):
        ''' Construct the particle simulator GUI '''
        self.window = window
        self.window.protocol('WM_DELETE_WINDOW', self.safe_exit)
        self.width = 400
        self.canvas = Canvas(self.window, bg='black',
                        width=self.width, height=self.width)
        self.canvas.pack()
        self.terminated = False
        self.p_list = []
        self.render()
        
        # create add particle button
        add_particle_button = Button(text='Add particle', command = self.add_particle)
        add_particle_button.pack(side = LEFT)
        
        # remove particle when clicked
        self.canvas.bind('<Button-1>', self.check_remove_particle)
        
    def add_particle(self):
        ''' Add particle to the list '''
        particle = Particle(randint(0, self.width), randint(0, self.width), randint(-25, 25), randint(-25, 25), 15, get_random_color())
        self.p_list.append(particle)
        
    def check_remove_particle(self, event):
        ''' Remove particle from the list ''' 
        copy = self.p_list[:]
        for particle in copy:
            if distance(particle.get_x(), particle.get_y(), event.x, event.y) < particle.get_radius():
                self.p_list.remove(particle)
                                       
    def safe_exit(self):
        ''' Turn off the event loop before closing the GUI '''
        self.terminated = True
        self.window.destroy()
        
    def render(self):
        ''' Move and render the particle if the program is not yet terminated '''
        if not self.terminated:
            self.canvas.delete(ALL)
            for particle in self.p_list:
                particle.move(self.canvas)
                particle.render(self.canvas)
                for particle2 in self.p_list:
                    particle.bounce(particle2)
            self.canvas.after(50, self.render)
            
            
            

if __name__ == '__main__':
    root = Tk()
    root.title('Particle Simulation')    
    app = ParticleSimulation(root)
    root.mainloop()
    