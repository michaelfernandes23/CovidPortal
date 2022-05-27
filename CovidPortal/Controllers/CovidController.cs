using CovidPortal.Domain.DTO;
using CovidPortal.Services;
using CovidPortal.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CovidController : ServiceControllerBase
    {
        private readonly ICovidService _covidService;

        public CovidController(ILogger<CovidController> logger,
                                 CovidService covidService) : base(logger)
        {
            _covidService = covidService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<CovidData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetCovidData()
        {
            try
            {
                return OkServiceResponse(await _covidService.GetAllCovidData());
            }
            catch (ValidationException vex)
            {
                return HandleValidationException(vex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<List<CovidData>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetCovidDataById([FromRoute] string id)
        {
            try
            {
                return OkServiceResponse(await _covidService.GetCovidDataById(id));
            }
            catch (ValidationException vex)
            {
                return HandleValidationException(vex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<CovidData>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> SaveCovidData([FromBody()] CovidData model)
        {
            try
            {
                return CreatedServiceResponse(await _covidService.SaveCovidData(model));
            }
            catch (ValidationException vex)
            {
                return HandleValidationException(vex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<CovidData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> UpdateCovidData([FromBody()] CovidData model)
        {
            try
            {
                return OkServiceResponse(await _covidService.UpdateCovidData(model));
            }
            catch (ValidationException vex)
            {
                return HandleValidationException(vex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> DeleteCovidData([FromQuery] string id)
        {
            try
            {
                await _covidService.DeleteCovidData(id);
                return OkServiceResponse(null);
            }
            catch (ValidationException vex)
            {
                return HandleValidationException(vex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }
    }
}
