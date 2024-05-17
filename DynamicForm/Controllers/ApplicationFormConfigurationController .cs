using AutoMapper;
using DynamicForm.DTOs;
using DynamicForm.IServices;
using DynamicForm.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationFormConfigurationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ApplicationFormConfigurationRepository _applicationFormConfigurationRepository;

        public ApplicationFormConfigurationController(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _applicationFormConfigurationRepository = new ApplicationFormConfigurationRepository(_unitOfWork, mapper);
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var applicationFormConfigurations = await _applicationFormConfigurationRepository.GetAll(a => !a.IsDeleted);
                var result = _mapper.Map<List<ApplicationFormConfigurationDTO>>(applicationFormConfigurations);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Application form setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet("{Id}/{programId}")]
        public async Task<IActionResult> GetOne(Guid Id, Guid programId)
        {
            try
            {
                var response = await _applicationFormConfigurationRepository.GetById(Id, programId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Application form setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpPut("{Id}/{programId}")]
        public async Task<IActionResult> Update(Guid Id, Guid programId, [FromBody] CreateApplicationFormConfigurationDTO createApplicationFormConfigurationDTO)
        {
            try
            {
                var response = await _applicationFormConfigurationRepository.UpdateApplicationFormConfiguration(programId, Id, createApplicationFormConfigurationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Application form setup modification was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return await _applicationFormConfigurationRepository.DeleteApplicationFormConfiguration(Id);
            }
            catch (Exception ex)
            {
                return BadRequest("Application form setup removal was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }
    }
}
