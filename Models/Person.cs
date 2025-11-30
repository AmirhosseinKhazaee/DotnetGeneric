using XGeneric.Attributes;

namespace XGeneric.Models
{
    [XBaseModel]
    public class Person : BaseModel
    {
        [XKey]
        public Guid Id {get; set;}
        
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}