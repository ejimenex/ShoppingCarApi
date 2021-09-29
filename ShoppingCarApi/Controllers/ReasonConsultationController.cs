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

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class ReasonConsultationController : ControllerBase
    {
        readonly IReasonRepository per;
        public ReasonConsultationController(IReasonRepository _per) 
        {
            per = _per;
        }
        [HttpGet]
        public  IActionResult Get()
        {
            try
            {
                var data = per.Reasons();
                return Ok(data);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
           
        }
    }
}