using DevServ.Core.Entities;
using DevServ.Core.Exceptions;
using DevServ.Infrastructure;
using DevServ.SharedKernel.Interfaces;
using DevServ.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<DevelopersController> _logger;

        public DevelopersController(IRepository<Developer> repository, ILogger<DevelopersController> logger)
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
                _logger.LogInformation("Get list of developers");
                var repositoryItems = await _repository.ListAsync();
                var items = repositoryItems.Select(DeveloperDto.FromDeveloper);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error in get list of developers");
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
                _logger.LogInformation("Get filtered list of developers");

                var filter = new DeveloperFilter(skills);

                var repositoryItems = await _repository.ListAsync(filter);
                var items = repositoryItems.Select(DeveloperDto.FromDeveloper);

                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error in get filtered list of developers");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET: api/Developers
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Start get by id");

                var repositoryItem = await _repository.GetByIdAsync(id);

                if (repositoryItem != null)
                {
                    var item = DeveloperDto.FromDeveloper(repositoryItem);
                    return Ok(item);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error in get by id");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/Developers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeveloperDto developerDto)
        {
            _logger.LogInformation("Start add new developer");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Add new developer invalid input");
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
                _logger.LogError(ex, "Error in add developer");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Developers
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DeveloperDto developerDto)
        {
            _logger.LogInformation("Start update developer");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update developer invalid input");
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
                _logger.LogWarning("Update developer no entity found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in update developer");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Developers
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Start delete developer");
            try
            {
                await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (NoEntityFoundWithIdException)
            {
                _logger.LogWarning("Delete developer no entity found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in delete developer");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
