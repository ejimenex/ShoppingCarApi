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

    public class DoctorOfficeController : BaseController<DoctorOffice,DoctorOfficeDto, IBaseService<DoctorOffice>>
    {
        public DoctorOfficeController(IBaseService<DoctorOffice> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
        [HttpGet]
        [Route("GetOfficePaginated")]
        [AuthorizeMedical]
        public IActionResult  GetOfficePaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.DoctorId == resource.DoctorId);

            collection = collection.Where(c => c.Name.Contains(resource.parameters)
            || c.MedicalCenter.Name.Contains(resource.parameters));
            var dtos = _Mapper.ProjectTo<DoctorOfficeDto>(collection);
            var result =  PagedList<DoctorOfficeDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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
            return Ok( pagination);
        }
        [HttpGet("getByDoctor")]
        public IActionResult GetByDoctor(int Id)
        {
            try
            {
                var data = _service.FindByCondition(x => x.DoctorId == Id).Select(c=> new DoctorOfficeDto{
                DoctorName=$"{c.Doctor.Treament} { c.Doctor.Name} {c.Doctor.SurName}",
                Specification=c.Specification,
                UrlMapsAddress=c.UrlMapsAddress,
                MedicalCenterName=c.MedicalCenter.Name,
                Name=c.Name,
                Id=c.Id
                }).AsQueryable();
                var ret = _Mapper.Map<IEnumerable<DoctorOfficeDto>>(data);
                return Ok(ret);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        
        }
    }
}