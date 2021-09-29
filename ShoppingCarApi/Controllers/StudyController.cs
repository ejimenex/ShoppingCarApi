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
    public class StudyController : BaseController<Study,StudyDto, IBaseService<Study>>
    {
        public StudyController(IBaseService<Study> manager, IMapper Mapper, IDoctorGuid _doctor) : base(manager,Mapper,_doctor)
        {
        }
       
        [HttpGet]
        [Route("GetStudyPaginated")]
        public IActionResult GetStudyPaginated(ResourceParameters resource)
        {
            var data = _service.FindByCondition(c => c.DoctorGuid == userData.GetGuidDoctor()) ;
            if (resource.parameters == null) resource.parameters = "";
            var collection = data.Where(x => x.Description.Contains(resource.parameters));

            if (collection.Count() == 0)
                return NotFound();
            var dtos = _Mapper.ProjectTo<StudyDto>(collection);
            var result = PagedList<StudyDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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