namespace DIContainer.Framework.Extensions
{
    public interface IFactory<T>
    {
        T Create();
    }
}