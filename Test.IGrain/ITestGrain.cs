using Orleans;

namespace Test.IGrains.Interfaces
{
    public interface ITestGrain : IGrainWithStringKey
    {
        Task AddInstruction(int value);
        Task<int> GetNextInstruction();
        Task<int> GetInstructionCount();
        Task<string> GetAllInstructions();
    }
}
