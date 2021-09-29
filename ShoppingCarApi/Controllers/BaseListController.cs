using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BussinesLogic.Interface;
using Entities.Entity;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace ApiMedical.Controllers
{
    // [Route("api/[controller]")]

    public class BaseListController<TEntity, TDto, TManager> : ODataController where TEntity : BaseClass where TManager : IBaseService<TEntity>
    {

        protected readonly TManager _service;

        protected readonly IMapper _Mapper;

        public BaseListController(TManager manager, IMapper mapper)
        {
            _service = manager;
            _Mapper = mapper;

        }

        [HttpGet]
        [EnableQuery]
        
        public virtual IEnumerable<TDto> Get()
        {
            try
            {
                var objects = _service.FindAll();
                var dtos = _Mapper.ProjectTo<TDto>(objects);
                return dtos;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
       
}
