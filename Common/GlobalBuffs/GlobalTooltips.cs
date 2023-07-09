using Humanizer;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using XenoMod.Content.Buffs;

namespace XenoMod.Common.GlobalBuffs
{
    public class GlobalTooltips : GlobalBuff
    {
        public override void ModifyBuffTip(int type, ref string tip, ref int rare)
        {
            // This code adds a more extensible remaining time tooltip for suitable buffs
            Player player = Main.LocalPlayer;

            if (type == ModContent.BuffType<LunaticShots>())
            {
                int buffIndex = player.FindBuffIndex(type);
                if (buffIndex < 0 || buffIndex >= player.buffTime.Length)
                {
                    return;
                }

                tip = Language.GetTextValue(XenoMod.BuffDescLocal + "LunaticShots", player.GetModPlayer<LunaticShotsPlayer>().Counter);
            }
        }
    }
}
