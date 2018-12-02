using Carcarbe.Shared.Domain;
using Carcarbe.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementsController : Controller
    {
        private readonly IMeasurementRepository _measurementRepository;
        public MeasurementsController(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        [HttpGet, Route("{measurementType}")]
        public async Task<IActionResult> GetMeasurementsByTypeAsync([FromRoute]string measurementType)
        {
            MeasurementType measurementTypeEnum= MeasurementType.humidity;
            Enum.TryParse( measurementType, out measurementTypeEnum);
            var measurements = await _measurementRepository.FindAllByTypeAsync(measurementTypeEnum);
            return Ok(measurements);
        }
    }
}
