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
        event EventHandler<int>? EntriesReceivedAsString;
        Task SendRequest(string url, string oldTeamName, string newTeamName);
        Task GetNumberOfEntries(string url);
    }
}
