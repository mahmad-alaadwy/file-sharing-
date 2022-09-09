using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Helper.Email
{
    public interface IMailService
    {
        void SendEmail(InputEmailMessage model);
    }
}
