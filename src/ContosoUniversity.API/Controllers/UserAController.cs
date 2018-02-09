using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Model.Entities;
using ContosoUniversity.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserAController : Controller
    {
        private readonly IAdminRepository _repository;

        public UserAController(IAdminRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("{userName}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(string userName)
        {
            ApplicationUser user = _repository.GetSingle(au => au.UserName == userName);

            if (user != null)
            {
                ApplicationUserViewModel userVM = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);

                return new OkObjectResult(userVM);
            }
            else
                return NotFound();
        }
    }
}