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

    private const int _gridMaxXCoordinate = 5;
    private const int _gridMaxYCoordinate = 5;

    [SetUp]
    public void Setup()
    {
        _plateau = new Plateau();
        //_plateau.SetPlateauGridSize(_gridMaxXCoordinate, _gridMaxYCoordinate);

        _rover_R01 = new Rover();
        _rover_R02 = new Rover();
        
        _commandCenter = new CommandCenter();        
    }

    [Test]
    public void Test_If_Plateau_Grid_Size_Is_Set_As_Expected()
    {
        _plateau.SetPlateauGridSize(5, 5);
        _plateau.GridMaxXCoordinate.Should().Be(5);
        _plateau.GridMaxYCoordinate.Should().Be(5);

        _plateau.SetPlateauGridSize(3, 4);
        _plateau.GridMaxXCoordinate.Should().Be(3);
        _plateau.GridMaxYCoordinate.Should().Be(4);

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
        _plateau.SetPlateauGridSize(5, 5);
        _rover_R01.SetRoverPosition(_plateau.GridMaxXCoordinate, _plateau.GridMaxYCoordinate, 1, 1, 'N');
        _rover_R01.CurrentXCoordinate.Should().Be(1);
        _rover_R01.CurrentYCoordinate.Should().Be(1);
        _rover_R01.CurrentDirectionFacing.Should().Be(Direction.North);
    }

    [Test]
    public void Test_If_Rover_Is_Placed_Within_The_Palteau_As_Expected()
    {
        _plateau.SetPlateauGridSize(5, 5);
        var exceptionRoverPosition = Assert.Throws<ArgumentException>(() 
            => _rover_R01.SetRoverPosition(_plateau.GridMaxXCoordinate, _plateau.GridMaxYCoordinate, 5, 6, 'N'));
        Assert.That(exceptionRoverPosition.Message, Is.EqualTo("Rover position should not be outside the plateau grid."));

    }

    [Test]
    public void Test_If_Rover_Facing_Direction_After_Move_Is_Set_As_Expected()
    {
        //1st Rover
        _rover_R01.MoveRover(5, 5, 1, 2, 'N', "LMLMLMLMM");
        _rover_R01.CurrentDirectionFacing.Should().Be(Direction.North);

        //2nd Rover
        _rover_R02.MoveRover(5, 5, 3, 3, 'E', "MMRMMRMRRM");
        _rover_R02.CurrentDirectionFacing.Should().Be(Direction.East);
    }

    [Test]
    public void Test_If_Rover_Moves_And_Returns_Position_And_Direction_As_Expected()
    {
        //1st Rover 
        _rover_R01.MoveRover(5, 5, 1, 2, 'N', "LMLMLMLMM").Should().Be("1 3 N");

        //2nd Rover
        _rover_R02.MoveRover(5, 5, 3, 3, 'E', "MMRMMRMRRM").Should().Be("5 1 E");        
    }
}