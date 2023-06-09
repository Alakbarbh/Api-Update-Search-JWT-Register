﻿using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using Services.DTOs.Employee;
using Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK,Type = typeof(IEnumerable<EmployeeDto>))]
        [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute][Required] int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);

            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto employee)
        {
            try
            {
                await _service.CreateAsync(employee);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        

        [HttpPut]
        public async Task<IActionResult> SoftDelete([Required] int id)
        {
            try
            {
                await _service.SoftDeleteAsync(id);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }

        }



        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute][Required] int id, EmployeeUpdateDto employee)
        {
            try
            {
                await _service.UpdateAsync(id, employee);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
        public async Task<IActionResult> Search(string? searchText)
        {
            try
            {
                return Ok(await _service.SearchAsync(searchText));
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }


    }
}
