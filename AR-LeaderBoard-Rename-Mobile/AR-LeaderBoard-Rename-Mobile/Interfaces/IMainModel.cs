using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR_LeaderBoard_Rename_Mobile.Interfaces
{
    public interface IMainModel
    {
        event EventHandler<bool>? RequestSent;
        void SendRequest(string url, string oldTeamName, string newTeamName);
    }
}
