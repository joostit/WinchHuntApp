using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Server.Models.Db;
using WinchHuntApp.Server.Models.Inmemory;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services.Implementation
{
    public class HunterService : IHunterService
    {

        
        private readonly WinchHuntDbContext dbContext;
        private InMemoryDbContext inMemoryDb;

        public HunterService(WinchHuntDbContext ctx, InMemoryDbContext inMemoryDb)
        {
            dbContext = ctx;
            this.inMemoryDb = inMemoryDb;
        }



        public async Task<HunterDto> GetHunter()
        {
            HunterDto result = new HunterDto();
            MemDbHunter dbHunter = await inMemoryDb.Hunters.FirstOrDefaultAsync();
            
            if(dbHunter != null)
            {
                result.Hunter = CreateHunterDto(dbHunter);
            }

            return result;
        }


        public async Task SetHunter(DbSite site, WinchHunter hunter)
        {
            MemDbHunter dbHunter;

            // Just a safeguard in case of inconsistencies. There can be only one ;)
            if (inMemoryDb.Hunters.Count() > 1)
            {
                List<MemDbHunter> toRemove = inMemoryDb.Hunters.ToList();

                toRemove.ForEach(h => inMemoryDb.Hunters.Remove(h));
            }

            if (inMemoryDb.Hunters.Count() == 0)
            {
                dbHunter = new MemDbHunter();
                UpdateHunter(dbHunter, hunter);
                inMemoryDb.Hunters.Add(dbHunter);
            }
            else
            {
                dbHunter = inMemoryDb.Hunters.First();
                UpdateHunter(dbHunter, hunter);
            }

            await inMemoryDb.SaveChangesAsync();
        }


        private void UpdateHunter(MemDbHunter existing, WinchHunter update)
        {
            existing.Name = update.Device.Name;
            existing.DeviceId = update.Device.Id;
            existing.Hardware = update.Device.Hardware;
            existing.Version = update.Device.Version;
            existing.DeviceType = update.Device.DeviceType;
            existing.LastUpdate = update.LastUpdate;
        }


        private WinchHunter CreateHunterDto(MemDbHunter fox)
        {
            WinchHunter dto = new WinchHunter();

            dto.Device.Name = fox.Name;
            dto.Device.Id = fox.DeviceId;
            dto.Device.Hardware = fox.Hardware;
            dto.Device.Version = fox.Version;
            dto.Device.DeviceType = fox.DeviceType;
            dto.LastUpdate = fox.LastUpdate;

            return dto;
        }
    }
}
