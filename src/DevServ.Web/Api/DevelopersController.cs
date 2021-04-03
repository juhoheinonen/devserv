using DevServ.Core.Entities;
using DevServ.SharedKernel;
using DevServ.SharedKernel.Interfaces;
using DevServ.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevServ.Web.Api
{
    public class DevelopersController : BaseApiController
    {
        private IRepository<Developer> _repository;

        public DevelopersController(IRepository<Developer> repository)
        {
            _repository = repository;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var repositoryItems = await _repository.ListAsync();
            var items = repositoryItems.Select(DeveloperDto.FromDeveloper);
            return Ok(items);
        }

        // GET: api/Developers
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var repositoryItem = await _repository.GetByIdAsync(id);

            if (repositoryItem != null)
            {
                var item = DeveloperDto.FromDeveloper(repositoryItem);
                return Ok(item);
            }

            return NotFound();
        }
    }
}
