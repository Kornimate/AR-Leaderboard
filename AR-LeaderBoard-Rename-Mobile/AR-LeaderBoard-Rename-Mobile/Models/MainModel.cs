using AR_LeaderBoard_Rename_Mobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR_LeaderBoard_Rename_Mobile.Models
{
    public class MainModel : IMainModel
    {
        public event EventHandler<bool>? RequestSent;

        public void SendRequest(string url, string oldTeamName, string newTeamName)
        {
            throw new NotImplementedException();
        }
    }
}
