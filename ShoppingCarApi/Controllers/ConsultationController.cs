using ApiMedical.Auth;
using ApiMedical.Common.Filter;
using AutoMapper;
using BussinesLogic.Interface;
using BussinesLogic.Interface.ListInterface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    
    public class ConsultationController : BaseController<Consultation,ConsultationDto, IBaseService<Consultation>>
    {
        readonly IConsultationList consu;
        public ConsultationController(IBaseService<Consultation> manager, IMapper Mapper, IConsultationList _consu, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {
            consu = _consu;
        }
        [HttpGet]
        [AuthorizeMedical]
        [Route("GetConsultationPaginated")]
        public IActionResult GetMConsultationPaginated(ConsultationFilter resource)
        {
            var pagination = consu.GetPaged(resource);
            return Ok(pagination);
        }
        [HttpPost]
        [AuthorizeMedical]
        [Route("GetConsultationMother")]
        public IActionResult GetConsultationMother([FromBody]ConsultationDto resource)
        {
            var data = consu.GetConsultationMother(resource);
            return Ok(data);
        }
    }
}