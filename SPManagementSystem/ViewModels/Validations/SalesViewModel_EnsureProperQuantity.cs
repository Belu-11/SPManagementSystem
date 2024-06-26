﻿using SPManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using UseCases.ProductsUseCases;

namespace SPManagementSystem.ViewModels.Validations
{
    public class SalesViewModel_EnsureProperQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var salesViewModel = validationContext.ObjectInstance as SalesViewModel;
            if (salesViewModel != null)
            {
                if(salesViewModel.QuantityToSell <= 0)
                {
                    return new ValidationResult("The quantity to sell must be greater than zero.");
                }
                else
                {
                    var getProductByIdUseCase = validationContext.GetService(typeof(IViewSelectedProductUseCase)) as IViewSelectedProductUseCase;

                    if (getProductByIdUseCase != null)
                    {
                        var product = getProductByIdUseCase.Execute(salesViewModel.SelectedProductId);
                        if (product != null)
                        {
                            if (product.Quantity < salesViewModel.QuantityToSell)
                                return new ValidationResult($"{product.Name} only has {product.Quantity} left");
                        }
                    }
                    else
                    {
                        return new ValidationResult("The selected product does not exists.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
