using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs.Abstraction
{
    public interface IJob
    {
        public Task Execute();
    }
}
