using NUnit.Framework;
using NSubstitute;

namespace DIContainer.Example.UnitTests
{
    [TestFixture]
    public class GuessANumberTests
    {
        [Test]
        public void ShouldReturnTrueForRightGuess()
        {
            var mockRandom = Substitute.For<IRandom>();
            var guessANumber = new GuessANumber(mockRandom);
            var expected = 6;
            mockRandom.Range(Arg.Any<int>(), Arg.Any<int>()).Returns(expected);
            guessANumber.Roll();

            Assert.True(guessANumber.MakeGuess(expected));
        }
    }

    public class GuessANumber
    {
        private readonly IRandom random;
        private int result;
        private const int MAX = 6;

        public GuessANumber(IRandom random)
        {
            this.random = random;
        }

        public GuessANumber Roll()
        {
            result = random.Range(1, MAX);
            return this;
        }

        public bool MakeGuess(int guess)
        {
            return result == guess;
        }
    }
}

