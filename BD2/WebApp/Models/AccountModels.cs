using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace WebApp.Models
{

        public class CredentialsModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            public bool IsValid(string userName, string password)
            {
                bool result = false;
                bool resultSpecifiec = false;
                using(var service = new LoginWebServiceReference.LoginService())
                {
                    service.CanLogin(userName, password, out result, out resultSpecifiec);
                }

                return result;
            }

        }

}
