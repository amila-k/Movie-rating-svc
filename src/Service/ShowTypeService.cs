using System.Threading.Tasks;
using AutoMapper;
using ContractModels;
using Repository.Abstractions;
using Service.Abstractions;

namespace Service
{
    public class ShowTypeService : IShowTypeService
    {
        private readonly IShowTypeRepository _showTypeRepository;
        private readonly IMapper _mapper;

        public ShowTypeService(IShowTypeRepository showTypeRepository, IMapper mapper)
        {
            _showTypeRepository = showTypeRepository;
            _mapper = mapper;
        }
        public async Task<ShowType> GetShowTypeByNameAsync(string name)
        {
            var showType = await _showTypeRepository.GetShowTypeByNameAsync(name);
            return _mapper.Map<ShowType>(showType);
        }
    }
}
