using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GigHub.Core.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] formats = { "d MMM yyyy", "dd MMM yyyy", "dd M yyyy", "d M yyyy", "d/MM/yyyy",
                    "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "d/M/yyyy"};

            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                formats, 
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, 
                out dateTime );
            return isValid && dateTime > DateTime.Now;
        }
    }
}