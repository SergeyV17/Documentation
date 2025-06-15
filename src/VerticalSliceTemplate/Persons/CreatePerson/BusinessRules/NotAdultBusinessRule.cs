// <copyright file="NotAdultBusinessRule.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using Gems.BusinessRules;

namespace VerticalSliceApi.Persons.CreatePerson.BusinessRules
{
    public class NotAdultBusinessRule : IBusinessRule<CreatePersonCommand>
    {
        public const string ErrorMessage = "Нельзя создать пользователя младше 18 лет";

        public bool IsBroken(CreatePersonCommand person, out string errorMessage)
        {
            if (person.Age < 18)
            {
                errorMessage = ErrorMessage;
                return true;
            }

            errorMessage = string.Empty;
            return false;
        }
    }
}
