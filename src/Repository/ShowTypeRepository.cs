using System.Threading.Tasks;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository
{
    public class ShowTypeRepository : IShowTypeRepository
    {
        private readonly ShowContext _context;

        public ShowTypeRepository(ShowContext context)
        {
            _context = context;
        }

        public async Task<ShowType> GetShowTypeByNameAsync(string name)
        {
            return await _context.ShowTypes
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() || x.Code.ToLower() == name.ToLower());
        }
    }
}
