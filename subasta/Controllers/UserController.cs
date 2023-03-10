using core.Dto;
using core.Interface;
using core.Models;
using Infra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using subasta.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace subasta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepoUser _RepoUser;
        protected DtoResponse _reponse;
        public UserController(IRepoUser RepoUser) 
        {
            _RepoUser = RepoUser;
            _reponse = new DtoResponse();
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register(userDto user)
        {
            var respuesta = await _RepoUser.Register(new user
            {
                userName = user.userName
            }, user.password);


            if(respuesta == -1)
            {
                _reponse.isSuccess = false;
                _reponse.Message = "Usuario ya existe";
                return BadRequest(_reponse);
            }

            if(respuesta == -500)
            {
                _reponse.isSuccess = false;
                _reponse.Message = "Error al crear el usuario";
            }

            _reponse.Message = "Usuario creado con exito";
            _reponse.result = respuesta;

            return Ok(_reponse);

        }
        // POST api/<ValuesController>
        [HttpPost("Login")]
        public async Task<ActionResult> Post([FromBody] userDto user)
        {
            var respuesta = await _RepoUser.login(user.userName, user.password);
            
            if(respuesta == "nouser")
            {
                _reponse.Message = "No existe el usuario";
                _reponse.isSuccess = false;
                return BadRequest(_reponse);
            }

            _reponse.Message = "se ha logeado con exito";
            _reponse.result = respuesta;
            _reponse.isSuccess = false;

            return Ok(_reponse);
        }

        [HttpGet("prueba")]
        public string Get() 
        {
            return "its working";
        }
    }
}
