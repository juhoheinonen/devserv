using DevServ.Core.Entities;
using DevServ.Core.Exceptions;
using DevServ.Infrastructure;
using DevServ.SharedKernel.Interfaces;
using DevServ.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        private readonly ILogger _logger;

        public DevelopersController(IRepository<Developer> repository, ILogger logger)
        {
            _repository = repository;
            this._logger = logger;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                _logger.Information("Get list of developers");
                var repositoryItems = await _repository.ListAsync();
                var items = repositoryItems.Select(DeveloperDto.FromDeveloper);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error in get list of developers");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/Developer
        [HttpPost]
        [Route("filtered")]
        public async Task<IActionResult> FilteredList([FromBody] List<string> skills)
        {
            try
            {
                _logger.Information("Get filtered list of developers");

                var filter = new DeveloperFilter(skills);

                var repositoryItems = await _repository.ListAsync(filter);
                var items = repositoryItems.Select(DeveloperDto.FromDeveloper);

                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error in get filtered list of developers");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
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
            _logger.Information("Start add new developer");

            if (!ModelState.IsValid)
            {
                _logger.Warning("Add new developer invalid input");
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
                _logger.Error(ex, "Error in add developer");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Developers
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeveloperDto developerDto)
        {
            _logger.Information("Start update developer");

            if (!ModelState.IsValid)
            {
                _logger.Warning("Update developer invalid input");
                return BadRequest();
            }

            try
            {
                var developer = DeveloperDto.ToDeveloper(developerDto);

                await _repository.UpdateAsync(developer);
                return Ok();
            }
            catch (NoEntityFoundWithIdException)
            {
                _logger.Warning("Update developer no entity found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in update developer");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Developers
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Information("Start delete developer");
            try
            {
                await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (NoEntityFoundWithIdException)
            {
                _logger.Warning("Delete developer no entity found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in delete developer");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
