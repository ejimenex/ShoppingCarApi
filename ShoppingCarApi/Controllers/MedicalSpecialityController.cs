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
    public class MedicalSpecialityController : BaseController<MedicalSpeciality,MedicalSpecialityDto, IBaseService<MedicalSpeciality>>
    {
        public MedicalSpecialityController(IBaseService<MedicalSpeciality> manager, IMapper Mapper, IDoctorGuid _doctorGuid) : base(manager,Mapper,_doctorGuid)
        {

        }

    }
}