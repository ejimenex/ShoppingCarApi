using ApiMedical.Auth;
using ApiMedical.Common.Filter;
using AutoMapper;
using BussinesLogic.Interface;
using BussinesLogic.Interface.ListInterface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using Repository.Interface;

namespace ApiMedical.Controllers
{
    
    public class UserMovileController : BaseController<UserMovile,UserMovileDto, IBaseService<UserMovile>>
    {
        readonly IUserMovileList consu;
        public UserMovileController(IBaseService<UserMovile> manager, IMapper Mapper, IUserMovileList _consu, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {
            consu = _consu;
        }
        [HttpGet]
        [AuthorizeMedical]
        [Route("GetUserMovilePaginated")]
        public IActionResult GetUserMovilePaginated(UserMovileFilter resource)
        {
            var pagination = consu.GetPaged(resource);
            return Ok(pagination);
        }
    }
}