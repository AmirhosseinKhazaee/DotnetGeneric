using XGeneric.Attributes;

namespace XGeneric.Models
{
    [XBaseModel]
    public class Tasks : BaseModel
    {
        [XKey]
        public int salam { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}