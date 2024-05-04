﻿using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        void DeleteCategory(int categoryId);
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategoryById(int categoryId);
        void UpdateCategory(int categoryId, Category category);
    }
}