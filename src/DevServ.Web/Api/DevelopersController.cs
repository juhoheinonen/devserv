using DevServ.Core.Entities;
using DevServ.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevServ.Web.Api
{
    public class DevelopersController : BaseApiController
    {
        private IRepository _repository;

        public DevelopersController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = (await _repository.ListAsync<Developer>())
                            .Select(ToDoItemDTO.FromToDoItem);
            return Ok(items);
        }
    }
}
