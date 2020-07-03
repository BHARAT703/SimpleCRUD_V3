using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Dto;
using SimpleCRUD.Entities.Entities;
using SimpleCRUD.Entities.Helpers;
using SimpleCRUD.Infrastructure.Services;
using System.Collections.Generic;

namespace SimpleCRUD.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(source: _service.GetAll()));
        }

        [HttpGet("GetAllWithPagging")]
        public IActionResult GetAllWithPagging(int pageNum, int pageSize)
        {
            var skip = pageSize * (pageNum - 1);
            var data = _service.GetAllTheRecordsWithPagging(skip, pageSize);
            return new OkObjectResult(new PagedList<UserDto> {
                count = data.count,
                results = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(source: data.results)
            });
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            if (Id <= 0)
                return new NotFoundResult();

            User item = _service.GetById(Id);

            if (item == null)
                return new NotFoundResult();

            return new OkObjectResult(_mapper.Map<User, UserDto>(source: item));
        }

        [HttpPost("Insert")]
        public IActionResult Post([FromBody]UserPostDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Response<User> itemToReturn = _service.Insert(_mapper.Map<UserPostDto, User>(source: model));
            if (itemToReturn.IsSuccess == true)
                return new OkObjectResult(_mapper.Map<User, UserDto>(source: itemToReturn.Data));
            else
                return new ObjectResult(itemToReturn.ErrorMessage) { StatusCode = 500 };
        }

        [HttpPut("Update")]
        public IActionResult Put([FromBody]UserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Id <= 0)
                return new NotFoundResult();

            User item = _service.GetById(model.Id);

            if (item == null)
                return new NotFoundResult();

            Response<User> itemToReturn = _service.Update(_mapper.Map<UserDto, User>(model, item));
            if (itemToReturn.IsSuccess == true)
                return new OkObjectResult(_mapper.Map<User, UserDto>(source: itemToReturn.Data));
            else
                return new ObjectResult(itemToReturn.ErrorMessage) { StatusCode = 500 };
        }

        [HttpDelete("SoftDelete")]
        public IActionResult SoftDelete(int id)
        {
            if (id <= 0)
                return new NotFoundResult();

            User item = _service.GetById(id);

            if (item == null)
                return new NotFoundResult();

            Response<User> itemToReturn = _service.SoftDelete(_mapper.Map<User>(source: item));
            if (itemToReturn.IsSuccess == true)
                return new OkObjectResult(true);
            else
                return new ObjectResult(itemToReturn.ErrorMessage) { StatusCode = 500 };
        }

        [HttpDelete("HardDelete")]
        public IActionResult HardDelete(int id)
        {
            if (id <= 0)
                return new NotFoundResult();

            User item = _service.GetById(id);

            if (item == null)
                return new NotFoundResult();

            Response<User> itemToReturn = _service.HardDelete(_mapper.Map<User>(source: item));
            if (itemToReturn.IsSuccess == true)
                return new OkObjectResult(true);
            else
                return new ObjectResult(itemToReturn.ErrorMessage) { StatusCode = 500 };
        }
    }
}