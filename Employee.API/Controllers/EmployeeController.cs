using AutoMapper;
using Employee.Entity.Entities;
using Employee.Service.DataTransferObjects;
using Employee.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Employee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IMapper mapper, IEmployeeService employeeService)
        {
            _logger = logger;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get the list of employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation("Getting list of employees.");

            var employees = await _employeeService.GetEmployees();

            var data = _mapper.Map<IEnumerable<GetEmployeeInformationDto>>(employees);

            if(data.Count() == 0)
                return NotFound($"Employees count {data.Count()}.");

            return Ok(data);
        }

        /// <summary>
        /// Get employee record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("SearchEmployeeNumber/{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByEmployeeNumber(int id)
        {
            _logger.LogInformation($"Getting employee information - employee id {id}.");

            var employee = await _employeeService.GetEmployeeById(id);

            var data = _mapper.Map<GetEmployeeInformationDto>(employee);

            if (data == null)
                return NotFound($"Employee number {id} not found.");

            return Ok(data);
        }

        /// <summary>
        /// Add new employee record.
        /// </summary>
        /// <param name="createEmployeeInformationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateEmployeeRecord(CreateEmployeeInformationDto createEmployeeInformationDto)
        {           
            if(!ModelState.IsValid)
                return BadRequest();

            _logger.LogInformation("Creating new employee information.");

            var data = _mapper.Map<EmployeeInformation>(createEmployeeInformationDto);

            var result = await _employeeService.Add(data);

            return CreatedAtAction("GetEmployeeById", new { id = result.EmployeeNumber } , createEmployeeInformationDto);
        }

        /// <summary>
        /// Updates employee record.
        /// </summary>
        /// <param name="updateEmployeeInformationDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateEmployeeRecord(UpdateEmployeeInformationDto updateEmployeeInformationDto)
        {            
            if (!ModelState.IsValid)
                return BadRequest();

            _logger.LogInformation($"Updating employee information with employee number {updateEmployeeInformationDto.EmployeeNumber}.");

            var employee = await _employeeService.GetEmployeeById(updateEmployeeInformationDto.EmployeeNumber);
            if (employee == null) 
                return NotFound($"Employee number {updateEmployeeInformationDto.EmployeeNumber} not found.");

            var data = _mapper.Map<EmployeeInformation>(updateEmployeeInformationDto);

            await _employeeService.Update(data);

            return Ok();
        }

        [HttpGet("SearchFirstName/{firstName}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByFirstName(string firstName)
        {
            _logger.LogInformation($"Getting employee record - first name {firstName}.");

            var employee = await _employeeService.GetEmployeeByFirstName(firstName);

            var data = _mapper.Map<GetEmployeeInformationDto>(employee);

            if (data == null)
                return NotFound($"Employee first name {firstName} not found.");

            return Ok(data);
        }

        [HttpGet("SearchLastName/{lastName}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByLastName(string lastName)
        {
            _logger.LogInformation($"Getting employee record - last name {lastName}.");

            var employee = await _employeeService.GetEmployeeByFirstName(lastName);

            var data = _mapper.Map<GetEmployeeInformationDto>(employee);

            if (data == null)
                return NotFound($"Employee last name {lastName} not found.");

            return Ok(data);
        }

        [HttpGet("SearchTemperature/{temperatureStart}/{temperatureEnd}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByTemperature(int temperatureStart, int temperatureEnd)
        {
            if (temperatureStart < 1 || temperatureEnd < 1)
                return BadRequest("Temperature range start and end value must be greater than 0.");

            if (temperatureStart > temperatureEnd)
                return BadRequest("Temperature end should be greater than start value.");

            _logger.LogInformation($"Getting employee information temperature range {temperatureStart} to {temperatureEnd}.");

            var result = await _employeeService.GetEmployeeByTemperature(temperatureStart, temperatureEnd);

            var data = _mapper.Map<IEnumerable<GetEmployeeInformationDto>>(result);

            if (!data.Any())
                return NotFound($"Employee information with temperature range {temperatureStart} to {temperatureEnd} not found.");

            return Ok(data);
        }

        [HttpGet("SearchRecordDate/{recordStartDate}/{recordEndDate}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByRecordDate(DateTime recordStartDate, DateTime recordEndDate)
        {
            if (recordStartDate == DateTime.MinValue || recordEndDate == DateTime.MinValue)
                return BadRequest("Record date range start and end value must be valid.");

            if (recordStartDate > recordEndDate)
                return BadRequest("Record date end should be greater than start date.");

            _logger.LogInformation($"Getting employee information by record date range {recordStartDate} to {recordEndDate}.");

            var result = await _employeeService.GetEmployeeByRecordDate(recordStartDate, recordEndDate);

            var data = _mapper.Map<IEnumerable<GetEmployeeInformationDto>>(result);

            if (!data.Any())
                return NotFound($"Employee information with record date {recordStartDate} to {recordEndDate} not found.");

            return Ok(data);
        }                
    }
}
