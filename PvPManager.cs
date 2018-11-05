using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.API.Collections;
using System;

namespace Diagonal.PvPManager
{
    public class PvPManager : RocketPlugin<PvPManagerConfiguration>
    {
        public static PvPManager Instance;

        #region Write
        public static void Write(string message)
        {
            Console.WriteLine(message);
        }
        public static void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        #endregion

        #region Load/Unload
        protected override void Load()
        {
            Instance = this;
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;

            #region WriteLoad
            if (Configuration.Instance.PvPOnOffMessage)
            {
                Write("PvP on|off Message: Enabled", ConsoleColor.Green);
            }
            else
            {
                Write("PvP on|off Message: Disabled", ConsoleColor.Red);
            }

            if (Configuration.Instance.PvPOnOffPermission)
            {
                Write("PvP on|off permission: Enabled", ConsoleColor.Green);
                Write($"Permission: {Configuration.Instance.Permission}", ConsoleColor.DarkGreen);
            }
            else
            {
                Write("PvP on|off permission: Disabled", ConsoleColor.Red);
            }
            #endregion
        }

        protected override void Unload()
        {
            Instance = null;
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
        }
        #endregion

        #region Player Connected
        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.PvPOnOffPermission)
            {
                var SteamID = ((UnturnedPlayer)player).CSteamID;
                foreach (var steamPlayer in Provider.clients)
                {
                    if (steamPlayer.playerID.steamID == SteamID && player.HasPermission(Configuration.Instance.Permission))
                    {
                        Provider.isPvP = true;

                        if (Configuration.Instance.PvPOnOffMessage)
                        {
                            UnturnedChat.Say(Translate("pvp_on"));
                        }
                        continue;
                    }

                    UnturnedPlayer Players = UnturnedPlayer.FromSteamPlayer(steamPlayer);

                    if (Players.HasPermission(Configuration.Instance.Permission))
                    {
                        return;
                    }
                }
            }
        }
        #endregion

        #region Player Disconnected
        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.PvPOnOffPermission)
            {
                var SteamID = ((UnturnedPlayer)player).CSteamID;
                foreach (var steamPlayer in Provider.clients)
                {
                    if (steamPlayer.playerID.steamID == SteamID && player.HasPermission(Configuration.Instance.Permission))
                    {
                        Provider.isPvP = false;

                        if (Configuration.Instance.PvPOnOffMessage)
                        {
                            UnturnedChat.Say(Translate("pvp_off"));
                        }
                        continue;
                    }

                    UnturnedPlayer Players = UnturnedPlayer.FromSteamPlayer(steamPlayer);

                    if (Players.HasPermission(Configuration.Instance.Permission))
                    {
                        return;
                    }
                }
            }
        }
        #endregion

        #region Translate
        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                { "pvp_on","PvP enabled!" },
                { "pvp_off","PvP disabled!" },
                { "pvp_on_global","Player {0} enabled PvP!" },
                { "pvp_off_global","Player {0} disabled PvP!" }
            };
        #endregion
    }
}
