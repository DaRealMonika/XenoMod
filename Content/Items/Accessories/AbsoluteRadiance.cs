using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Projectiles;

namespace XenoMod.Content.Items.Accessories
{
    public class AbsoluteRadiance : ModItem
    {
        public override string Texture => "ModLoader/UnloadedItem";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(gold: 36);
            Item.DefaultToAccessory();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AbsoluteRadiancePlayer>().AbsoluteRadiance = true;
            player.GetModPlayer<AbsoluteRadiancePlayer>().item = Item;
        }
    }

    public class AbsoluteRadiancePlayer : ModPlayer {
        public bool AbsoluteRadiance;
        public Item item;

        public override void ResetEffects()
        {
            AbsoluteRadiance = false;
            item = null;
        }

        // Vanilla applies immunity time before this method and after PreHurt and Hurt
        // Therefore, we should apply our immunity time increment here
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            // Here we increase the player's immunity time by 1 second when Example Immunity Accessory is equipped
            if (!AbsoluteRadiance)
            {
                return;
            }

            // Different cooldownCounter values mean different damage types taken and different cooldown slots
            // See ImmunityCooldownID for a list.
            // Don't apply extra immunity time to pvp damage (like vanilla)
            if (!pvp)
            {
                Player.AddImmuneTime(cooldownCounter, 60);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (AbsoluteRadiance)
            {
                Projectile.NewProjectileDirect(Player.GetSource_Accessory(item), Player.Center, Vector2.Zero, ModContent.ProjectileType<FriendlyHallowBossDeathAurora>(), 700, 0, Player.whoAmI);
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }
    }
}
