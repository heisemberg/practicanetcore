using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CiudadController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
        {
            var ciudades = await _unitOfWork.Ciudades.GetAllAsync();
            return _mapper.Map<List<CiudadDto>>(ciudades);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CiudadDto>> Get(int id)
        {
            var ciudades = await _unitOfWork.Ciudades.GetByIdAsync(id);

            if (ciudades == null)
            {
                return NotFound();
            }

            return _mapper.Map<CiudadDto>(ciudades);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ciudad>> Post(CiudadDto CiudadDto)
        {
            var ciudades = _mapper.Map<Ciudad>(CiudadDto);
            this._unitOfWork.Ciudades.Add(ciudades);
            await _unitOfWork.SaveAsync();

            if (ciudades == null)
            {
                return BadRequest();
            }
            CiudadDto.Id = ciudades.Id;
            return CreatedAtAction(nameof(Post), new { id = CiudadDto.Id }, CiudadDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto CiudadDto)
        {
            if (CiudadDto.Id == 0)
            {
                CiudadDto.Id = id;
            }

            if(CiudadDto.Id != id)
            {
                return BadRequest();
            }

            if(CiudadDto == null)
            {
                return NotFound();
            }

            var ciudades = _mapper.Map<Ciudad>(CiudadDto);
            _unitOfWork.Ciudades.Update(ciudades);
            await _unitOfWork.SaveAsync();
            return CiudadDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ciudades = await _unitOfWork.Ciudades.GetByIdAsync(id);

            if (ciudades == null)
            {
                return NotFound();
            }

            _unitOfWork.Ciudades.Remove(ciudades);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}