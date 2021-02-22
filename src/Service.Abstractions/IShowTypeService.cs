using ContractModels;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IShowTypeService
    {
        Task<ShowType> GetShowTypeByNameAsync(string name);
    }
}
