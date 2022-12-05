using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using QbcBackend.Tools.QbcException;

namespace QbcApi.Controllers
{
    [Produces("application/json")]
    [Route("api/chemicalcalculations")]
    public class ChemicalCalculationController : Controller
    {

        #region private properties

        private IChemicalCalculationService Service { get; }

        #endregion


        public ChemicalCalculationController(IChemicalCalculationService service)
        {
            this.Service = service;
        }


        /// <summary>
        /// Get a specified chemical calculation
        /// </summary>
        /// <param name="Id">the Id of the calculation</param>
        /// <returns>Information about a chelical calculation</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ChemicalCalculation), 200)]
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
        /// Get the list of calculations for the model
        /// </summary>
        /// <param name="modelId">the id of the model</param>
        /// <returns>List of calculation corresponding to the model</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<ChemicalCalculation>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByModel/{modelId}")]
        public async Task<ActionResult> GetByModel(int modelId)
        {
            var result = await this.Service.GetByModel(modelId);
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
        /// Create a chemical calculation
        /// </summary>
        /// <param name="value">the chemical calculation to create</param>
        /// <returns>Infirmation about success or failure</returns>
        /// <response code="201">the insert worked</response>
        /// <response code="500">internal server error</response>
        /// <response code="406">a validation error occured</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ChemicalCalculation value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { Id = value.Id });
        }


        /// <summary>
        /// Update a chemical calculation
        /// </summary>
        /// <param name="id">the id of the calculation to update</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acsepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]ChemicalCalculation value)
        {
            await this.Service.UpdateAsync(id, value);
            return AcceptedAtAction("Get", new { Id = value.Id });
        }

        /// <summary>
        /// Delete a chemical calculation
        /// </summary>
        /// <param name="id">the id of the calculation to delete</param>
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
