using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.Botany;
using QbcBackend.Molecules.Services;
using QbcBackend.Tools.QbcException;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace QbcApi.Controllers
{
    [Produces("application/json")]
    [Route("api/BotanicalRank")]
    public class BotanicalRankController : Controller
    {
        #region private properties

        private IBotanicalRankService Service
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// BotanicalRankController
        /// </summary>
        /// <param name="service">input service</param>
        public BotanicalRankController(IBotanicalRankService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Get all botanical ranks
        /// </summary>
        /// <returns>The list of all botanicals ranks from parent to child </returns>
        /// <response code="200">the operation succeeded</response>
        /// <response code="204">no data found</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<BotanicalRank>), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await this.Service.GetAllAsync();
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
        /// Get the botanical rank bij name
        /// </summary>
        /// <param name="name">the case sensitive name of the botanical rank</param>
        /// <returns>The requested botanical rank</returns>
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
        /// Get the pedigree of the botanical rank
        /// </summary>
        /// <param name="name">the case sensitive name of the botanical rank</param>
        /// <returns>The list of botanical rank in the order parent to child</returns>
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
        /// Insert a botanical rank
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
        public async Task<ActionResult> Post([FromBody]BotanicalRank value)
        {
            await this.Service.CreateAsync(value);
            return CreatedAtAction("Get", new { Name = value.Name });
        }

        /// <summary>
        /// Update a botanical rank
        /// </summary>
        /// <param name="name">the case sensitive name of the botanical rank</param>
        /// <param name="value">the data to update</param>
        /// <returns>Information about the success or failure</returns>
        /// <response code="202">the update was acepted</response>
        /// <response code="406">a validation error occured</response>
        /// <response code="500">internal server error</response>
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPut("{name}")]
        public async Task<ActionResult> Put(string name, [FromBody]BotanicalRank value)
        {
            await this.Service.UpdateAsync(name, value);
            return AcceptedAtAction("Get", new { Name = value.Name });
        }

        /// <summary>
        /// Delete a botanical rank
        /// </summary>
        /// <param name="name">the case sensitive name of the botanical rank</param>
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