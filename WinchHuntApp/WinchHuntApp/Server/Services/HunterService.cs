using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinchHuntApp.Server.Data;
using WinchHuntApp.Server.Data.Db;
using WinchHuntApp.Shared.Dto;

namespace WinchHuntApp.Server.Services
{
    public class HunterService : IHunterService
    {

        private List<DbHunter> Hunters { get; set; } = new List<DbHunter>();
        private readonly WinchHuntContext context;

        public HunterService(WinchHuntContext ctx)
        {
            context = ctx;
        }



        public async Task<HunterDto> GetHunter()
        {
            HunterDto result = new HunterDto();
            DbHunter dbHunter = Hunters.FirstOrDefault();
            
            if(dbHunter != null)
            {
                result.Hunter = CreateHunterDto(dbHunter);
            }

            return result;
        }


        public async Task SetHunter(WinchHunter hunter)
        {
            DbHunter dbHunter;

            // Just a safeguard in case of inconsistencies. There can be only one ;)
            if (Hunters.Count() > 1)
            {
                List<DbHunter> toRemove = Hunters.ToList();

                toRemove.ForEach(h => Hunters.Remove(h));
            }

            if (Hunters.Count() == 0)
            {
                dbHunter = new DbHunter();
                UpdateHunter(dbHunter, hunter);
                Hunters.Add(dbHunter);
            }
            else
            {
                dbHunter = Hunters.First();
                UpdateHunter(dbHunter, hunter);
            }

            
        }


        private void UpdateHunter(DbHunter existing, WinchHunter update)
        {
            existing.Name = update.Device.Name;
            existing.DeviceId = update.Device.Id;
            existing.Hardware = update.Device.Hardware;
            existing.Version = update.Device.Version;
            existing.DeviceType = update.Device.DeviceType;
            existing.LastUpdate = update.LastUpdate;
        }


        private WinchHunter CreateHunterDto(DbHunter fox)
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
