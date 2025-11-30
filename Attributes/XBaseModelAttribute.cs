namespace XGeneric.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class XBaseModelAttribute : Attribute
    {
        public DateTime CreateAt {get; set;}
        public DateTime UpdateAt{get; set;}
        public bool softDeleted{get; set;}
        public DateTime DeleteAt{get; set;}

        public XBaseModelAttribute()
        {
            
        }
    }
}