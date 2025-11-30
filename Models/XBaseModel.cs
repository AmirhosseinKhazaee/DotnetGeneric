using XGeneric.Attributes;

namespace XGeneric.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool SoftDeleted { get; set; }
    }

}

    /// <summary>
    /// - XBaseModelAttribute:
    /// - [] Force Check Properties has only One XKey Attribute;
    /// - [] if Has XKey, attach CreateAt, UpdateAt, softDeleted, DeleteAt Properties ...
    /// - XKeyAttribute:
    /// - [X] Retrieve Key Property Field Name;
    /// - [X] Retrieve Key Property Field Value;
    /// - [X] Has a Method to Determines Has Key or Not;
    /// </summary>



