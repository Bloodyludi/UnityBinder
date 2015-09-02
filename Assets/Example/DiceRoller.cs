using Container.Framework;

namespace Container.Example
{
    public interface IDiceRoller
    {
        IDiceRoller Roll();

        string Result();
    }

    public class DiceRoller : IDiceRoller
    {
        [Inject]
        public IRandom random { get; set; }

        private int result;
        private const int pips = 6;

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