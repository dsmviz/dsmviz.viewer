﻿using System.Windows.Controls;

namespace Dsmviz.Viewer.View.Validation
{
    public class ElementNameMustBeNonEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object? value, System.Globalization.CultureInfo cultureInfo)
        {
            string name = value?.ToString() ?? string.Empty;
            return name.Length == 0 ? new ValidationResult(false, "Please enter non empty string") : new ValidationResult(true, null);
        }
    }
}
