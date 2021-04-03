using DevServ.Core.Entities;
using DevServ.Core.Exceptions;
using DevServ.Infrastructure;
using DevServ.SharedKernel.Interfaces;
using DevServ.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            try
            {
                var repositoryItems = await _repository.ListAsync();
                var items = repositoryItems.Select(DeveloperDto.FromDeveloper);
                return Ok(items);
            }
            catch (Exception ex)
            {
                // todo: log
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/Developer
        [HttpPost]
        [Route("filtered")]
        public async Task<IActionResult> FilteredList([FromBody] List<string> skills)
        {
            var filter = new DeveloperFilter(skills);

            var repositoryItems = await _repository.ListAsync(filter);
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {                
                var developer = DeveloperDto.ToDeveloper(developerDto);

                await _repository.UpdateAsync(developer);
                return Ok();
            }
            catch (NoEntityFoundWithIdException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // todo: log this
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
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
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
