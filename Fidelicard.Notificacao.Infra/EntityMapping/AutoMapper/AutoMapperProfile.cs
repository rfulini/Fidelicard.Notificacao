using AutoMapper;
using Fidelicard.Notificacao.Core.Models;
using Fidelicard.Notificacao.Infra.EntityMapping.DTO;

namespace Fidelicard.Notificacao.Infra.EntityMapping.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NotificacoesDTO, Notificacoes>().ReverseMap();
        }
    }
}
