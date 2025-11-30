using XGeneric.Attributes;

namespace XGeneric.Models
{
    public class Tasks : BaseModel
    {
        [XKey]
        public Guid salam { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}