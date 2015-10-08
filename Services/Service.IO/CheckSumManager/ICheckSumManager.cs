using Service.IO.Buffers;

namespace Service.IO.CheckSumManager
{
    public interface ICheckSumManager
    {
        void Calculate(IOBuffer b);
        bool IsValid(IOBuffer b);
    }
}
