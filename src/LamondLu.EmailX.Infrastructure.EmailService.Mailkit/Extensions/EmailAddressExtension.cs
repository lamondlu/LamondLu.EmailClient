using System.Collections.Generic;
using LamondLu.EmailX.Domain;
using MimeKit;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.Extensions
{
    public static class MailboxAddressExtension
    {
        public static string UnionAddress(this IEnumerable<MailboxAddress> emailBoxAddresses)
        {
            string result = string.Empty;
            foreach (var emailBoxAddress in emailBoxAddresses)
            {
                result += emailBoxAddress.Address + ";";
            }
            return result;
        }
    }
}