using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Common.ItemDropRules.DropConditions;
using XenoMod.Content.Items.Accessories;
using XenoMod.Content.Items.Materials;
using XenoMod.Content.Items.Weapons.Magic;

namespace XenoMod.Common.GlobalItems
{
    public class NPCLoot : GlobalItem
    {

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type)
            {
                case ItemID.FairyQueenBossBag:
                    foreach(var rule in itemLoot.Get())
                    {
                        if (rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.FairyQueenMagicItem))
                        {
                            var original = oneFromOptionsDrop.dropIds.ToList();
                            original.Add(ModContent.ItemType<TomeOfLight>());
                            original.Add(ModContent.ItemType<AbsoluteRadiance>());
                            oneFromOptionsDrop.dropIds = original.ToArray();
                        }

                        DropAfterBoss dropAfterBoss = new("MoonLordHead", () => NPC.downedMoonlord);
                        IItemDropRule conditionalRule = new LeadingConditionRule(dropAfterBoss);
                        IItemDropRule dropRule = new CommonDrop(ModContent.ItemType<PureLight>(), chanceDenominator: 100, chanceNumerator: 65, amountDroppedMinimum: 5, amountDroppedMaximum: 13);
                        conditionalRule.OnSuccess(dropRule);
                        itemLoot.Add(conditionalRule);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
