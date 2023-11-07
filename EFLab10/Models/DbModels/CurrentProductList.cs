using System;
using System.Collections.Generic;

namespace EFLab10.Models.DbModels
{
    public partial class CurrentProductList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
