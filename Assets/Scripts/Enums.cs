using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum GameScenes
    {
        None = 0,
        MapScene,
        EncounterScene,
        DungeonScene
    }

    public enum MagicalEffects
    {
        None,
        Poison,
        Bleed,
        Weakness,
    }

    public enum BuffEffects
    {
        Restore,
        Health,
        HealthRegen,
        Power,
        PowerRegen,
        Magic,
        MagicRegen
    }

    public enum MeleeEffects
    {
        None,
        ArmorBreak,
        ArmorPiercing,
        Steal,
    }
}
