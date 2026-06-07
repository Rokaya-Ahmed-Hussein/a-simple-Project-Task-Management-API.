using BL.DTO.TasksItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation.TasksItemVaidation
{
    public class CreateTaskDTOValidator : AbstractValidator<CreateTaskDTO>
    {
        public CreateTaskDTOValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Task title is required")
               .MaximumLength(100).WithMessage("Task title must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Pending", "InProgress", "Done" }.Contains(status))
                .WithMessage("Status must be one of: Pending, InProgress, Done");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required")
                .Must(priority => new[] { "Low", "Medium", "High" }.Contains(priority))
                .WithMessage("Priority must be one of: Low, Medium, High");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be valid");
        }
    }
}
