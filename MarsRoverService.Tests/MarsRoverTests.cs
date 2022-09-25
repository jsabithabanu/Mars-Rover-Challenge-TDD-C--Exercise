using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace MarsRoverService.Tests;

public class MarsRoverTests
{
    private Rover _rover_R01;
    private Rover _rover_R02;
    private Plateau _plateau;
    private CommandCenter _commandCenter;

    private const int _GRID_MAX_X_COORDINATE = 5;
    private const int _GRID_MAX_Y_COORDINATE = 5;

    [SetUp]
    public void Setup()
    {
        //Creating an instance and setting up the plateau grid size
        _plateau = new Plateau();
        _plateau.SetPlateauGridSize(_GRID_MAX_X_COORDINATE, _GRID_MAX_Y_COORDINATE);

        //Creating instances for 2 Rovers - "Rover R01", "Rover R02"
        _rover_R01 = new Rover(_plateau);
        _rover_R02 = new Rover(_plateau);

        //Creating instance for CommandCenter
        _commandCenter = new CommandCenter();        
    }

    [Test]
    public void Test_If_Plateau_Grid_Size_Is_Set_As_Expected()
    {
        _plateau.pointGridMax.X.Should().Be(_GRID_MAX_X_COORDINATE);
        _plateau.pointGridMax.Y.Should().Be(_GRID_MAX_Y_COORDINATE);
    }

    [Test]
    public void Test_If_Plateau_Grid_Size_Is_Valid()
    {
        var exception = Assert.Throws<ArgumentException>(() => _plateau.SetPlateauGridSize(-1, 0));
        Assert.That(exception.Message, Is.EqualTo("Please enter a valid plateau grid size."));

        var exceptionGridSize = Assert.Throws<ArgumentException>(() => _plateau.SetPlateauGridSize(0, 0));
        Assert.That(exceptionGridSize.Message, Is.EqualTo("The plateau grid size must be greater than (0, 0)"));
    }

    [Test]
    public void Test_If_Rover_Coordinates_And_Direction_Are_Set_As_Expected()
    {
        //1st Rover
        _rover_R01.SetRoverPosition(1, 2, 'N');
        _rover_R01.pointCurrent.X.Should().Be(1);
        _rover_R01.pointCurrent.Y.Should().Be(2);
        _rover_R01.CurrentDirectionFacing.Should().Be(Direction.North);

        //2nd Rover
        _rover_R02.SetRoverPosition(3, 3, 'E');
        _rover_R02.pointCurrent.X.Should().Be(3);
        _rover_R02.pointCurrent.Y.Should().Be(3);
        _rover_R02.CurrentDirectionFacing.Should().Be(Direction.East);
    }

    [Test]
    public void Test_If_Rover_Is_Placed_Within_The_Palteau_As_Expected()
    {
        //1st Rover
        var exceptionR01Position = Assert.Throws<ArgumentException>(() 
            => _rover_R01.SetRoverPosition(5, 6, 'N'));
        Assert.That(exceptionR01Position.Message, Is.EqualTo("Rover position should not be outside the plateau grid."));

        //2nd Rover
        var exceptionR02Position = Assert.Throws<ArgumentException>(()
            => _rover_R02.SetRoverPosition(7, 5, 'N'));
        Assert.That(exceptionR02Position.Message, Is.EqualTo("Rover position should not be outside the plateau grid."));

    }

    [Test]
    public void Test_If_Rover_Facing_Direction_After_Move_Is_Set_As_Expected()
    {   
        //1st Rover
        _rover_R01.SetRoverPosition(1, 2, 'N');
        _commandCenter.MoveRover(_rover_R01, "LMLMLMLMM");
        _commandCenter.CurrentDirectionFacing.Should().Be(Direction.North);

        //2nd Rover
        _rover_R02.SetRoverPosition(3, 3, 'E');
        _commandCenter.MoveRover(_rover_R02, "MMRMMRMRRM");
        _commandCenter.CurrentDirectionFacing.Should().Be(Direction.East);
    }

    [Test]
    public void Test_If_Rover_Moves_And_Returns_Position_And_Direction_As_Expected()
    {
        //1st Rover
        _rover_R01.SetRoverPosition(1, 2, 'N');
        _commandCenter.MoveRover(_rover_R01, "LMLMLMLMM").Should().Be("1 3 N");

        //2nd Rover
        _rover_R02.SetRoverPosition(3, 3, 'E');
        _commandCenter.MoveRover(_rover_R02, "MMRMMRMRRM").Should().Be("5 1 E");
    }
}