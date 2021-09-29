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
using Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]
    public class DoctorController : BaseController<Doctor,DoctorDto, IBaseService<Doctor>>
    {
        private readonly IRepository<Users> userRepository;
        public DoctorController(IBaseService<Doctor> manager, IDoctorGuid _doctorGuid, IRepository<Users> _userRepository, IMapper Mapper) : base(manager,Mapper, _doctorGuid)
        {
            userRepository = _userRepository;
        }
     
        [HttpGet]
        [Route("GetDoctorPaginated")]
        [AuthorizeMedical]
        public IActionResult GetDoctorPaginated(ResourceParameters resource)
        {
            if (resource.parameters == null) resource.parameters = "";
            var collection = _service.FindAll().Where(x => x.Name.Contains(resource.parameters) 
            || x.CountryWork.Name.Contains(resource.parameters));
           
            var dtos = _Mapper.ProjectTo<DoctorDto>(collection);
            var result = PagedList<DoctorDto>.Create(dtos, resource.PageNumber, resource.PageSize);
            var pagination = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                currentPage = result.CurrentPage,
                totalPage = result.TotalPages,
                result.HasNext,
                 result.HasPrevious,
                data = result
            };
            return Ok(pagination);
        }
        [HttpGet]
        [Route("GetNameByGuid")]
        [AuthorizeMedical]
        public IActionResult GetNameByGuid()
        {
            try
            {
                
                var data = _service.FindAll().Where(c => c.DoctorGuid == userData.GetGuidDoctor())
                                .Include(c => c.MedicalSpecialityDoctor.Where(c => c.IsActive)).ThenInclude(c=> c.MedicalSpeciality).FirstOrDefault();
                var dtos = _Mapper.Map<DoctorDto>(data);
                var spelciaties = string.Join(" - ", data.MedicalSpecialityDoctor.Select(c => c.MedicalSpeciality.Name));
                string name = $"{data.Treament} {data.Name} {data.SurName}: {spelciaties}";

                return Json(name);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
            [HttpGet]
        [Route("GetByGuid")]
        [AuthorizeMedical]
        public IActionResult GetByGuid()
        {
            try
            {
                var data = _service.FindAll().Where(c => c.DoctorGuid == userData.GetGuidDoctor()).Include(c => c.CountryWork)
                                .Include(c => c.MedicalSpecialityDoctor.Where(c=> c.IsActive)).FirstOrDefault();
                var dtos = _Mapper.Map<DoctorDto>(data);

                return Ok(dtos);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
}