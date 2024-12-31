public interface Icategory
{
    Task<IEnumerable<Category>> GetAllCategoryAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
}
