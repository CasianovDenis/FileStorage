using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageClient.Models
{
    public class ValidationUserData : AbstractValidator<Users_ManageProject>
    {
        public ValidationUserData(Users_ManageProject usersViewModel)
        {

            RuleFor(customer => usersViewModel.Username).NotNull().WithMessage("Username is required");
            RuleFor(customer => usersViewModel.Email).NotNull().WithMessage("Email is required");
            RuleFor(customer => usersViewModel.Password).NotNull().WithMessage("Field Password is empty");
            RuleFor(customer => usersViewModel.Password_verification).NotNull().WithMessage("Field Password is empty");

        }
    }
}
