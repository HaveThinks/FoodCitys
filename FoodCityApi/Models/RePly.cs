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
    
    public partial class RePly
    {
        public int RePlyID { get; set; }
        public Nullable<int> PostEvaluationID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> ReplyDate { get; set; }
        public string Content { get; set; }
    }
}
