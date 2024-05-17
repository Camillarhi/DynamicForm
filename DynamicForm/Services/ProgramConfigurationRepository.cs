using AutoMapper;
using DynamicForm.DTOs;
using DynamicForm.Exceptions;
using DynamicForm.IServices;
using DynamicForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Services
{
    public class ProgramConfigurationRepository : GenericRepository<ProgramConfiguration>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProgramConfigurationRepository(IUnitOfWork unitOfwork, IMapper mapper) : base(unitOfwork, mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
        }

        public async Task<ProgramConfigurationDTO> CreateProgramConfiguration(CreateProgramConfigurationDTO createProgramConfigurationDTO)
        {
            ProgramConfiguration program = _mapper.Map<ProgramConfiguration>(createProgramConfigurationDTO);
            program.Id = Guid.NewGuid();

            program.ApplicationForm.ProgramId = program.Id;

            if (program?.ApplicationForm?.CustomQuestions is not null && program?.ApplicationForm?.CustomQuestions.Count > 0)
            {
                foreach (var question in program.ApplicationForm.CustomQuestions)
                {
                    question.ApplicationFormConfigurationId = program.ApplicationForm.Id;
                    question.ProgramId = program.Id;
                    question.TypeDescription = question.Type.ToString();
                }
            }

            var entry = await Create(program);
            var result = _mapper.Map<ProgramConfigurationDTO>(entry.Value);

            return result;
        }

        public async Task<ProgramConfigurationDTO> UpdateProgramConfiguration(Guid id, CreateProgramConfigurationDTO createProgramConfigurationDTO)
        {

            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var program = await base.GetOne(x => x.Id == id);
            if (program == null)
            {
                throw new NotFoundException("Program not found");
            }

            ProgramConfiguration updateProgram = _mapper.Map<ProgramConfiguration>(createProgramConfigurationDTO);

            program.Name = updateProgram.Name;
            program.Description = updateProgram.Description;
            program.ApplicationForm.Nationality = updateProgram.ApplicationForm.Nationality;
            program.ApplicationForm.FirstName = updateProgram.ApplicationForm.FirstName;
            program.ApplicationForm.IdNumber = updateProgram.ApplicationForm.IdNumber;
            program.ApplicationForm.LastName = updateProgram.ApplicationForm.LastName;
            program.ApplicationForm.CurrentResidence = updateProgram.ApplicationForm.CurrentResidence;
            program.ApplicationForm.DateOfBirth = updateProgram.ApplicationForm.DateOfBirth;
            program.ApplicationForm.Email = updateProgram.ApplicationForm.Email;
            program.ApplicationForm.Phone = updateProgram.ApplicationForm.Phone;
            program.ApplicationForm.Gender = updateProgram.ApplicationForm.Gender;

            if (updateProgram?.ApplicationForm?.CustomQuestions is not null && updateProgram?.ApplicationForm?.CustomQuestions.Count > 0)
            {
                foreach (var question in updateProgram.ApplicationForm.CustomQuestions)
                {
                    question.ApplicationFormConfigurationId = program.ApplicationForm.Id;
                    question.ProgramId = program.Id;
                    question.TypeDescription = question.Type.ToString();
                }
            }

            program.ApplicationForm.CustomQuestions = updateProgram.ApplicationForm.CustomQuestions;

            await Update(id, program);
            var result = _mapper.Map<ProgramConfigurationDTO>(program);

            return result;
        }

        public async Task<ProgramConfigurationDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var program = await base.GetOne(x => x.Id == id);
            if (program == null)
            {
                throw new NotFoundException("Program not found");
            }

            var applicationForm = await _unitOfWork.Context.Set<ApplicationFormConfiguration>()
                .Where(a => a.ProgramId == program.Id)
                .FirstOrDefaultAsync();
            if (applicationForm == null)
            {
                throw new NotFoundException("Program Application Form not found");
            }

            var customQuestions = await _unitOfWork.Context.Set<QuestionConfiguration>()
                .Where(a => a.ApplicationFormConfigurationId == applicationForm.Id && a.ProgramId == id)
                .ToListAsync();

            program.ApplicationForm = applicationForm;
            program.ApplicationForm.CustomQuestions = customQuestions;

            var result = _mapper.Map<ProgramConfigurationDTO>(program);

            return result;
        }

        public async Task<IActionResult> DeleteProgramConfiguration(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID");
            }

            var program = await base.GetOne(x => x.Id == id);
            if (program == null)
            {
                throw new NotFoundException("Program not found");
            }

            program.IsDeleted = true;

            var entry = await Update(id, program);

            return NoContent();

        }
    }
}
