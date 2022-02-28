using MyEvernote.Common;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            //webde getusername alımı
            if (HttpContext.Current.Session["login"] != null)
            {
               EvernoteUser user = HttpContext.Current.Session["login"] as EvernoteUser;
                return user.Username;
            }
            return null;
        }
    }
}