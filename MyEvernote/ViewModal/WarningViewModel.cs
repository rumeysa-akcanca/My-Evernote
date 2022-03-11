using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.ViewModal
{
    public class WarningViewModel : NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Uyarı!";
        }
    }
}