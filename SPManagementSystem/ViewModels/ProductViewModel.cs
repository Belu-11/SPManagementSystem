﻿using SPManagementSystem.Models;

namespace SPManagementSystem.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<CoreBusiness.Category> Categories { get; set; } = new List<CoreBusiness.Category>();
        public CoreBusiness.Product Product { get; set; } = new CoreBusiness.Product();
    }
}
