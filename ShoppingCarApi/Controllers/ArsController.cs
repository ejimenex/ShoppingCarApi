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
    public class ArsController : BaseController<HealthManager,HealthManagerDto, IBaseService<HealthManager>>
    {
        public ArsController(IBaseService<HealthManager> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
       
        [HttpGet]
        [Route("GetArsPaginated")]
        public IActionResult GetCuntrypaged(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.Name.Contains(resource.parameters));

            if (collection.Count() == 0)
                return NotFound();
            var dtos = _Mapper.ProjectTo<HealthManagerDto>(collection);
           
            var result = PagedList<HealthManagerDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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
        [HttpGet("GetByCountry")]
        public  IActionResult GetByCountry([FromQuery]int Id)
        {
            try
            {
                var result = _service.FindByCondition(x => x.IsActive && x.CountryId == Id).Select(n=> new HealthManagerDto { 
                Id=n.Id,
                Name=n.Name
                });
                return Ok(_Mapper.Map<IEnumerable<HealthManagerDto>>(result));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
   
}