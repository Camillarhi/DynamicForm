using AutoMapper;
using DynamicForm.DTOs;
using DynamicForm.Enums;
using DynamicForm.IServices;
using DynamicForm.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionConfigurationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        QuestionConfigurationRepository _questionConfigurationRepository;

        public QuestionConfigurationController(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _questionConfigurationRepository = new QuestionConfigurationRepository(_unitOfWork, mapper);
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllConfiguredQuestions()
        {
            try
            {
                var questionConfigurations = await _questionConfigurationRepository.GetAll(a => !a.IsDeleted);
                var result = _mapper.Map<List<QuestionConfigurationDTO>>(questionConfigurations);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Question setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("getall/questiontype/{type}")]
        public async Task<IActionResult> GetAllConfiguredQuestionsByType(QuestionType type)
        {
            try
            {
                var questionConfigurations = await _questionConfigurationRepository.GetAll(a => !a.IsDeleted && a.Type == type);
                var result = _mapper.Map<List<QuestionConfigurationDTO>>(questionConfigurations);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Question setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("getall/applicationform/{applicationFormId}")]
        public async Task<IActionResult> GetAllConfiguredQuestionsByApplicationForm(Guid applicationFormId)
        {
            try
            {
                var questionConfigurations = await _questionConfigurationRepository.GetAll(a => !a.IsDeleted && a.ApplicationFormConfigurationId == applicationFormId);
                var result = _mapper.Map<List<QuestionConfigurationDTO>>(questionConfigurations);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Question setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("getall/applicationform/type/{applicationFormId}/{type}")]
        public async Task<IActionResult> GetAllConfiguredQuestionsByApplicationFormAndType(Guid applicationFormId, QuestionType type)
        {
            try
            {
                var questionConfigurations = await _questionConfigurationRepository.GetAll(a => !a.IsDeleted && a.ApplicationFormConfigurationId == applicationFormId && a.Type == type);
                var result = _mapper.Map<List<QuestionConfigurationDTO>>(questionConfigurations);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Question setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne(Guid Id)
        {
            try
            {
                var response = await _questionConfigurationRepository.GetOne(x => x.Id == Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving Question setup was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpPut("{Id}/{applicationFormId}")]
        public async Task<IActionResult> Update(Guid Id, Guid applicationFormId, [FromBody] CreateQuestionConfigurationDTO createQuestionConfigurationDTO)
        {
            try
            {
                var response = await _questionConfigurationRepository.UpdateQuestionConfiguration(Id, applicationFormId, createQuestionConfigurationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Question setup modification was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return await _questionConfigurationRepository.DeleteQuestionConfiguration(Id);
            }
            catch (Exception ex)
            {
                return BadRequest("Question setup removal was not successful" + ex.Message ?? ex.InnerException?.Message);

            }
        }
    }
}
