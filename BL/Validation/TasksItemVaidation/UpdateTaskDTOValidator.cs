using BL.DTO.TasksItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation.TasksItemVaidation
{
    public class UpdateTaskDTOValidator : AbstractValidator<UpdateTaskDTO>
    {
        public UpdateTaskDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Task Id must be valid");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Pending", "InProgress", "Done" }.Contains(status))
                .WithMessage("Status must be one of: Pending, InProgress, Done");

        }
    }
}
