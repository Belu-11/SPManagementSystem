using SPManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using UseCases.ProductsUseCases;

namespace SPManagementSystem.ViewModels
{
    public class SalesViewModel
    {
        public int SelectedCategoryId { get; set; }

        public IEnumerable<CoreBusiness.Category> Categories { get; set; } = new List<CoreBusiness.Category>();

        public int SelectedProductId { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue)]
        [Validations.SalesViewModel_EnsureProperQuantity]
        public int QuantityToSell { get; set; }
    }
}
