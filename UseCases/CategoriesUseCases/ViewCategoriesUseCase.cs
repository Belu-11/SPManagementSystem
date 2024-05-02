using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.CategoriesUseCases
{
    public class ViewCategoriesUseCase
    {
        private readonly ICategryRepository categryRepository;

        public ViewCategoriesUseCase(ICategryRepository categryRepository)
        {
            this.categryRepository = categryRepository;
        }
        public IEnumerable<Category> Execute()
        {
            return categryRepository.GetCategories();
        }
    }
}
