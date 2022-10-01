namespace QnA.DbModels
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
