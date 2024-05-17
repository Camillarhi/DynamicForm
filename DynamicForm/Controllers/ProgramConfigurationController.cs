using AutoMapper;
using DynamicForm.DTOs;
using DynamicForm.IServices;
using DynamicForm.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramConfigurationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ProgramConfigurationRepository _programConfigurationRepository;

        public ProgramConfigurationController(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _programConfigurationRepository = new ProgramConfigurationRepository(_unitOfWork, mapper);
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetConfiguredPrograms()
        {
            try
            {
                var programConfigurations = await _programConfigurationRepository.GetAll(a => !a.IsDeleted);
                var result = _mapper.Map<List<ProgramConfigurationDTO>>(programConfigurations);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Program setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne(Guid Id)
        {
            try
            {
                var response = await _programConfigurationRepository.GetById(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Program setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProgramConfigurationDTO createProgramConfigurationDTO)
        {
            try
            {
                var response = await _programConfigurationRepository.CreateProgramConfiguration(createProgramConfigurationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Program setup creation was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] CreateProgramConfigurationDTO createProgramConfigurationDTO)
        {
            try
            {
                var response = await _programConfigurationRepository.UpdateProgramConfiguration(Id, createProgramConfigurationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Program setup modification was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return await _programConfigurationRepository.DeleteProgramConfiguration(Id);
            }
            catch (Exception ex)
            {
                return BadRequest("Program setup removal was not successful" + ex.Message ?? ex.InnerException?.Message);

            }
        }
    }
}
