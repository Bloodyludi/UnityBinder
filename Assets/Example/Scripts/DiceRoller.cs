using JetBrains.Annotations;

namespace DIContainer.Example
{
    [UsedImplicitly]
    public class DiceRoller : IDiceRoller
    {
        private const int Pips = 6;

        private readonly IRandom random;
        private int result;

        public DiceRoller(IRandom random)
        {
            this.random = random;
        }

        public IDiceRoller Roll()
        {
            result = random.Range(1, Pips);

            return this;
        }

        public string Result()
        {
            return result.ToString();
        }
    }
}