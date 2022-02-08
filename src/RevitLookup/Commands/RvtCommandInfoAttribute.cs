namespace RevitLookup.Commands
{
    public class RvtCommandInfoAttribute : Attribute
    {
        public string Name { get; set; }

        public string Description {  get; set; }

        public string Image { get;set; }
    }
}
