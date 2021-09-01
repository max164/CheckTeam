using Facepunch;
using Network;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Oxide.Core.Configuration;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Core;
using Oxide.Game.Rust;
using Rust;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oxide.Plugins
{
    [Info("CheckTeam", "Blessed", "0.0.1")]
    [Description("You can check any player for team")]
    class CheckTeam : RustPlugin
    {
        private void Init()
        {
            permission.RegisterPermission("checkteam.use", this);
        }

        [ChatCommand("checkteam")]
        void CheckTeams(BasePlayer player, string command, string[] args)
        {
            if (!player.IPlayer.HasPermission("checkteam.use"))
            {
                player.ChatMessage("Not enough permissions");
                return; 
            }

            string playerID;
            if (args[0] != null) 
            { 
                playerID = args[0];
            } 
            else //Почему-то не работает
            {
                player.ChatMessage("SteamID is not specified");
                return;
            }

            BasePlayer personForSearch = BasePlayer.Find(playerID);

            if (personForSearch == null)
            {
                player.ChatMessage("Player with this SteamID: " + playerID + " not found");
                return;
            }

            string leader = personForSearch.Team.teamLeader.ToString();
            string members = String.Join(", ", personForSearch.Team.members.ToArray());
            string time = personForSearch.Team.teamLifetime.ToString();
            string id = personForSearch.Team.teamID.ToString();
            string team = personForSearch.Team.members.ToString();

            player.ChatMessage("Team owner: " + leader);
            player.ChatMessage("List of members: " + members);
            player.ChatMessage("Team life time: " + time);
            player.ChatMessage("Team ID: " + id);            
        }
    }
}