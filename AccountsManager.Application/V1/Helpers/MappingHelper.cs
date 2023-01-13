using AccountsManager.Application.V1.Contracts.HelperContracts;
using AutoMapper;

namespace AccountsManager.Application.V1.Helpers
{
    public sealed class MappingHelper : IMappingExtension
    {
        private IMapper _mapper;

        public MappingHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination MapEntity<TSource, TDestination>(TSource entity)
        {
            return _mapper.Map<TDestination>(entity);
        }
        public TDestination MapEntiyInto<TSource, TDestination>(TSource source, TDestination destination){
            return _mapper.Map(source, destination);
        }
    }
}
