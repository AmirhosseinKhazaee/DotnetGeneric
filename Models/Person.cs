using XGeneric.Attributes;

namespace XGeneric.Models
{
    public class Person : BaseModel
    {
        [XKey]
        public Guid Id {get; set;}
        
        [XKey]
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}