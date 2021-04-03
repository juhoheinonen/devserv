using DevServ.Core.Entities;
using DevServ.Core.Exceptions;
using DevServ.SharedKernel.Interfaces;
using DevServ.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
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

        // POST: api/Developers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeveloperDto developerDto)
        {
            var developer = DeveloperDto.ToDeveloper(developerDto);

            try
            {
                developer = await _repository.AddAsync(developer);
                return Ok(DeveloperDto.FromDeveloper(developer));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }            
        }

        // PUT: api/Developers
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeveloperDto developerDto)
        {
            // todo: implement some kind of data security

            // todo: implement validation.
            var developer = DeveloperDto.ToDeveloper(developerDto);

            try
            {
                await _repository.UpdateAsync(developer);
                return Ok();
            }
            catch (NoEntityFoundWithIdException ex)
            {
                return NotFound();
            }
        }

        // DELETE: api/Developers
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (NoEntityFoundWithIdException ex)
            {
                return NotFound();
            }
        }
    }
}
