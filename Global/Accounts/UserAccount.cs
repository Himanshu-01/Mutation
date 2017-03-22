using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace Global.Accounts
{
    public class UserAccount
    {
        #region Fields

        /// <summary>
        /// Name of the user account.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Indicates if the account is an offline (local) account or
        /// an online account that can be logged into.
        /// </summary>
        public bool IsLocalOnly { get; set; }

        private string sessionId { get; set; }

        private string cookie { get; set; }

        /// <summary>
        /// If the user account is an online account this value indicates that
        /// the account successfully logged in.
        /// </summary>
        public bool IsOnline { get; set; }

        #endregion

        private const string loginForm = "http://remnantmods.com/forums/ucp.php?mode=login";

        public UserAccount()
        {
            // Pull account information from UserAccounts settings file
            UserName = Settings.UserAccount.Default.UserName;
            IsLocalOnly = Settings.UserAccount.Default.LocalUser;

            // Initialize runtime data to default values
            IsOnline = false;
        }

        public bool Login()
        {
            // Check if the account is an online account
            if (IsLocalOnly)
            {
                // The account is local only so no extra processing is required
                return true;
            }

            // Setup a get request for a session id from remnant
            WebRequest request = WebRequest.Create(loginForm);
            request.Method = "GET";

            // Try to send the request and handle any web exceptions that occure.
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException e)
            {
                // Print the exception and return false.
                Console.WriteLine("WebException from remnantmods.com: {0}", e.Message);
                return false;
            }

            // Pull out the session id from the GET request
            string cookie = response.Headers["Set-Cookie"];
            int start = cookie.IndexOf("sid=") + 4;
            sessionId = cookie.Substring(start, cookie.IndexOf(";", start) - start);

            // Create a new web request to POST the login data, and get our login cookie
            request = WebRequest.Create(String.Format("{0}&sid={1}", loginForm, sessionId));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // We may need to prompt the user for their password, check if there
            // is a stored password and if not prompt them for it.
            string password = Settings.UserAccount.Default.Password;
            if (password == null || password == "")
            {
                // TODO: Prompt the user for their password.
            }

            // Format our login credentials
            string credentials = String.Format("username={0}&password={1}&redirect={2}" +
                "&sid={3}&redirect=index.php&login=Login", UrlEncode(UserName),
                UrlEncode(password), UrlEncode("./ucp.php?mode=login"), sessionId);

            // Format our credentials into ascii text
            byte[] data = Encoding.ASCII.GetBytes(credentials);
            request.ContentLength = data.Length;

            // Write the formatted data to the request stream
            using (Stream stream = request.GetRequestStream())
                stream.Write(data, 0, data.Length);

            // Execute the request
            response = request.GetResponse();

            // If we have a valid cookie then we have successfully logged in
            if (response.Headers["Set-Cookie"] != null)
            {
                // Save the cookie
                IsOnline = true;
                this.cookie = response.Headers["Set-Cookie"];

                // Done, successfully logged in
                return true;
            }

            // Failed to log in
            return false;
        }

        #region UrlEncode

        private readonly static string reservedCharacters = "!*'();:@&=+$,/?%#[]";

        private static string UrlEncode(string value)
        {
            if (String.IsNullOrEmpty(value))
                return String.Empty;

            var sb = new StringBuilder();

            foreach (char @char in value)
            {
                if (reservedCharacters.IndexOf(@char) == -1)
                    sb.Append(@char);
                else
                    sb.AppendFormat("%{0:X2}", (int)@char);
            }
            return sb.ToString();
        }

        #endregion
    }
}
