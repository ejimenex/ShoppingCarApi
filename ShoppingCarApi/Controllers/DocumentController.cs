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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net.Http.Headers;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
      
        IRepository<Documents> document; 
        public DocumentController( IRepository<Documents> _document) 
        {
            document = _document;
          
        }
        [HttpPost, DisableRequestSizeLimit]

        public IActionResult SaveFile()
        {
            try
            {
                var _files = Request.Form.Files;
                var files = _files
                    .Where(x => x.FileName.Contains(".jpg") || x.FileName.Contains(".png") || x.FileName.Contains(".jpeg") || x.FileName.Contains(".pdf") || x.FileName.Contains(".JPG") || x.FileName.Contains(".PNG") || x.FileName.Contains(".JPEG") || x.FileName.Contains(".PDF"))
                    .ToList();
                var archivo = files.FirstOrDefault();
                string folderName = archivo.Name.Replace(archivo.FileName, "");
                string webRootPath = "";// img.GetRuta("ImgVisDom");
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {

                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        var newDocument = new Documents();
                        newDocument.DoctorGuid =Guid.Parse("");
                        newDocument.PatientGuid = Guid.Parse("");
                        newDocument.UrlDocument = fullPath;
                        newDocument.DocumentTypeId = 0;
                        newDocument.Name = fileName;
                        document.Create(newDocument);
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
    }
}