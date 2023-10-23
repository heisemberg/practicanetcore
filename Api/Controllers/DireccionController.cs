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
    public class DireccionController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DireccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DireccionDto>>> Get()
        {
            var direcciones = await _unitOfWork.Direcciones.GetAllAsync();
            return _mapper.Map<List<DireccionDto>>(direcciones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DireccionDto>> Get(int id)
        {
            var direcciones = await _unitOfWork.Direcciones.GetByIdAsync(id);

            if (direcciones == null)
            {
                return NotFound();
            }

            return _mapper.Map<DireccionDto>(direcciones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Direccion>> Post(DireccionDto DireccionDto)
        {
            var direcciones = _mapper.Map<Direccion>(DireccionDto);
            this._unitOfWork.Direcciones  .Add(direcciones);
            await _unitOfWork.SaveAsync();

            if (direcciones == null)
            {
                return BadRequest();
            }
            DireccionDto.Id = direcciones.Id;
            return CreatedAtAction(nameof(Post), new { id = DireccionDto.Id }, DireccionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DireccionDto>> Put(int id, [FromBody] DireccionDto DireccionDto)
        {
            if (DireccionDto.Id == 0)
            {
                DireccionDto.Id = id;
            }

            if(DireccionDto.Id != id)
            {
                return BadRequest();
            }

            if(DireccionDto == null)
            {
                return NotFound();
            }

            var direcciones = _mapper.Map<Direccion>(DireccionDto);
            _unitOfWork.Direcciones.Update(direcciones);
            await _unitOfWork.SaveAsync();
            return DireccionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var direcciones = await _unitOfWork.Direcciones.GetByIdAsync(id);

            if (direcciones == null)
            {
                return NotFound();
            }

            _unitOfWork.Direcciones.Remove(direcciones);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}