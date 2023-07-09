using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.ItemDropRules.Chains;
using XenoMod.Common.ItemDropRules.DropConditions;
using XenoMod.Content.Items.Accessories;
using XenoMod.Content.Items.Weapons.Magic;
using XenoMod.Content.Items.Materials;

namespace XenoMod.Common.GlobalNPCs
{
    public class GlobalNPCLoot : GlobalNPC
    {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (NPCID.Sets.Zombies[npc.type] || npc.type == NPCID.MaggotZombie)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fangs>(), chanceDenominator: 100));
            }
            switch (npc.type)
            {
                case NPCID.BloodZombie:
                case NPCID.ZombieMerman:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fangs>(), chanceDenominator: 50));
                    break;
                case NPCID.Lavabat:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlazingCore>(), chanceDenominator: 3));
                    break;
                case NPCID.HallowBoss:
                    foreach(var rule in npcLoot.Get())
                    {
                        if (rule is LeadingConditionRule leadingConditionRule && leadingConditionRule.condition is Conditions.NotExpert) {
                            foreach(var chained in leadingConditionRule.ChainedRules)
                            {
                                if (chained is TryIfSucceeded chainedRule && chainedRule.RuleToChain is OneFromOptionsDropRule oneFromOptions)
                                {
                                    var original = oneFromOptions.dropIds.ToList();
                                    original.Add(ModContent.ItemType<TomeOfLight>());
                                    original.Add(ModContent.ItemType<AbsoluteRadiance>());
                                    oneFromOptions.dropIds = original.ToArray();
                                }
                            }
                        }
                    }
                    DropAfterBoss dropAfterBoss = new DropAfterBoss("MoonLordHead", () => NPC.downedMoonlord);
                    LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
                    IItemDropRule conditionalRule = new LeadingConditionRule(dropAfterBoss);
                    IItemDropRule dropRule = new CommonDrop(ModContent.ItemType<PureLight>(), chanceDenominator: 100, chanceNumerator: 43, amountDroppedMinimum: 5, amountDroppedMaximum: 13);
                    notExpertRule.OnSuccess(conditionalRule);
                    conditionalRule.OnSuccess(dropRule);
                    npcLoot.Add(conditionalRule);
                    break;
                default:
                    break;
            }
        }
    }
}
