using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        readonly ILanguage lang;
        public LanguageController(ILanguage _lang) 
        {
            lang = _lang;
        }
        [HttpGet]
        public  IActionResult Get()
        {
            try
            {
                var data = lang.Get();
                return Ok(data);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
           
        }
    }
}