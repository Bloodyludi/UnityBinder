namespace DIContainer.Example
{
    public class DiceRoller : IDiceRoller
    {
        private IRandom random;
        private int result;
        private const int pips = 6;

        public DiceRoller(IRandom random)
        {
            this.random = random;
        }

        public IDiceRoller Roll()
        {
            result = random.Range(1, pips);

            return this;
        }

        public string Result ()
        {
            return result.ToString();
        }
    }
}