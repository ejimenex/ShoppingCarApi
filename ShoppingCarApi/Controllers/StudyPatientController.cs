using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Dtos;
using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class StudyPatientController : BaseController<StudyPatient,StudyPatientDto, IBaseService<StudyPatient>>
    {
        public StudyPatientController(IBaseService<StudyPatient> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
       [HttpGet]
       [Route("GetByConsultation")]
       public IActionResult GetByDr(int ConsultationId, Guid Dr){
           try
           {
         var result=_service.FindAll().Where(c=> c.ConsultationId==ConsultationId && c.DoctorGuid==Dr);
                var newResult = _Mapper.Map<IEnumerable<StudyPatientDto>>(result);
         return Ok(newResult);
           }
           catch(Exception ex){
        return BadRequest(ex.Message);
           }
          
       }
       
    }
   
}