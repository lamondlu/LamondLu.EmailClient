using System.Collections.Generic;

namespace LamondLu.EmailX.Domain.Extension
{
    public static class EmailAddressExtension
    {
        public static string UnionAddress(this List<EmailAddress> emailAddresses)
        {
            string result = string.Empty;
            foreach (var emailAddress in emailAddresses)
            {
                result += emailAddress.Address + ";";
            }
            return result;
        }
    }
}