using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductListSearchVM : IValidatableObject
    {
        public string q { get; set; }
        public int Stock_S { get; set; }
        public int Stock_E { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Stock_E < Stock_S)
            {
                yield return new ValidationResult("庫存資料篩選條件錯誤", new string[] { "Stock_S", "Stock_E" });
            }
        }
    }
}