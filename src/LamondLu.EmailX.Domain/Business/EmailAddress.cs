namespace LamondLu.EmailX.Domain
{
    public class EmailAddress
    {
        public EmailAddress(string address, string displayName){
            Address = address;
            DisplayName = displayName;
        }

        public EmailAddress(string address){
            Address = address;
        }

        public string Address { get; set; }

        public string DisplayName { get; set; }
    }
}
