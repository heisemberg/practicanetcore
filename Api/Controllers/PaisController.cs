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
    public class PaisController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
        {
            var pais = await _unitOfWork.Paises.GetAllAsync();
            return _mapper.Map<List<PaisDto>>(pais);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaisDto>> Get(int id)
        {
            var Paises = await _unitOfWork.Paises.GetByIdAsync(id);

            if (Paises == null)
            {
                return NotFound();
            }

            return _mapper.Map<PaisDto>(Paises);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pais>> Post(PaisDto PaisDto)
        {
            var Paises = _mapper.Map<Pais>(PaisDto);
            this._unitOfWork.Paises.Add(Paises);
            await _unitOfWork.SaveAsync();

            if (Paises == null)
            {
                return BadRequest();
            }
            PaisDto.Id = Paises.Id;
            return CreatedAtAction(nameof(Post), new { id = PaisDto.Id }, PaisDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaisDto>> Put(int id, [FromBody] PaisDto PaisDto)
        {
            if (PaisDto.Id == 0)
            {
                PaisDto.Id = id;
            }

            if(PaisDto.Id != id)
            {
                return BadRequest();
            }

            if(PaisDto == null)
            {
                return NotFound();
            }

            var Paises = _mapper.Map<Pais>(PaisDto);
            _unitOfWork.Paises.Update(Paises);
            await _unitOfWork.SaveAsync();
            return PaisDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Paises = await _unitOfWork.Paises.GetByIdAsync(id);

            if (Paises == null)
            {
                return NotFound();
            }

            _unitOfWork.Paises.Remove(Paises);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}