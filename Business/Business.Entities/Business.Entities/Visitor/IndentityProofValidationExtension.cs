using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class IndentityProofValidationExtension : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var data = (VisitorMetaData)validationContext.ObjectInstance;

            if (data.IdentityProofNumber == null)
                return new ValidationResult("Identity proof is required.");

            var isValid = true;
            Regex regexp = new Regex("([A-Z]){5}([0-9]){4}([A-Z]){1}$");
            var x = data.IdentityProofTypeID.ToString();
            var message = "";
            //1=Pan card
            if (x == "1")
            {
                message = "pan card number";
            }
            else if (x == "2")  //2=Passport
            {
                message = "passport number";
                //regexp = new Regex("[A-PR-WYa-pr-wy][1-9]\\d" + "\\s?\\d{4}[1-9]$");
            }
            else if (x == "3")  //3=Aadhar
            {
                message = "aadhar card number";
                //regexp = new Regex("^[2-9]{1}[0-9]{3}\\s[0-9]{4}\\s[0-9]{4}$");
            }
            else if (x == "4") //4=Voter
            {
                message = "voter id number";
                //regexp = new Regex("([a-zA-Z]){3}([0-9]){7}?$");
            }

            if (regexp.IsMatch(data.IdentityProofNumber))
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }


            return isValid
                ? ValidationResult.Success
                : new ValidationResult("Please enter a valid " + message);
        }
    }
}
