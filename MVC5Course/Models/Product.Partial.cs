﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVC5Course.Models.Validations;

namespace MVC5Course.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 商品數量 { get { return this.OrderLine.Count; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 100 && this.Stock > 5)
            {
                yield return new ValidationResult("價格與庫存數量不合理",
                    new string[] { "Price", "Stock" });
            }

            if (this.OrderLine.Count() > 5 && this.Stock == 0)
            {
                yield return new ValidationResult("Stock 與訂單數量不匹配",
                    new string[] { "Stock" });
            }

            yield break;
        }
    }

    public partial class ProductMetaData
	{
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請輸入商品名稱")]
        //[MinLength(3), MaxLength(30)]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        [DisplayName("商品名稱")]
        [商品名稱必須包含Will字串(ErrorMessage = "商品名稱必須包含Will字串")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = "請設定正確的商品價格範圍")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [DisplayName("商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        [Required]
        //[Range(0, 100, ErrorMessage = "請設定正確的商品價格範圍")]
        [DisplayName("商品庫存")]
        public Nullable<decimal> Stock { get; set; }
    }
}