using AutoMapper;
using DynamicForm.DTOs;
using DynamicForm.Exceptions;
using DynamicForm.IServices;
using DynamicForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Services
{
    public class ApplicationFormConfigurationRepository : GenericRepository<ApplicationFormConfiguration>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationFormConfigurationRepository(IUnitOfWork unitOfwork, IMapper mapper) : base(unitOfwork, mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
        }

        public async Task<ApplicationFormConfigurationDTO> UpdateApplicationFormConfiguration(Guid programId, Guid id, CreateApplicationFormConfigurationDTO createApplicationFormConfigurationDTO)
        {
            if (id == Guid.Empty || programId == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var applicationForm = await base.GetOne(x => x.Id == id && x.ProgramId == programId);
            if (applicationForm == null)
            {
                throw new NotFoundException("Application Form not found");
            }

            ApplicationFormConfiguration updateApplicationFormConfiguration = _mapper.Map<ApplicationFormConfiguration>(createApplicationFormConfigurationDTO);

            applicationForm.Nationality = updateApplicationFormConfiguration.Nationality;
            applicationForm.FirstName = updateApplicationFormConfiguration.FirstName;
            applicationForm.IdNumber = updateApplicationFormConfiguration.IdNumber;
            applicationForm.LastName = updateApplicationFormConfiguration.LastName;
            applicationForm.CurrentResidence = updateApplicationFormConfiguration.CurrentResidence;
            applicationForm.DateOfBirth = updateApplicationFormConfiguration.DateOfBirth;
            applicationForm.Email = updateApplicationFormConfiguration.Email;
            applicationForm.Phone = updateApplicationFormConfiguration.Phone;
            applicationForm.Gender = updateApplicationFormConfiguration.Gender;

            var customQuestions = await _unitOfWork.Context.Set<QuestionConfiguration>()
                .Where(a => a.ApplicationFormConfigurationId == applicationForm.Id && a.ProgramId == programId)
                .ToListAsync();

            if (customQuestions != null && customQuestions?.Count > 0)
            {
                var customQuestionsToRemoveId = customQuestions.Select(s => s.Id);
                var customQuestionsToRemove = await _unitOfWork.Context.Set<QuestionConfiguration>()
                    .Where(x => customQuestionsToRemoveId.Contains(x.Id)).ToListAsync();

                customQuestionsToRemove.ForEach(rp => rp.IsDeleted = true);
            }

            if (updateApplicationFormConfiguration?.CustomQuestions is not null && updateApplicationFormConfiguration?.CustomQuestions?.Count > 0)
            {
                foreach (var question in updateApplicationFormConfiguration.CustomQuestions)
                {
                    question.ApplicationFormConfigurationId = applicationForm.Id;
                    question.ProgramId = programId;
                    question.TypeDescription = question.Type.ToString();
                }
                applicationForm.CustomQuestions = updateApplicationFormConfiguration.CustomQuestions;
            }

            await base.Update(id, applicationForm);
            var result = _mapper.Map<ApplicationFormConfigurationDTO>(applicationForm);

            return result;
        }

        public async Task<ApplicationFormConfigurationDTO> GetById(Guid id, Guid programId)
        {
            if (id == Guid.Empty || programId == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var applicationForm = await base.GetOne(a => a.Id == id && a.ProgramId == programId && !a.IsDeleted);
            if (applicationForm == null)
            {
                throw new NotFoundException("Program Application Form not found");
            }

            var customQuestions = await _unitOfWork.Context.Set<QuestionConfiguration>()
                .Where(a => a.ApplicationFormConfigurationId == applicationForm.Id && a.ProgramId == id)
                .ToListAsync();

            applicationForm.CustomQuestions = customQuestions;

            var result = _mapper.Map<ApplicationFormConfigurationDTO>(applicationForm);

            return result;
        }

        public async Task<IActionResult> DeleteApplicationFormConfiguration(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var applicationForm = await base.GetOne(x => x.Id == id && !x.IsDeleted);
            if (applicationForm == null)
            {
                throw new NotFoundException("Application form not found");
            }

            applicationForm.IsDeleted = true;

            var entry = await Update(id, applicationForm);

            return NoContent();

        }
    }
}
