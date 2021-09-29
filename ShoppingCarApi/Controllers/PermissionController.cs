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
    public class PermissionController : ControllerBase
    {
        readonly IPermission per;
        public PermissionController(IPermission _per) 
        {
            per = _per;
        }
        [HttpGet("getPermision")]
        public  IActionResult Get(PermisionFilterDto dto)
        {
            try
            {
                var data = per.GetPermission(dto.RolId,dto.PermissionId);
                return Ok(data);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
           
        }
    }
}