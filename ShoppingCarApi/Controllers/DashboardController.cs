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
using Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        readonly IDataStats repository;
        protected readonly IMapper _Mapper;
        public DashboardController(IDataStats _repository, IMapper Mapper) 
        {
            _Mapper = Mapper;
            repository = _repository; ;
        }
        [Route("GetDataStats/{doctor}")]
        [HttpGet]
        public IActionResult GetDataStats(int doctor)
        {
            return Ok(repository.GetDataByDr(doctor));
        }
        [Route("GetDataStatsDasboardConsultation/{doctor}")]
        [HttpGet]
        public IActionResult GetDataStatsDasboardConsultation(int doctor)
        {
            return Ok(repository.GetDataDashboardByDr(doctor));
        }
    }
}