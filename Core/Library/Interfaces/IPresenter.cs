using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Forms.Interfaces
{
    public interface IPresenter
    {
        void PresentPage(string page, IParametersService parameters = null);
    }
}
