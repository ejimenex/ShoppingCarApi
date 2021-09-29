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
using BussinesLogic.Interface.ListInterface;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class MedicalProcessController : BaseController<MedicalProcess,MedicalProcessDto, IBaseService<MedicalProcess>>
    {
        private readonly IMedicalProcess service;
        public MedicalProcessController(IBaseService<MedicalProcess> manager, IMedicalProcess _service, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {
            this.service = _service;
        }
       
        [HttpGet]
        [Route("GetPaginated")]
        public IActionResult GetPaged(MedicalProcessFilter resource)
        {
            var data = this.service.GetPaged(resource);
            return Ok(data);
        }
       
    }
   
}