using AutoMapper;
using DynamicForm.DTOs;
using DynamicForm.Exceptions;
using DynamicForm.IServices;
using DynamicForm.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Services
{
    public class QuestionConfigurationRepository : GenericRepository<QuestionConfiguration>
    {
        private readonly IMapper _mapper;

        public QuestionConfigurationRepository(IUnitOfWork unitOfwork, IMapper mapper) : base(unitOfwork, mapper)
        {
            _mapper = mapper;
        }


        public async Task<QuestionConfigurationDTO> UpdateQuestionConfiguration(Guid id, Guid applicationFormConfigurationId, CreateQuestionConfigurationDTO updateQuestionConfigurationDTO)
        {
            if (id == Guid.Empty || applicationFormConfigurationId == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var questionConfig = await base.GetOne(x => x.Id == id && x.ApplicationFormConfigurationId == applicationFormConfigurationId);
            if (questionConfig == null)
            {
                throw new NotFoundException("Question Configuration not found");
            }

            QuestionConfiguration updateQuestionConfiguration = _mapper.Map<QuestionConfiguration>(updateQuestionConfigurationDTO);

            questionConfig.Question = updateQuestionConfiguration.Question;
            questionConfig.ChoiceAllowed = updateQuestionConfiguration.ChoiceAllowed;
            questionConfig.Choices = updateQuestionConfiguration.Choices?.Count() > 0 ? updateQuestionConfiguration?.Choices : null;
            questionConfig.EnableOtherOption = updateQuestionConfigurationDTO.EnableOtherOption;
            questionConfig.Type= updateQuestionConfiguration.Type;
            questionConfig.TypeDescription = updateQuestionConfiguration.Type.ToString();

            await Update(id, questionConfig);
            var result = _mapper.Map<QuestionConfigurationDTO>(questionConfig);

            return result;
        }

        public async Task<IActionResult> DeleteQuestionConfiguration(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var program = await base.GetOne(x => x.Id == id);
            if (program == null)
            {
                throw new NotFoundException("Question configuration not found");
            }

            program.IsDeleted = true;

            var entry = await Update(id, program);

            return NoContent();

        }
    }
}
