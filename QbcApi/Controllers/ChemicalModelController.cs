using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.MoleculeModel;
using QbcBackend.Molecules.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using QbcBackend.Tools.QbcException;

namespace QbcApi.Controllers
{
    [Produces("application/json")]
    [Route("api/chemicalmodels")]
    public class ChemicalModelController : Controller
    {

        #region private members

        private IChemicalModelService Service { get; }

        #endregion


        public ChemicalModelController(IChemicalModelService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get the chemical model for the id
        /// </summary>
        /// <returns>the corresponding chemical model </returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ChemicalModel), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            var result = await this.Service.Get(Id);
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
        /// Get the chemical models for the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>the corresponding chemical models </returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<ChemicalModel>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByProject/{projectId}")]
        public async Task<ActionResult> GetByProject(int projectId)
        {
            var result = await this.Service.GetModelForProject(projectId);
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
        /// Create a new chemical model
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
        public async Task<ActionResult> Post([FromBody]ChemicalModel value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { Name = value.Name });
        }

        /// <summary>
        /// Update a chemical model
        /// </summary>
        /// <param name="id">the id of the model to update</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]ChemicalModel value)
        {
            await this.Service.UpdateAsync(id, value);
            return AcceptedAtAction("Get", new { Name = value.Name });
        }

        /// <summary>
        /// Delete a chemical model
        /// </summary>
        /// <param name="id">the id of the model to delete</param>
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
