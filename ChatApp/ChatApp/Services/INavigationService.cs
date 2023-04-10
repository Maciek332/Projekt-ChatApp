using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    internal interface INavigationService
    {
        void Navigate(Type sourcePageType);
        void GoBack();
    }
}
