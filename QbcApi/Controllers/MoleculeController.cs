using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using QbcBackend.Tools.QbcException;

namespace QbcApi.Controllers
{
    [Produces("application/json")]
    [Route("api/molecule")]
    public class MoleculeController : Controller
    {

        #region private datamembers


        private IMoleculeService Service { get; }

        #endregion


        public MoleculeController(IMoleculeService service)
        {
            this.Service = service;
        }


        /// <summary>
        /// Get the list of molecules corresponding to the model
        /// </summary>
        /// <param name="modelId">the id of the model</param>
        /// <returns>List of molecules</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<MoleculeInfo>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByModel/{modelId}")]
        public async Task<ActionResult> GetByModel(int modelId)
        {
            var result = await this.Service.GetByModelID(modelId);
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
        /// Get the list of molecules corresponding to the calculation
        /// </summary>
        /// <param name="calculationid">the id of the calculation</param>
        /// <returns>List of molecules</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<MoleculeInfo>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByCalculationID/{calculationid}")]
        public async Task<ActionResult> GetByCalculation(int calculationid)
        {
            var result = await this.Service.GetByCalculationID(calculationid);
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
        /// Get the molcule with his unique identifier
        /// </summary>
        /// <param name="id">id if the molecule</param>
        /// <returns>molecule</returns>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(MoleculeInfo), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await this.Service.GetById(id);
            if ( result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }


        /// <summary>
        /// Create a molecule
        /// </summary>
        /// <param name="value">the moleculeto create</param>
        /// <returns>Infirmation about success or failure</returns>
        /// <response code="201">the insert worked</response>
        /// <response code="500">internal server error</response>
        /// <response code="406">a validation error occured</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]MoleculeInfo value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { Id = value.Id });
        }


        /// <summary>
        /// Update a molecule
        /// </summary>
        /// <param name="id">the id of the molecule to update</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acsepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]MoleculeInfo value)
        {
            await this.Service.UpdateAsync(id, value);
            return AcceptedAtAction("Get", new { Id = value.Id });
        }

        /// <summary>
        /// Delete a molecule
        /// </summary>
        /// <param name="id">the id of the molecule to delete</param>
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
