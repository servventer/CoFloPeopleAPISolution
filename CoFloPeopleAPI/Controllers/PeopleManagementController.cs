using Microsoft.AspNetCore.Mvc;

namespace CoFloPeopleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleManagementController : ControllerBase
    {
        private readonly ILogger<PeopleManagementController> _logger;
        private readonly IPeopleManagement _peopleManagement;

        public PeopleManagementController(ILogger<PeopleManagementController> logger, IPeopleManagement peopleManagement)
        {
            _logger = logger;
            _peopleManagement = peopleManagement;
        }


        /// <summary>
        /// Creates Person Entry
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        [Route("api/person/create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newPerson = await _peopleManagement.CreatePersonAsync(personModel);
                    return Ok(newPerson);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error in Create : {e.Message}");
                    return new ObjectResult(e.Message)
                    {
                        StatusCode = 400
                    };
                }
            }
            _logger.LogError($"Error in Create : {ModelState}");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get Person Entry By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/person/get/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonById(int? id)
        {
            if (id == null)
            {
                return new ObjectResult("Id cannot be null")
                {
                    StatusCode = 400
                };
            }
            try
            {
                var personModel = await _peopleManagement.GetPersonById((int)id);
                return Ok(personModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetPersonById : {e.Message}");
                return new ObjectResult(e.Message)
                {
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// Get List of Persons
        /// </summary>
        /// <returns></returns>
        [Route("api/person/getlist")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PersonModel>>> GetPersonList()
        {
            try
            {
                return Ok(await _peopleManagement.GetListOfPersonAsync());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetPersonList : {e.Message}");
                return new ObjectResult(e.Message)
                {
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// Delete Person Entry By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/person/delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersonById(int? id)
        {
            if (id == null)
            {
                return new ObjectResult("Id cannot be null")
                {
                    StatusCode = 400
                };
            }
            try
            {
                await _peopleManagement.DeletePersonAsync((int)id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DeletePersonById : {e.Message}");
                return new ObjectResult(e.Message)
                {
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// Update Person Entry
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        [Route("api/person/update")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedPerson = await _peopleManagement.UpdatePersonAsync(personModel);
                    return Ok(updatedPerson);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error in Update : {e.Message}");
                    return new ObjectResult(e.Message)
                    {
                        StatusCode = 400
                    };
                }
            }
            _logger.LogError($"Error in Update : {ModelState}");
            return BadRequest(ModelState);
        }

    }
}
