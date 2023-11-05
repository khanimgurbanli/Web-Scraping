using CreateCategory_Task.Entities.Common;

namespace Test.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string? ImagePath { get; set; }
        public string CategoryName { get; set; }
        public string ProductLink { get; set; }
    }
}
