using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace MarsRoverService.Tests;

public class MarsRoverTests
{
    private Rover _rover;
    private Plateau _plateau;
    private CommandCenter _commandCenter;

    [SetUp]
    public void Setup()
    {
        _rover = new Rover();
        _plateau = new Plateau();
        _commandCenter = new CommandCenter();        
    }

    [Test]
    public void Test_If_Plateau_Grid_Size_Is_Set_As_Expected()
    {
        _plateau.SetPlateauGridSize(1, 1);
        _plateau.GridMaxXCoordinate.Should().Be(1);
        _plateau.GridMaxYCoordinate.Should().Be(1);

        _plateau.SetPlateauGridSize(5, 5);
        _plateau.GridMaxXCoordinate.Should().Be(5);
        _plateau.GridMaxYCoordinate.Should().Be(5);

        _plateau.SetPlateauGridSize(3, 4);
        _plateau.GridMaxXCoordinate.Should().Be(3);
        _plateau.GridMaxYCoordinate.Should().Be(4);

        var exception = Assert.Throws<ArgumentException>(() => _plateau.SetPlateauGridSize(-1, 0));
        Assert.That(exception.Message, Is.EqualTo("Please enter a valid plateau grid size."));

        var exceptionGridSize = Assert.Throws<ArgumentException>(() => _plateau.SetPlateauGridSize(0, 0));
        Assert.That(exceptionGridSize.Message, Is.EqualTo("The plateau grid size must be greater than (0, 0)"));
    }
}