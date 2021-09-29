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
    public class InvoiceDetailController : Controller
    {
        readonly IInvoideDetail dis;
        protected readonly IMapper _Mapper;
        public InvoiceDetailController(IInvoideDetail _dis, IMapper Mapper) 
        {
            _Mapper = Mapper;
            dis = _dis;
        }
        [HttpGet("{Id}")]
        public IActionResult Get([FromRoute]int Id)
        {
                var data = dis.GetByInvoice(Id);
                var final = _Mapper.ProjectTo<InvoiceDetailDto>(data);
                return Ok(final);
        }
        [HttpPost]
        public IActionResult Add([FromBody]InvoiceDetail detail)
        {
            try
            {
                var result = dis.Add(detail);                
                return Ok();
            }
            catch (Exception)
            {
               return BadRequest();
            }
            
        }
        [HttpPut]
        public IActionResult Edit([FromBody]InvoiceDetail detail)
        {
            try
            {
                dis.Edit(detail);
                return NoContent();
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                dis.Delete(Id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}