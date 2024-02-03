using Exiled.API.Interfaces;
using InventorySystem.Items.Usables.Scp330;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalaRosa
{
    public sealed class Config : IConfig
    {

        [Description("Wheter the plugin is Enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;

        [Description("Chances of getting each candy")]
        public Dictionary<CandyKindID, float> Chances { get; set; } = new()
        {
            { CandyKindID.Red, 15.83f },
            { CandyKindID.Purple, 15.83f },
            { CandyKindID.Green, 15.83f },
            { CandyKindID.Blue, 15.83f },
            { CandyKindID.Yellow, 15.83f },
            { CandyKindID.Rainbow, 15.83f },
            { CandyKindID.Pink, 5.02f },
        };

        [Description("Number of times players can interact with Scp330 before their hands get severed")]
        public int MaxInteractionAmount { get; set; } = 2;

        [Description("Chance of severing the player's hands after reaching the limit of interactions")]
        public float SeveringHandsChance { get; set; } = 100f;

    }
}
