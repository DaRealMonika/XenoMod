using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Projectiles;

namespace XenoMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class CounterStrikePendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 4);
            Item.DefaultToAccessory(26, 32);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CounterStrikePendantPlayer>().CounterStrikePendant = true;
            player.GetModPlayer<CounterStrikePendantPlayer>().item = Item;
        }
    }

    public class CounterStrikePendantPlayer : ModPlayer {
        public bool CounterStrikePendant;
        public Item item;

        public override void ResetEffects()
        {
            CounterStrikePendant = false;
            item = null;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (CounterStrikePendant)
            {
                Color color = Main.hardMode ? Color.Crimson : Color.LimeGreen;
                int dmg = Main.hardMode ? 75 : 17;
                int mul = 1;

                if (!Main.hardMode)
                {
                    if (NPC.downedBoss1) mul++;
                    if (NPC.downedBoss2) mul++;
                    if (NPC.downedBoss3) mul++;
                }

                dmg *= mul;
                Vector2[] pos = {
                    new Vector2(Player.Center.X + 40, Player.Center.Y),
                    new Vector2(Player.Center.X, Player.Center.Y + 40),
                    new Vector2(Player.Center.X + 40, Player.Center.Y + 40),
                    new Vector2(Player.Center.X - 40, Player.Center.Y),
                    new Vector2(Player.Center.X, Player.Center.Y - 40),
                    new Vector2(Player.Center.X - 40, Player.Center.Y - 40),
                    new Vector2(Player.Center.X + 40, Player.Center.Y - 40),
                    new Vector2(Player.Center.X - 40, Player.Center.Y + 40)
                };

                for (int i = 0; i < pos.Length; i++)
                {
                    Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_Accessory(item), pos[i], Vector2.Zero, ModContent.ProjectileType<PlayerSlash>(), dmg, 0, Player.whoAmI);
                    proj.rotation = Player.Center.DirectionTo(proj.Center).ToRotation();
                    (proj.ModProjectile as PlayerSlash).color = color;
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }
    }
}
