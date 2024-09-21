

using Folkin.Data.Base;
using Folkin.Models;
using Folkin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Folkin.KortMangment
{
    public interface IKortService :IEntityBaseRepository<Kort>
    {
        Task<KortDropdowns> GetKortDropdownsValues();
        Task AddNewKortAsync(KortViweModls data, string user);

        Task<Kort> GetKortByIdAsync(int id);

        Task UpdateKortAsync(KortViweModls data);
        List<PlayerInfo> GetPlayers(string user);
        Task ChangeCardOwner(int cardid, string newUserId);
    }
}
