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
using ApiMedical.Common.Filter;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class CountryController : BaseController<Country,CountryDto, IBaseService<Country>>
    {
        public CountryController(IBaseService<Country> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {           
    }
        [HttpGet]
        [AuthorizeMedical]
        [Route("GetCountryPaginated")]
        public IActionResult GetCuntrypaged(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.Name.Contains(resource.parameters));

            if (collection.Count() == 0)
                return NotFound();
            var dtos = _Mapper.ProjectTo<CountryDto>(collection);
            var result= PagedList<CountryDto>.Create(dtos, resource.PageNumber, resource.PageSize);
            var pagination = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                currentPage = result.CurrentPage,
                totalPage = result.TotalPages,
                result.HasNext,
                result.HasPrevious,
                data = result
            };
            return Ok(pagination);
        }
    }
}