using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Infrastructure.EmailService.Mailkit.Extensions
{
    public static class StringExtension
    {
        public static string GetSafeFileName(this string file_name)
        {
            if (string.IsNullOrEmpty(file_name) || string.IsNullOrWhiteSpace(file_name))
            {
                return null;
            }

            //remove special word \u202c
            return file_name.Split("/").Last().RemoveSpecialWords();
        }

        public static string RemoveSpecialWords(this string source)
        {
            //remove special word \u202c
            return source?.Replace("‬", string.Empty);
        }
    }
}