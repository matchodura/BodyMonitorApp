using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Helpers.ValidationRules
{
      
    public class CannotBeEmptyRule : ValidationRule 
    {               
        public string ErrorMessage { get; set; }

        public CannotBeEmptyRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            string strValue = Convert.ToString(value);


            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Login cannot be empty!");
            }
            else
            {
                return new ValidationResult(true, null);
            }                        
        
        }

    }


    public class InvalidUserLogin : ValidationRule
    {

        public string ErrorMessage { get; set; }

        public InvalidUserLogin()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Login cannot be empty!");
            }

            if (strValue.Length > 8 )
            {
                return new ValidationResult(false, "Login cannot have more than 8 characters!");
            }
            else
            {
                return new ValidationResult(true, null);
            }

        }

    }


    public class InvalidNameRule : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public InvalidNameRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            Regex regex = new Regex(@"^[aA-zZ\d_\s]+$");

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Name cannot be empty!");
            }
                      
            else if (regex.IsMatch(strValue))
            {
                return new ValidationResult(true, null);
            }

            else
            {
                return new ValidationResult(false, "Invalid data!");
            }

        }

    }


    public class HeightValidationRule : ValidationRule
    {

        public HeightValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);                   
            int i = 0;
            bool canConvert = int.TryParse(strValue, out i);

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Height cannot be empty!");
            }

            if (canConvert && i > 0)
            {
                return new ValidationResult(true, null);
            }
             
            else
            {
                return new ValidationResult(false, $"Invalid value!");
            }

        }

    }


    public class InvalidPassword : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public InvalidPassword()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            string strValue = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Password cannot be empty!");
            }

            if (strValue.Length < 4 && strValue.Length > 8)
            {
                return new ValidationResult(false, "Password cannot have less than 4 and more than 8 characters!");
            }

            else
            {
                return new ValidationResult(true, null);
            }

        }

    }


    public class EmailValidationRule : ValidationRule
    {          
        public EmailValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);
            Regex regex = new Regex(@"^([\w\.\]+)@((?!\.|\-)[\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Mail cannot be empty!");
            }

            if (regex.IsMatch(strValue))
            {

                return new ValidationResult(true, null);

            }

            else
            {
                return new ValidationResult(false, "Wrong E-Mail!");
            }

        }

    }


    public class GenderValidationRule : ValidationRule
    {            
        public GenderValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            string strValue = Convert.ToString(value);


            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Gender cannot be empty!");
            }

            if (strValue == "m" || strValue == "f")
            {

                return new ValidationResult(true, null);
              
            }
            else
            {
                return new ValidationResult(false, "Write m or f");
            }

        }

    }


    public class ProgressValidationRule : ValidationRule
    {
        public ProgressValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);            
            Regex regex = new Regex(@"^\d*\.?\d+$");

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return new ValidationResult(false, "Value cannot be empty!");
            }

            if (regex.IsMatch(strValue))
            {
                return new ValidationResult(true, null);
            }

            else
            {
                return new ValidationResult(false, $"Invalid value!");
            }

        }

    }
}
