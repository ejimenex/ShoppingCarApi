using ApiMedical.Common.Pagination;
using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiMedical.Controllers
{


    [Route("api/[controller]")]
    public class AppointmentController : BaseController<Appointment,AppointmentDto, IBaseService<Appointment>>
    {
        public AppointmentController(IBaseService<Appointment> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }
        [HttpGet]
        [Route("GetAppointmentPaginated")]
        public IActionResult GetAppointmentPaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.Patient.Name.Contains(resource.parameters)
           || x.AppointmentState.Name.Contains(resource.parameters));

            var dtos = _Mapper.ProjectTo<AppointmentDto>(collection);
            var result = PagedList<AppointmentDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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
    [EnableQuery]
    public class AppointmentListController : ODataController
    {
        protected readonly IMapper _Mapper;
        protected IBaseService<Appointment> _service;
       
        public AppointmentListController( IMapper Mapper,IBaseService<Appointment> service) 
        {
            _service = service;
            _Mapper = Mapper;
        }
        [HttpGet]
        [EnableQuery]
        public  IEnumerable<AppointmentDto> Get()
        {
            try
            {
                var objects = _service.FindAll();
                var dtos = _Mapper.Map<IQueryable<AppointmentDto>>(objects);
                return dtos;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}