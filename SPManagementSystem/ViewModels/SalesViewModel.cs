using SPManagementSystem.Models;

namespace SPManagementSystem.ViewModels
{
    public class SalesViewModel
    {
        public int SelectecCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
