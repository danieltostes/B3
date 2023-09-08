using AutoMapper;
using B3.Aplicacao.Modelos;
using B3.Dominio.Entidades;

namespace B3.Infraestrutura.CrossCuting.Mapper
{
    /// <summary>
    /// Configuração do mapeamento de entidades e dtos
    /// </summary>
    public static class AutoMapperConfig
    {
        private static IMapper? mapper = null;

        public static IMapper Mapper()
        {
            if (mapper == null)
            {
                var config = new MapperConfiguration(conf =>
                {
                    conf.CreateMap<RentabilidadeCdb, RentabilidadeCdbDto>().ReverseMap();
                });
                mapper = config.CreateMapper();
            }
            return mapper;
        }
    }
}
