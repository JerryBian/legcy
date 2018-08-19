namespace Laobian.Infrastuture.Interface.Service
{
    public interface ITaskBase<T>
    {
        void Add(T item);

        void DoWork();
    }
}
