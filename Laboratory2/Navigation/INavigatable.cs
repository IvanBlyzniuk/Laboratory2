using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory2.ViewModels;

namespace Laboratory2.Navigation
{
    internal interface INavigatable
    {
        NavigationTypes ViewType
        {
            get;
        }
    }
}
