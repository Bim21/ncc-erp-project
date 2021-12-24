using ProjectManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.NccCore.Helper
{
    public class UserHelper
    {
        public static string GetUserName(string emailAddress)
        {
            if (!string.IsNullOrEmpty(emailAddress))
            {
                var gmailFormat = "@ncc.asia";
                var userName = emailAddress.Contains(gmailFormat) ? emailAddress.Replace(gmailFormat, "") : null;
                return userName;
            }
            return null;

        }
    }
}
