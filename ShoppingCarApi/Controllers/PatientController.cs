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
using ApiMedical.Auth;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    //[Route("api/[controller]")]
    public class PatientController : BaseController<Patient, PatientDto, IBaseService<Patient>>
    {
        private readonly IBaseService<Consultation> cons;
        private readonly IInvoiceNofactRepository invo;
        public PatientController(IBaseService<Patient> manager, IMapper Mapper, IBaseService<Consultation> _cons, IInvoiceNofactRepository _invo, IDoctorGuid _doctorGuid) : base(manager, Mapper,_doctorGuid)
        {
            cons = _cons;
            invo = _invo;
        }
        [HttpGet]
        [Route("GetPatientPaginated")]
        [AuthorizeMedical]
        public IActionResult GetPatientPaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.DoctorId == resource.DoctorId);

            collection = collection.Where(c => c.Name.Contains(resource.parameters)
            || c.Phone.Contains(resource.parameters) || c.Age.ToString()==resource.parameters);
            var dtos = _Mapper.ProjectTo<PatientDto>(collection);

            var result = PagedList<PatientDto>.Create(dtos, resource.PageNumber, resource.PageSize);
            foreach (var item in result)
            {
                item.ConsultantQty =
                    cons.FindByCondition(x => x.DoctorId == resource.DoctorId && x.PatientId == item.Id && x.IsActive)?.Count();
            }
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
        [Route("GetByUI")]
        [AuthorizeMedical]
        public IActionResult GetByUI(Guid id)
        {
            var data = _service.FindAll().Where(c => c.PatientGuid == id && c.IsActive).FirstOrDefault();
            return Ok(data);
        }
        [HttpGet("getByDoctor")]
        [EnableQuery()]
        public IActionResult GetByDoctor(int Id)
        {
            return Ok(_service.FindByCondition(x => x.DoctorId == Id).AsQueryable());
        }
        [HttpGet("allByDoctor")]
        [AuthorizeMedical]
        public IActionResult AllByDoctor(int Id)
        {
            return Ok(_service.FindByCondition(x => x.DoctorId == Id).OrderBy(C => C.Name).AsQueryable());
        }
        [HttpGet("byDoctorAndName")]
        [AuthorizeMedical]
        public IActionResult byDoctorAndName(int Id, string Patient)
        {
            if (Patient == null) Patient = "";
            return Ok(_service.FindByCondition(x => x.DoctorId == Id && x.Name.Contains(Patient)).OrderBy(C => C.Name).AsQueryable());
        }

        [HttpGet("GetInvoiceNoFact/{Id}")]
        [AuthorizeMedical]
        public IActionResult GetByInvoice(int Id)
        {
            try
            {
                return Ok(invo.GetByatient(Id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
           
        }
    }
}