using BL.DTO.ProjectItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation.ProjectItemValidation
{
    public class CreateProjectDTOValidator : AbstractValidator<CreateProjectDTO>
    {
        public CreateProjectDTOValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required")
            .MaximumLength(100).WithMessage("Project name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}
