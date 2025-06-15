// <copyright file="BusinessRulesChecker.cs" company="Company">
// Copyright (c) Company. All rights reserved.
// </copyright>

using System;

namespace VerticalSliceApi.Persons.CreatePerson.BusinessRules
{
    public class BusinessRulesChecker
    {
        private readonly NotAdultBusinessRule notAdultBusinessRule;

        public BusinessRulesChecker(NotAdultBusinessRule notAdultBusinessRule)
        {
            this.notAdultBusinessRule = notAdultBusinessRule;
        }

        public void CheckAdult(CreatePersonCommand person)
        {
            if (this.notAdultBusinessRule.IsBroken(person, out var errorMessage))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}
