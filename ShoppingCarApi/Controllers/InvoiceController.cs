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
using Entities.Entity.View;
using Repository.Interface;

namespace ApiMedical.Controllers
{

    public class InvoiceController : BaseController<Invoice, InvoiceDto, IBaseService<Invoice>>
    {
        private readonly IInvoice invoiceService;
        public InvoiceController(IBaseService<Invoice> manager, IMapper Mapper, IInvoice _invoiceService, IDoctorGuid _doctorGuid) : base(manager, Mapper,_doctorGuid)
        {
            invoiceService = _invoiceService;
        }
        [HttpPost]
        [Route("BillToPatient")]
        public IActionResult BillToPatient([FromBody] InvoiceVwDto data)
        {
            try
            {
                invoiceService.InvoiceAll(data);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("GetInvoicePaginated")]
        [AuthorizeMedical]
        public IActionResult  GetInvoicePaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindByCondition(x => x.DoctorGuid == userData.GetGuidDoctor()).OrderByDescending(c=> c.Id).AsQueryable();
            if (collection.Count() == 0)
                return BadRequest("No Data");
            if (resource.param != null) collection = collection.Where(x => x.PatientId == resource.param);
            if (collection.Count() == 0) return BadRequest("No Data");
            collection = collection.Where(c => c.Patient.Name.Contains(resource.parameters)
            || c.Office.Name.Contains(resource.parameters));
            var dtos = _Mapper.ProjectTo<InvoiceDto>(collection);
            var result =  PagedList<InvoiceDto>.Create(dtos, resource.PageNumber, resource.PageSize);
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
            return Ok( pagination);
        }
       
    }
}