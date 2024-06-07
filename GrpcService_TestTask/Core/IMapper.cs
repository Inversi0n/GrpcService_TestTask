namespace GrpcService_TestTask.Core
{
    public interface IMapper<T1, T2>
    {
        T1 Cast(T2 value);
        T2 Cast(T1 value);
    }
}
