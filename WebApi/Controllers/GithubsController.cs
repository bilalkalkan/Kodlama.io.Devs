using Application.Features.Githubs.Commands.CreateGithub;
using Application.Features.Githubs.Commands.DeleteGithub;
using Application.Features.Githubs.Commands.UpdateGithub;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Models;
using Application.Features.Githubs.Queries.GetByIdGithub;
using Application.Features.Githubs.Queries.GetListGithub;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateGithubCommand createGithubCommand)
        {
            CreateGithubDto createGithubDto = await Mediator.Send(createGithubCommand);
            return Created("", createGithubDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            GetListGithubQuery getListGithubQuery = new() { PageRequest = pageRequest };
            GithubListModel result = await Mediator.Send(getListGithubQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdGithubQuery getByIdGithubQuery)
        {
            GetByIdGithubDto result = await Mediator.Send(getByIdGithubQuery);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubCommand deleteGithubCommand)
        {
            DeleteGithubDto result = await Mediator.Send(deleteGithubCommand);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateGithubCommand updateGithubCommand)
        {
            UpdateGithubDto result = await Mediator.Send(updateGithubCommand);
            return Ok(result);
        }
    }
}