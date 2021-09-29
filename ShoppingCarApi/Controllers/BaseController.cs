using ApiMedical.Auth;
using ApiMedical.Common.Filter.Base;
using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiMedical.Controllers
{
    [Route("api/[controller]")]

    public class BaseController<TEntity, TDto, TManager> : Controller where TEntity : BaseClass where TManager : IBaseService<TEntity>
    {

        protected readonly TManager _service;
        protected readonly IDoctorGuid userData;
        protected readonly IMapper _Mapper;

        public BaseController(TManager manager, IMapper mapper, IDoctorGuid _userData)
        {
            _service = manager;
            userData = _userData;
            _Mapper = mapper;
        }

        [HttpGet("{id}")]
        [AuthorizeMedical]
        public virtual TDto Get([FromRoute]int id)
        {
            var accessToken = Request.Headers;
            try
            {
                var entity = _service.GetOne(id);
                if (entity == null || !entity.IsActive)
                {
                    throw new ArgumentException("Not Found");
                }
                TDto dto = _Mapper.Map<TDto>(entity);
                return dto;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        [HttpGet]
        [Route("GetPagedData")]
        [AuthorizeMedical]
        public virtual IActionResult GetPaged([FromQuery] BaseFilter filter)
        {
            try
            {
                var data = _service.FindAll();
                var entity = _service.GetPaged(filter,data);              
                var dto = _Mapper.Map<IEnumerable<TEntity>>(entity.Data);
                entity.Data = dto;
                return Ok(entity);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        [HttpGet]
        [AuthorizeMedical]
        public virtual IEnumerable<TDto> GetAll()
        {
            try
            {
                var objects = _service.FindAll().OrderByDescending(x => x.Id);
                var dtos = _Mapper.ProjectTo<TDto>(objects);
                return dtos.AsEnumerable();
            }
            catch (Exception e)
            {
                 throw new ArgumentException( e.Message);
            }
        }

    

    [HttpPost]
        public virtual IActionResult Post([FromBody] TDto dto)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    throw new ArgumentException(messages);
                }
                TEntity entity = _Mapper.Map<TEntity>(dto);
                entity.CreateBy = userData.GetIdUser();
                return Ok(_service.Create(entity));
            }

            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual IActionResult Delete([FromRoute]int id)
        {
            try
            {
                if (id == 0) return NotFound("Id can not be zero");
                _service.Delete(id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        public virtual IActionResult Put([FromBody] TDto dto)
        {
            try
            {
                TEntity entity = _Mapper.Map<TEntity>(dto);
                entity.ModifiedBy = userData.GetIdUser();
                _service.Update(entity);
                return NoContent();
            }

            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
