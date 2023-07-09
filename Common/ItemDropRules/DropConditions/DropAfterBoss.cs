using Humanizer;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace XenoMod.Common.ItemDropRules.DropConditions
{
    public class DropAfterBoss : IItemDropRuleCondition
    {
        public string name = "KingSlime";
        public Func<bool> check = () => NPC.downedSlimeKing;

        public DropAfterBoss() { }

        public DropAfterBoss(string name, Func<bool> check)
        {
            this.name = name;
            this.check = check;
        }
        public bool CanDrop(DropAttemptInfo info)
        {
            if (info.IsInSimulation) return false;
            return check();
        }

        public bool CanShowItemDropInUI () { return true; }

        public string GetConditionDescription()
        {
            return Language.GetTextValue("Mods.XenoMod.DropCondition.AfterBoss", Language.GetTextValue("NPCName." + name));
        }
    }
}
