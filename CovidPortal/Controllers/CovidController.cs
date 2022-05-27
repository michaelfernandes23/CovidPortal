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
        [ProducesResponseType(typeof(ServiceResponse<List<CovidDataResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> GetCovidData()
        {
            try
            {
                return OkServiceResponse(await _covidService.GetCovidCountryData());
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
