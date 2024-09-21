using Folkin.Data;
using Folkin.Data.Base;
using Folkin.Models;
using Folkin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Folkin.KortMangment
{
    public class KortService : EntityBaseRepository<Kort>, IKortService
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public KortService(DataContext context, UserManager<IdentityUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public string GetUserId(string user)
        {
            var id = _userManager.FindByEmailAsync(user);
            return id.Result.Id;
        }



        public List<PlayerInfo> GetPlayers(string userid)
        {
            List<PlayerInfo> usersResult= new List<PlayerInfo>();
            var users = _userManager.Users;

            foreach (var item in users)
            {
                PlayerInfo tmp = new();
                tmp.UserName = item.UserName;
                tmp.Id = item.Id;
                if(tmp.Id != userid)
                {
                    usersResult.Add(tmp);
                }
            }
            return usersResult;
        }

        public async Task AddNewKortAsync(KortViweModls data, string user)
        {

            var newkort = new Kort()
            {
                Beskivelse = data.Beskivelse,
                Titel = data.Titel,
                SillhouetteId = data.SillhouetteId,
                Abstract = data.Abstract,
                SpillerId = GetUserId(user)

            };
            await _context.Korts.AddAsync(newkort);
            await _context.SaveChangesAsync();

            if (data.TagIds!=null)
            {      
            foreach (var tagId in data.TagIds)
            {       
                var newTagOgKort = new Kort_og_tag()
                {
                KortId = newkort.Id,
                TagId = tagId

                };
                await _context.Korts_og_tags.AddAsync(newTagOgKort);
            }
            await _context.SaveChangesAsync();
            }
            if (data.TypeIds != null)
            {         
            foreach (var type in data.TypeIds)
            {       
                var newtypeOgKort = new Kort_og_Type()
                {
                KortId = newkort.Id,
                TypeId = type

                };
                await _context.Korts_og_Types.AddAsync(newtypeOgKort);
            }
            await _context.SaveChangesAsync();
            }
        }

        public async Task<KortDropdowns> GetKortDropdownsValues()
        {
            var response = new KortDropdowns()
            {
                Types = await _context.Types.OrderBy(n => n.Titel).ToListAsync(),
                Tags = await _context.Tags.OrderBy(n => n.Titel).ToListAsync(),
                Sillhouettes = await _context.Sillhouettes.OrderBy(n => n.Titel).ToListAsync()
            };
            return response;

        }


        public async Task<Kort> GetKortByIdAsync(int id)
        {
            var kortDetails = await _context.Korts
              .Include(c => c.Sillhouette)
              .Include(am => am.Korts_og_tags).ThenInclude(a => a.Tags)
              .Include(am => am.Korts_og_Types).ThenInclude(a => a.Type)
              .FirstOrDefaultAsync(n => n.Id == id);
            return kortDetails;
        }

        public async Task ChangeCardOwner(int cardId, string newUserId)
        {
            var dbKort = await _context.Korts.FirstOrDefaultAsync(n => n.Id == cardId);
            if (dbKort != null)
            {
                dbKort.SpillerId = newUserId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateKortAsync(KortViweModls data)
        {
            var dbKort = await _context.Korts.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbKort != null)
            {
                dbKort.Titel = data.Titel;
                dbKort.Beskivelse = data.Beskivelse;
                dbKort.SillhouetteId = data.SillhouetteId;

                dbKort.Abstract = data.Abstract;

                await _context.SaveChangesAsync();

            }
            //Remove existing Tags
            var existingtagsDb = _context.Korts_og_tags.Where(n => n.KortId == data.Id).ToList();
            var existingtypesDb = _context.Korts_og_Types.Where(n => n.KortId == data.Id).ToList();
            _context.Korts_og_tags.RemoveRange(existingtagsDb);
            _context.Korts_og_Types.RemoveRange(existingtypesDb);
            await _context.SaveChangesAsync();

            //Add kort TAg

            if (data.TagIds!=null)
            {
            foreach (var TagId in data.TagIds)
            {
                var newtagKort = new Kort_og_tag()
                {
                    KortId = data.Id,
                    TagId = TagId
                };
                await _context.Korts_og_tags.AddAsync(newtagKort);
            }


            }
            //Add kort Type
            if (data.TypeIds != null)
            {

            //Add kort Type
            foreach (var TypeId in data.TypeIds)

            {
                var newTypeKort = new Kort_og_Type()
                {
                    KortId = data.Id,
                    TypeId = TypeId
                };
                await _context.Korts_og_Types.AddAsync(newTypeKort);
            }

            }
            await _context.SaveChangesAsync();

        }


    }
}
