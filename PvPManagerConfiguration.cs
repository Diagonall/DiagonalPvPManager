﻿using Rocket.API;

namespace Diagonal.PvPManager
{
    public class PvPManagerConfiguration : IRocketPluginConfiguration
    {
        public bool PvPOnOffMessage;
        public bool PvPOnOffPermission;
        public string Permission;

        public void LoadDefaults()
        {
            PvPOnOffMessage = true;
            PvPOnOffPermission = true;
            Permission = "pvp.limiter";
        }
    }
}
