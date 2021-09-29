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
using ApiMedical.Common.Pagination;
using ApiMedical.Common.Filter;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class PrescriptionController : BaseController<Prescription,PrescriptionDto, IBaseService<Prescription>>
    {
        
        public PrescriptionController(IBaseService<Prescription> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
        [HttpGet]
        [Route("GetPrescriptionbyDoctor")]

        public IActionResult GetPrescriptionbyDoctor(PrescriptionFilter resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindByCondition
                (x => x.DoctorId==resource.DoctorId && x.PatientId==resource.param)
                .OrderByDescending(v=> v.Id);

            if (collection.Count() == 0)
                return null;
            var dtos = _Mapper.ProjectTo<PrescriptionDto>(collection);
            var result = PagedList<PrescriptionDto>.Create(dtos, resource.PageNumber, resource.PageSize);
            var pagination = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                currentPage = result.CurrentPage,
                totalPage = result.TotalPages,
                HasNext = result.HasNext,
                HasPrevious = result.HasPrevious,
                data = result
            };
            return Ok(pagination);
        }
        [HttpGet]
        [Route("GetPrescriptionbyConsultation/{patient}/{id}/{dr}")]

        public IActionResult GetPrescriptionbyConsultation(int patient,int id,int dr)
        {
            var collection = _service.FindByCondition
                (x => x.DoctorId == dr && x.PatientId == patient && x.ConsultationId==id)
                .OrderByDescending(v => v.Id);

            return Ok(collection);
        }

        [HttpGet("{id}/{doctorId}")]
        public IActionResult GetByDoctor(int id, int doctorId)
        {
            try
            {
                var result = _service.FindByCondition(x => x.PatientId == id && x.DoctorId == doctorId).AsQueryable();
                var dto=_Mapper.Map<IEnumerable<PrescriptionDto>>(result);
                return Ok(dto);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
}