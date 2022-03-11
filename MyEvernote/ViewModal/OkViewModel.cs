using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.ViewModal
{
    public class OkViewModel:NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "İşlem Başarılı.";

        }
    }
}