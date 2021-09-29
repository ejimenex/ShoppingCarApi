using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Dtos;
using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class PatientFormController : BaseController<PatientForm,PatientFormDto, IBaseService<PatientForm>>
    {
        private IBaseService<MedicalForm> medi;
        public PatientFormController(IBaseService<MedicalForm> _medi,IBaseService<PatientForm> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {
            medi = _medi;
        }
        [HttpGet]
        [Route("filtered/{id}/{patientid}")]
        public IActionResult GetFiltered(Guid id, int patientid)
        {
            var data = _service.FindByCondition(c => c.DoctorGuid == id && c.PatientId == patientid)
                .Include(c=> c.MedicalForm)
                .OrderBy(c=> c.MedicalForm.NoOrder);
            var result = _Mapper.Map<IEnumerable <PatientFormDto>>(data);
            return Ok(result
                );
        }
        [HttpGet]
        [Route("GetFilteredByDoctor/{id}")]
        public IActionResult GetFilteredByDoctor(Guid id)
        {
            try
            {
                var data = medi.FindByCondition(c => c.DoctorGuid == id && c.IsActive)
                              .OrderBy(c => c.NoOrder);
                var result = _Mapper.Map<IEnumerable<MedicalFormDto>>(data);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
          
        }
    }
}