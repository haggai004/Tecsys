using AutoMapper;
using System.Collections.Generic;
using Unity;

namespace Tecsys.Retail.TypeMapping
{
    public class TypeMapper : ITypeMapper
    {
        private readonly IMapper _mapper;
        IUnityContainer _container;
        MapperConfiguration _mapperConfig;

        public TypeMapper(IUnityContainer container,IMapper mapper)
        {
            _container = container;
            _mapperConfig = container.Resolve<MapperConfiguration>();
            _mapper = mapper;
        }


        public TTo Map<TFrom, TTo>(TFrom tfrom)
        {
            return _mapper.Map<TTo>(tfrom);
        }

        public List<TTo> Map<TFrom, TTo>(List<TFrom> tfrom)
        {
            return _mapper.Map<List<TTo>>(tfrom);
        }

        public IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }
    }
}
