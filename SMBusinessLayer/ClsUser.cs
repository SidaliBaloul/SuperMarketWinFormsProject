using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMDataLayer;

namespace SMBusinessLayer
{
    public class ClsUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private ClsUser(int userid,string username,string password)
        {
            this.UserName = username;
            this.UserID = userid;
            this.Password = password;
        }

        public static ClsUser GetUserByUserNameAndPassword(string username,string password)
        {
            password = ClsSecurity.ComputeHash(password);

            int userid = 0;

            if (ClsUserData.GetUserByUserNameAndPasswordData(ref userid, username, password))
                return new ClsUser(userid,username,password);
            else
                return null;
        }

        public static bool IsPasswordCorrect(int userid,string password)
        {
            password = ClsSecurity.ComputeHash(password);

            return ClsUserData.IsPasswordCorrect(userid,password);
        }

        public static void UpdateUserPassword(int userid, string password)
        {
            string HashedPassword = ClsSecurity.ComputeHash(password);

            ClsUserData.UpdateUserPasswordData(userid,HashedPassword);
            ClsCurrentUser.Password = HashedPassword;
        }


    }
}
