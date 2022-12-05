using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.Botany;
using QbcBackend.Molecules.Services;
using QbcBackend.Tools.QbcException;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcApi.Controllers
{
    [Produces("application/json")]
    [Route("api/BotanicalNameInfo")]
    public class BotanicalNameInfoController : Controller
    {

        #region private properties


        private IBotanicalNameService Service
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// BotanicalNameInfoController
        /// </summary>
        /// <param name="svc">service to delegate all calls</param>
        public BotanicalNameInfoController(IBotanicalNameService svc)
        {
            this.Service = svc;
        }

        /// <summary>
        /// Get all botanical names
        /// </summary>
        /// <returns>List of botanical names</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<BotanicalRank>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await this.Service.GetAllByNameAsync("viridiplantaea");
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
        /// Get the nameinfo by name
        /// </summary>
        /// <param name="name">the case insensitive name of the botanical name</param>
        /// <returns>the requested botanical name</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(BotanicalRank), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            var result = await this.Service.GetByNameAsync(name);
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
        /// Get the pedigree for the specified name
        /// </summary>
        /// <param name="name">the case insensitive name of the botanical name</param>
        /// <returns>list with botanical names from parent to child</returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<BotanicalRank>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("GetPedigreeByName/{name}")]
        public async Task<ActionResult> GetPedigreeByName(string name)
        {
            var result = await this.Service.GetPedigreeByNameAsync(name);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }

        }

        /// <summary>
        /// Insert a new botanical name
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
        public async Task<ActionResult> Post([FromBody]BotanicalNameInfo value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { value.Name });
        }

        /// <summary>
        /// Update a botanical name
        /// </summary>
        /// <param name="name">the case insensitive name of the botanical name</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{name}")]
        public async Task<ActionResult> Put(string name, [FromBody]BotanicalNameInfo value)
        {
            await this.Service.UpdateAsync(value);
            return AcceptedAtAction("Get", new { value.Name });
        }

        /// <summary>
        /// Delete a botanical name
        /// </summary>
        /// <param name="name">the case insensitive name of the botanical name</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the delete worked</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            await this.Service.DeleteAsync(name);
            return Accepted();
        }


    }
}