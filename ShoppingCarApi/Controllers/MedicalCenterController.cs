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
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class MedicalCenterController : BaseController<MedicalCenter,MedicalCenterDto, IBaseService<MedicalCenter>>
    {
        public MedicalCenterController(IBaseService<MedicalCenter> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
        [HttpGet]
        [Route("GetMedicalCenterPaginated")]
        public IActionResult GetMedicalCenterPaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.Name.Contains(resource.parameters)
           || x.CountryObj.Name.Contains(resource.parameters)
           || x.City.Contains(resource.parameters)
           || x.Phone1.Contains(resource.parameters));

            var dtos = _Mapper.ProjectTo<MedicalCenterDto>(collection);
            var result = PagedList<MedicalCenterDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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
    }
}