using AutoMapper;
using DynamicForm.Dtos;
using DynamicForm.DTOs;
using DynamicForm.Exceptions;
using DynamicForm.IServices;
using DynamicForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Services
{
    public class CandidateApplicationRepository : GenericRepository<CandidateApplication>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateApplicationRepository(IUnitOfWork unitOfwork, IMapper mapper) : base(unitOfwork, mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
        }

        public async Task<CandidateApplicationDTO> CreateCandidateApplication(CreateCandidateApplicationDTO createCandidateApplicationDTO)
        {
            CandidateApplication candidateApplication = _mapper.Map<CandidateApplication>(createCandidateApplicationDTO);
            candidateApplication.Id = Guid.NewGuid();

            if (candidateApplication?.AdditionalQuestions != null && candidateApplication?.AdditionalQuestions?.Count > 0)
            {
                foreach (var question in candidateApplication.AdditionalQuestions)
                {
                    question.CandidateApplicationId = candidateApplication.Id;
                }
            }

            var entry = await Create(candidateApplication);
            var result = _mapper.Map<CandidateApplicationDTO>(entry.Value);

            return result;
        }

        public async Task<CandidateApplicationDTO> UpdateCandidateApplication(Guid id, CreateCandidateApplicationDTO updateCandidateApplicationDTO)
        {

            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var candidateApplication = await base.GetOne(x => x.Id == id);
            if (candidateApplication == null)
            {
                throw new NotFoundException("Program not found");
            }

            CandidateApplication updateCandidateApplication = _mapper.Map<CandidateApplication>(updateCandidateApplicationDTO);

            candidateApplication.FirstName = updateCandidateApplication.FirstName;
            candidateApplication.LastName = updateCandidateApplication.LastName;
            candidateApplication.IdNumber = updateCandidateApplication.IdNumber;
            candidateApplication.DateOfBirth = updateCandidateApplication.DateOfBirth;
            candidateApplication.CurrentResidence = updateCandidateApplication.CurrentResidence;
            candidateApplication.Email = updateCandidateApplication.Email;
            candidateApplication.Phone = updateCandidateApplication.Phone;
            candidateApplication.Nationality = updateCandidateApplication.Nationality;
            candidateApplication.Gender = updateCandidateApplication.Gender;

            var additionalQuestions = await _unitOfWork.Context.Set<Question>()
               .Where(a => a.CandidateApplicationId == candidateApplication.Id)
               .ToListAsync();

            if (additionalQuestions != null && additionalQuestions?.Count > 0)
            {
                var additionalQuestionsToRemoveId = additionalQuestions.Select(s => s.Id);
                var additionalQuestionsToRemove = await _unitOfWork.Context.Set<Question>()
                    .Where(x => additionalQuestionsToRemoveId.Contains(x.Id)).ToListAsync();

                additionalQuestionsToRemove.ForEach(rp => rp.IsDeleted = true);
            }

            if (updateCandidateApplication?.AdditionalQuestions is not null && updateCandidateApplication?.AdditionalQuestions?.Count > 0)
            {
                foreach (var question in updateCandidateApplication.AdditionalQuestions)
                {
                    question.CandidateApplicationId = candidateApplication.Id;
                }
                candidateApplication.AdditionalQuestions = updateCandidateApplication.AdditionalQuestions;
            }

            await Update(id, candidateApplication);
            var result = _mapper.Map<CandidateApplicationDTO>(candidateApplication);

            return result;
        }

        public async Task<CandidateApplicationDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var candidateApplication = await base.GetOne(x => x.Id == id);
            if (candidateApplication == null)
            {
                throw new NotFoundException("Candidate Application not found");
            }

            var additionalQuestions = await _unitOfWork.Context.Set<Question>()
                .Where(a => a.CandidateApplicationId == candidateApplication.Id)
                .ToListAsync();

            candidateApplication.AdditionalQuestions = additionalQuestions;

            var result = _mapper.Map<CandidateApplicationDTO>(candidateApplication);

            return result;
        }

        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var candidateApplication = await base.GetOne(x => x.Id == id);
            if (candidateApplication == null)
            {
                throw new NotFoundException("Candidate Application not found");
            }

            candidateApplication.IsDeleted = true;

            var entry = await Update(id, candidateApplication);

            return NoContent();

        }
    }
}
