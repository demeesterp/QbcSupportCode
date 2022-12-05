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
    [Route("api/chemicalcalculationgroup")]
    public class ChemicalCalculationGroupController : Controller
    {

        #region private datamembers


        private IChemicalCalculationGroupService Service { get; }


        #endregion


        public ChemicalCalculationGroupController(IChemicalCalculationGroupService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Get the chemical models for the project
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns>the corresponding chemical calculationgroups </returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<ChemicalCalculationGroup>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetByParent/{parentId}")]
        public async Task<ActionResult> GetByParent(int parentId)
        {
            var result = await this.Service.GetByParentAsync(parentId);
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
        /// Create a new calculationgroup
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
        public async Task<ActionResult> Post([FromBody]ChemicalCalculationGroup value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { ParentId = value.ParentCalculationID });
        }

        /// <summary>
        /// Update a calculationgroup
        /// </summary>
        /// <param name="id">the id of the calculationgroup to update</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]ChemicalCalculationGroup value)
        {
            await this.Service.UpdateAsync(id, value);
            return AcceptedAtAction("Get", new { ParentId = value.ParentCalculationID });
        }

        /// <summary>
        /// Delete a calculationgroup
        /// </summary>
        /// <param name="id">the id of the calculationgroup to delete</param>
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
