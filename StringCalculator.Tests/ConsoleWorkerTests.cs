using Moq;
using System;
using Xunit;

namespace StringCalculator.Tests
{
    public class ConsoleWorkerTests
    {
        private readonly Mock<Calculator> _mockCalculator;
        private readonly Mock<CustomConsole> _mockConsole;
        private readonly ConsoleWorker _consoleWorker;

        public ConsoleWorkerTests()
        {
            _mockCalculator = new Mock<Calculator>();
            _mockConsole = new Mock<CustomConsole>();
            _consoleWorker = new ConsoleWorker(_mockCalculator.Object, _mockConsole.Object);
        }

        [Fact]
        public void Run_OnStart_DisplayInitialMassageOnce()
        {
            //arrange
            _mockConsole.SetupSequence(a => a.ReadLine())
                                        .Returns(string.Empty);
            
            //act
            _consoleWorker.Run();

            //assert
            _mockConsole.Verify(mock => mock.WriteLine("Enter comma separated numbers (enter to exit)"), Times.Once);
        }

        [Fact]
        public void Run_InputEmptyStringAtSecondTime_DisplayResultOnce()
        {
            //arrange
            _mockConsole.SetupSequence(a => a.ReadLine())
                                       .Returns("1,2")
                                       .Returns(string.Empty);
            _mockCalculator.Setup(a => a.Add(It.IsAny<string>()))
                                       .Returns(3);

            //act
            _consoleWorker.Run();

            //assert
            _mockConsole.Verify(mock => mock.WriteLine("Result: 3"), Times.Once);
        }

        [Fact]
        public void Run_InputNotEmptyStringMoreThanOnce_DisplayResultMoreThanOnce()
        {
            //arrange
            _mockConsole.SetupSequence(a => a.ReadLine())
                                       .Returns("1")
                                       .Returns("2")
                                       .Returns(string.Empty);
            _mockCalculator.Setup(a => a.Add(It.IsAny<string>()))
                                       .Returns(3);

            //act
            _consoleWorker.Run();

            //assert
            _mockConsole.Verify(mock => mock.WriteLine("Result: 3"), Times.AtLeast(2));
        }

        [Fact]
        public void Run_EmptyInputStringAtSecondTime_DisplayAdditionalMessageOnce()
        {
            //arrange
            _mockConsole.SetupSequence(a => a.ReadLine())
                                       .Returns("1")
                                       .Returns(string.Empty);

            //act
            _consoleWorker.Run();

            //assert
            _mockConsole.Verify(mock => mock.WriteLine("You can enter other numbers(enter to exit)?"), Times.Once);
        }

        [Fact]
        public void Run_NotEmptyInputString_DisplayAdditionalMessageMoreThanOnce()
        {
            //arrange
            _mockConsole.SetupSequence(a => a.ReadLine())
                                       .Returns("1")
                                       .Returns("2")
                                       .Returns(string.Empty);

            //act
            _consoleWorker.Run();

            //assert
            _mockConsole.Verify(mock => mock.WriteLine("You can enter other numbers(enter to exit)?"), Times.AtLeast(2));
        }

        [Fact]
        public void Run_InputStringContainsNegativeNumbers_DisplayArgumentExceptionMessage()
        {
            //arrange
            _mockConsole.SetupSequence(a => a.ReadLine())
                                       .Returns("-1")
                                       .Returns("2")
                                       .Returns(string.Empty);

            _mockCalculator.SetupSequence(a => a.Add(It.IsAny<string>()))
                                       .Throws(new ArgumentException("negatives not allowed - -1"));

            //act
            _consoleWorker.Run();

            //assert
            _mockConsole.Verify(mock => mock.WriteLine("negatives not allowed - -1"), Times.Once);
        }
    }
}
