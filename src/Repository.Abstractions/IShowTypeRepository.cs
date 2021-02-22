using DomainModels;
using System.Threading.Tasks;

namespace Repository.Abstractions
{
    public interface IShowTypeRepository
    {
        Task<ShowType> GetShowTypeByNameAsync(string name);
    }
}
