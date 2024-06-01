using Orleans;
using Test.IGrains.Interfaces;

namespace Test.Grains
{
    public class TestGrain : Grain, ITestGrain
    {
        private Queue<int> instructions = new Queue<int>();
        public Task AddInstruction(int value)
        {
            this.instructions.Enqueue(value);
            return Task.CompletedTask;
        }

        public Task<int> GetInstructionCount()
        {
            return Task.FromResult(this.instructions.Count);
        }

        public Task<int> GetNextInstruction()
        {
            if (this.instructions.Count == 0)
            {
                return Task.FromResult<int>(0);
            }
            var instruction = this.instructions.Dequeue();
            return Task.FromResult(instruction);
        }

        public Task<string> GetAllInstructions()
        {
            var instructions = string.Join(",", this.instructions);
            return Task.FromResult(instructions);
        }
    }
}
