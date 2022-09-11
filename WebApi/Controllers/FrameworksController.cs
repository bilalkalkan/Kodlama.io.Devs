using Application.Features.Frameworks.Commands.CreateFamework;
using Application.Features.Frameworks.Commands.DeleteFramework;
using Application.Features.Frameworks.Commands.UpdateFramework;
using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Models;
using Application.Features.Frameworks.Queries.GetByIdFramework;
using Application.Features.Frameworks.Queries.GetListFramework;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrameworksController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFrameworkCommand createFrameworkCommand)
        {
            CreateFrameworkDto createFrameworkDto = await Mediator.Send(createFrameworkCommand);
            return Created("", createFrameworkDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteFrameworkCommand deleteFrameworkCommand)
        {
            DeleteFrameworkDto deleteFrameworkDto = await Mediator.Send(deleteFrameworkCommand);
            return Created("", deleteFrameworkDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFrameworkCommand updateFrameworkCommand)
        {
            UpdateFrameworkDto updateFrameworkDto = await Mediator.Send(updateFrameworkCommand);
            return Created("", updateFrameworkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            GetListFrameworkQuery getListFrameworkQuery = new() { PageRequest = pageRequest };
            FrameworkListModel frameworkListModel = await Mediator.Send(getListFrameworkQuery);
            return Ok(frameworkListModel);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdFrameworkQuery frameworkGetByIdQuery)
        {
            GetByIdFrameworkDto getByIdFrameworkDto = await Mediator.Send(frameworkGetByIdQuery);
            return Ok(getByIdFrameworkDto);
        }
    }
}