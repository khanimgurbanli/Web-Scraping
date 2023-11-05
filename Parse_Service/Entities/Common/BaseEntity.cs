namespace CreateCategory_Task.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }

        virtual public DateTime? CreatedDate { get; set; }
        virtual public DateTime? UpdatedDate { get; set; }
    }
}
