using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class MedicalFileController : Controller
    {
      //  IHostingEnvironment hosting;
        readonly IGetData<Parameter> param;
        readonly IRepository<MedicalFile> fileMedical;
        public MedicalFileController(IGetData<Parameter> _param, IRepository<MedicalFile> _file /*IHostingEnvironment _hosting*/) 
        {
            //hosting = _hosting;
            param = _param;
            fileMedical = _file;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult SaveFile()
        {
            try
            {
                var _files = Request.Form.Files;
                var doctorGui = Request.Form["doctorGuid"].FirstOrDefault();
                var consultationId = Request.Form["consultationId"].FirstOrDefault();
                var patient = Request.Form["patient"].FirstOrDefault();
                var description = Request.Form["description"].FirstOrDefault();
                var files = _files;
           
                string webRootPath = param.GetData().Where(c => c.Name == "DoctorFiles").FirstOrDefault().Value;
                string pathDr = Path.Combine(webRootPath, doctorGui.ToString());
                if (!Directory.Exists(pathDr))                
                    Directory.CreateDirectory(pathDr);

                string newPath = Path.Combine(pathDr,patient.ToString());
                if (!Directory.Exists(newPath))                
                    Directory.CreateDirectory(newPath);

               foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                       if(!fileMedical.Exist(c=> c.FullPath == fullPath)) {
                            var newFile = new MedicalFile
                            {
                                FileName = fileName,
                                FullPath = fullPath,
                                FileExtension = "",
                                PatientGuid = new Guid(patient),
                                DoctorGuid = new Guid(doctorGui),
                                ConsultationId = Convert.ToInt32(consultationId),
                                Description = description
                            };
                            fileMedical.Create(newFile);
                        }                       
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la subida de imagenes:" + ex.Message);
                throw;
            }
        }
        [HttpGet("download")]
        public IActionResult Download([FromQuery]int id, Guid dr)
        {
            var data = fileMedical.FindByCondition(c => c.DoctorGuid == dr && c.Id == id).FirstOrDefault();
           
            string filePath =data.FullPath.Replace(@"\\",@"\");
            string fileName = data.FileName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }
        [HttpGet("getByPatient")]
        public IActionResult getByPatient([FromQuery] Guid patientId, Guid dr)
        {
            var data = fileMedical.FindByCondition(c => c.DoctorGuid == dr && c.PatientGuid == patientId).AsEnumerable();
            return Ok(data);
        }
        [HttpGet("getByConsultation")]
        public IActionResult getByConsultation([FromQuery] int ConsultationId, Guid dr)
        {
            var data = fileMedical.FindByCondition(c => c.DoctorGuid == dr && c.ConsultationId == ConsultationId).AsEnumerable();
            return Ok(data);
        }
       
    }
}