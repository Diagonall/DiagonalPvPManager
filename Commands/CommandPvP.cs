using Rocket.API;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace Diagonal.PvPManager
{
    public class CommandPvP : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "pvp";

        public string Help => "Enable or Disable PvP";

        public string Syntax => "Erro! Use /pvp on|off";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "pvplimiter.pvp" };

        public void Execute(IRocketPlayer caller, string[] command)
        {

            if (command.Length == 1)
            {
                if (command[0].ToLower().Equals("on"))
                {
                    if (PvPManager.Instance.Configuration.Instance.PvPOnOffMessage)
                    {
                        UnturnedChat.Say(PvPManager.Instance.Translate("pvp_on_global", caller.DisplayName), Color.green);
                    }

                    Provider.isPvP = true;
                    UnturnedChat.Say(caller, PvPManager.Instance.Translate("pvp_on"), Color.green);
                    return;
                }
                else
                {
                    if (command[0].ToLower().Equals("off"))
                    {
                        if (PvPManager.Instance.Configuration.Instance.PvPOnOffMessage)
                        {
                            UnturnedChat.Say(PvPManager.Instance.Translate("pvp_off_global", caller.DisplayName), Color.red);
                        }

                        Provider.isPvP = false;
                        UnturnedChat.Say(caller, PvPManager.Instance.Translate("pvp_off"), Color.red);
                        return;
                    }
                }
            }
            else
            {
                UnturnedChat.Say(caller, Syntax, Color.red);
                return;
            }
        }
    }
}
