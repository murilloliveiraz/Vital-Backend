using Application.DTOS.Consulta;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class DocumentoProfile : Profile
    {
        public DocumentoProfile()
        {
            CreateMap<Documento, AdicionarDocumentoRequestContract>().ReverseMap();
            CreateMap<Documento, AdicionarDocumentoResponseContract>().ReverseMap();
        }
    }
}
