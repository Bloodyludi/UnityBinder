namespace Container.Example
{
    public interface IRandom
    {
        int Range(int min, int max);
    }

    public class UnityRandom : IRandom
    {
        public int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

    public class SystemRandom : IRandom
    {
        private readonly System.Random rng;

        public SystemRandom ()
        {
            rng = new System.Random();
        }

        public int Range(int min, int max)
        {
            return rng.Next(min, max);
        }
    }
}