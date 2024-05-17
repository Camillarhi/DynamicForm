using AutoMapper;
using DynamicForm.Dtos;
using DynamicForm.DTOs;
using DynamicForm.IServices;
using DynamicForm.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateApplicationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        CandidateApplicationRepository _candidateApplicationRepository;

        public CandidateApplicationController(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _candidateApplicationRepository = new CandidateApplicationRepository(_unitOfWork, mapper);
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllCandidateForms()
        {
            try
            {
                var candidateApplications = await _candidateApplicationRepository.GetAll(a => !a.IsDeleted);
                var result = _mapper.Map<List<CandidateApplicationDTO>>(candidateApplications);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving candidate application was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("getall/program/{programId}")]
        public async Task<IActionResult> GetConfiguredPrograms(Guid programId)
        {
            try
            {
                var candidateApplications = await _candidateApplicationRepository.GetAll(a => !a.IsDeleted && a.ProgramId == programId);
                var result = _mapper.Map<List<CandidateApplicationDTO>>(candidateApplications);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving candidate applications was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne(Guid Id)
        {
            try
            {
                var response = await _candidateApplicationRepository.GetById(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieving candidate application was not successful " + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCandidateApplicationDTO createCandidateApplicationDTO)
        {
            try
            {
                var response = await _candidateApplicationRepository.CreateCandidateApplication(createCandidateApplicationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Candidate application was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] CreateCandidateApplicationDTO createCandidateApplicationDTO)
        {
            try
            {
                var response = await _candidateApplicationRepository.UpdateCandidateApplication(Id, createCandidateApplicationDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Candidate application form modification was not successful" + ex.Message ?? ex.InnerException?.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return await _candidateApplicationRepository.DeleteApplication(Id);
            }
            catch (Exception ex)
            {
                return BadRequest("Program setup removal was not successful" + ex.Message ?? ex.InnerException?.Message);

            }
        }
    }
}
