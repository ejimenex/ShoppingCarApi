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
using Fingers10.ExcelExport.ActionResults;
using Repository.Dtos.ExcelDto;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        readonly IExcellReport report;
        public ReportController(IExcellReport _report) 
        {
            report = _report;
        }
        [HttpGet("GetConsultation")]
        public  IActionResult GetConsultation(DateTime startDate, DateTime endDate, int? officeId, Guid doctorId, int? patientId)
        {
            try
            {
                var data = report.GetConsultationByDate(startDate,endDate,officeId,doctorId,patientId);
                return new ExcelResult<ConsultationExcelDto>(data.AsEnumerable(), "ReporteConsultasMedicas", "ReporteConsultasMedicas");
               
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
           
        }
        [HttpGet("GetPatient")]
        public IActionResult GetPatient(Guid doctorId)
        {
            try
            {
                var data = report.GetPatientByDoctor(doctorId);
                return new ExcelResult<PatientExcelDto>(data.AsEnumerable(), "ReportePacientes", "ReportePacientes");

            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

        }
    }
}