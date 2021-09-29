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

    public class MedicalServiceController : BaseController<MedicalServices,MedicalServiceDto, IBaseService<MedicalServices>>
    {
        public MedicalServiceController(IBaseService<MedicalServices> manager, IMapper Mapper, IDoctorGuid _doctor) : base(manager,Mapper,_doctor)
        {
        }
        [HttpGet]
        [Route("GetByGuid")]
        public IActionResult GetByGuid()
        {           
            var collection = _service.FindByCondition(x => x.DoctorGuid == userData.GetGuidDoctor() && x.IsActive).OrderBy(c=> c.Name);
            var dtos = _Mapper.ProjectTo<MedicalServiceDto>(collection);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("GetMedicalServicePaginated")]
        public IActionResult GetMedicalServicePaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.DoctorGuid == userData.GetGuidDoctor());
              collection=collection.Where(c=>c.Name.Contains(resource.parameters));
            var dtos = _Mapper.ProjectTo<MedicalServiceDto>(collection);
            var result = PagedList<MedicalServiceDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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