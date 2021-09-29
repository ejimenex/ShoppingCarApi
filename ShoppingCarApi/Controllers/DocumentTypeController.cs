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
    public class DocumentTypeController : Controller
    {
        readonly IGetData<DocumentType> repo;
        public DocumentTypeController(IGetData<DocumentType> _repo) 
        {
            repo = _repo;
        }

        [HttpGet]
        public  IActionResult Get()
        {
            try
            {
                var data = repo.GetData();
                return Ok(data);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }           
        }
    }
}