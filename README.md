# Mars Rover Challenge TDD CSharp Exercise
## Mars Rover Challenge - TDD - C# - Exercise 

Based on the Mars Rover Kata requirements, this project is completely developed using TDD approach and AAA model. This can be checked only by running the tests. No console input is included.

### Brief description of the requirements:

- It is assumed that the plateau is a square or rectangular grid.
- Representation of a Rover’s Position on the Plateau
- The Plateau is divided into a grid. A Rover’s position is represented by x and y co-ordinates and the letters N, S, W, E to represent North,
- South, West, East (the four cardinal compass points) respectively. Enum is used in the project to represent the directions.
#### Example
0 0 N
- This means the Rover is at the bottom-left corner facing in the North direction.

### Instructing a Rover to Move Around the Plateau
- To move a Rover around the Plateau, a string of letters is sent to a Rover. L Spins the Rover 90 degrees Left without moving from the current coordinate point. R Spins the Rover 90 degrees Right without moving from the current coordinate point. M Moves the Rover forward by one grid point, maintaining the same heading (i.e. from where the Rover is facing (its orientation)).

N.B. Assume that the square directly North from (x, y) is (x, y+1).

#### First Line of Input to the Program
- The first line inputted into the program represents the upper-right coordinates of the Plateau.
5 5
- This Plateau has maximum (x, y) co-ordinates of (5, 5). (N.B.) Assume that the lower-left coordinates is (0, 0).
- Subsequent Lines of Input into the Program - Input to Rovers
- This represents the instructions to move the rovers.
- Each rover receives two lines of input.

#### First Line of Input to a Rover
- The Rover’s position is represented by two integers representing the X and Y coordinates and a letter representing where the Rover is facing (its
orientation).
1 2 N

#### Second Line of Input to a Rover
- A string of letters representing the instructions to move the Rover around the Plateau.

#### Movement Rules
- Rovers move sequentially, this means that the first Rover needs to finish moving first before the next one can move.
Output
- For each Rover, the output represents its final position (final coordinates and where it is facing).

## Description of the TDD approach and the development of the Kata exercise:

- Firstly, the UML representation of the exercise is done using class diagram, based on the requirements. (The class diagram got changed eventually in parallel with testing and coding.)

### The final version of the class diagram contains:

#### Classes:
- Plateau
- Rover
- CommandCenter

#### Interfaces:
- IPlateau
- IRover

#### Enums:
- Direction
- RoverCommand

The class Plateau implement the interface IPlateau and the class Rover implement the interface IRover. 
The IPlateau interface was added as there are chances to add different shaped plateaus in future. This will be helpful in extending the kata exercise.
The IRover interface was added as there are chances to add different vehicles on the plateau in future. This will be helpful in extending the kata exercise.

### TDD - Test Driven Development of the Mars Rover Kata 

#### Step 1
- First, the plateau's grid size is set up and tested. The plateau can be a square or a rectangle.
- It is validated to check whether the plateau grid size entered is valid and also the plateau grid size is greater than starting point (0,0). Exception messages are shown to the user for the validations.
- Then, the Rover coordinates and direction are set and tested. The Rover position and direction should be set properly in the appropriate variables.
- This is validated to check if the Rover is placed inside the plateau.

#### Step 2
- Then developed and tested the Rover's facing direction after a move, based on the input Move command instructions - is set as expected. 
- If the Move instructions make the Rover move out of the plateau, the Rover can follow the instruction and move until the edge of the plateau and stop. It will not move further out of the plateau. An exception message is shown to the user including the current position and direction of the Rover. For example, "Rover cannot move outside the plateau. It now stands at the position (0, 3) facing West. Please modify the instructions."

#### Step 3
- The CommandCenter is the class that controls all the commands and executes the Rovers. Now, to integrate the concepts with the Command Center, it is developed and tested whether a Plateau can be added by the CommandCenter using Add Plateau command i.e. AddPlateau() method.
- It is developed and tested whether a Rover can be added by the CommandCenter using AddRover() method. It adds/sets up the plateau first and then the Rover.

#### Step 4
- Only certain number of Rovers could be landed on the Plateau. As they are assumed to move 1 point on the grid each time, there should be atleast 1 space to move for each Rover on the grid. A special formula is created to calculate the possible number of Rovers on the plateau. It is tested whether the possible number of Rovers is calculated correctly according to the Plateau grid size input.

#### Step 5
- It is then developed and tested whether a Rover can be added and moved by the CommandCenter successfully and the current location after of the Rover after moving is returned as the output.

#### Step 6
- Then, it is developed and tested whether 2 or more number of Rovers could be added and moved by the CommandCenter but within the no. of Rovers limit set by the special formula.
- Their coordinates and direction facing are tested via the returned outputs after move command is executed.

#### Step 7
- When more than one Rover is on the plateau, there is a possibility for collision. So it is developed and tested for collision if more than one Rover is added to the plateau. If the Rover01 moves and if it's next moving coordinate as per the move instruction has got Rover02 occupied, then Rover01 will stop moving just before the Rover02's coordinate and the exception message is given to user including the current position and direction facing of the Rover. For example, "Rover cannot move further. There is a collision ahead. It now stands at the position (5, 2) facing South. Please modify the instructions."

### Conclusion

Thus all the basic scenarios are covered and tested thoroughly for the Rovers to move around the plateau using the Test Driven Development approach.

### Extendable Features
This project is still extendable to add different shaped plateaus via the IPlateau interface and to add different types of vehicles other than Rovers via the IRover interface. Looking forward to include the console input code in Program.cs in future to see the project running interactively.





