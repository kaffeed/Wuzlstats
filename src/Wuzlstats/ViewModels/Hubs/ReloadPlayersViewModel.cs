﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wuzlstats.ExtensionMethods;
using Wuzlstats.Models;
using Microsoft.Data.Entity;

namespace Wuzlstats.ViewModels.Hubs
{
    public class ReloadPlayersViewModel
    {
        private readonly Db _db;


        public ReloadPlayersViewModel(Db db)
        {
            _db = db;
        }


        public async Task<ReloadPlayersViewModel> Fill(string league)
        {
            var leagueEntity = await CheckAndLoadLeague(league);
            players = await _db.Players.Where(x => x.LeagueId == leagueEntity.Id).Select(x => x.Name).Distinct().ToListAsync();

            return this;
        }


        // ReSharper disable once InconsistentNaming
        public IEnumerable<string> players { get; set; }


        private async Task<League> CheckAndLoadLeague(string league)
        {
            if (league.IsNoE())
            {
                throw new Exception("Invalid league, must not be empty.");
            }
            var leagueEntity = await _db.Leagues.FirstOrDefaultAsync(x => x.Name.Equals(league, StringComparison.CurrentCultureIgnoreCase));
            if (leagueEntity == null)
            {
                throw new Exception("Invalid league.");
            }

            return leagueEntity;
        }
    }
}