//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodCityApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Store { get; set; }
        public Nullable<decimal> Shipping { get; set; }
        public Nullable<int> Sales { get; set; }
        public string Details { get; set; }
        public string image { get; set; }
        public Nullable<int> CategoryId { get; set; }
    }
}
