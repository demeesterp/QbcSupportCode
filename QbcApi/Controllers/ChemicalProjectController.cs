using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.MoleculeProject;
using QbcBackend.Molecules.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QbcBackend.Tools.QbcException;

namespace QbcApi.Controllers
{
    /// <summary>
    /// CategoriesController
    /// </summary>
    [Produces("application/json")]
    [Route("api/chemicalprojects")]
    public class ChemicalProjectController : Controller
    {
        #region private members

        private IChemicalProjectService Service { get; }

        #endregion


        public ChemicalProjectController(IChemicalProjectService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>The list of prjects </returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<ChemicalProject>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            var result = await this.Service.GetAllChemicalProjectAsync();
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get the list of project matching the name
        /// </summary>
        /// <param name="name">The name or a pattern mathcing the name</param>
        /// <returns>The list of prjects </returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<ChemicalProject>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            var result = await this.Service.SearchChemicalProjectAsync(name);
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get the project with all related data
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <returns>The project with all related data</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<ChemicalProject>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await this.Service.GetChemicalProjectAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Create a new chemical project
        /// </summary>
        /// <param name="value">the data to insert</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="201">the insert worked</response>
        /// <response code="500">internal server error</response>
        /// <response code="406">a validation error occured</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ChemicalProject value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { Name = value.Name });
        }

        /// <summary>
        /// Update a chemical project
        /// </summary>
        /// <param name="id">the id of the project to update</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]ChemicalProject value)
        {
            await this.Service.UpdateAsync(id, value);
            return AcceptedAtAction("Get", new { Name = value.Name });
        }

        /// <summary>
        /// Delete a chemical project
        /// </summary>
        /// <param name="id">the id of the project to delete</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the delete worked</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await this.Service.DeleteAsync(id);
            return Accepted();
        }



    }
}
