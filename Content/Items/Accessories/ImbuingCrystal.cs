using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Accessories
{
    public class ImbuingCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(gold: 36);
            Item.DefaultToAccessory(22, 22);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.CrystalShard, 30)
                .AddIngredient(ItemID.ManaCrystal, 3)
                .AddIngredient(ItemID.SoulofLight, 15)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddTile(TileID.CrystalBall)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ImbuingCrystalPlayer>().Crystal = true;
            player.GetModPlayer<ImbuingCrystalPlayer>().item = Item;
        }
    }

    public class ImbuingCrystalPlayer : ModPlayer
    {
        public bool Crystal;
        public Item item;

        public override void ResetEffects()
        {
            Crystal = false;
            item = null;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Crystal)
            {
                if (XenoMod.isSummon(proj) && XenoMod.isHostileNpc(target))
                {
                    byte meleeEnchant = Player.meleeEnchant;

                    switch (meleeEnchant)
                    {
                        case 1:
                            target.AddBuff(BuffID.Venom, 60 * Main.rand.Next(5, 10));
                            break;
                        case 2:
                            target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(3, 7));
                            break;
                        case 3:
                            target.AddBuff(BuffID.OnFire, 60 * Main.rand.Next(3, 7));
                            break;
                        case 4:
                            target.AddBuff(BuffID.Midas, 120);
                            break;
                        case 5:
                            target.AddBuff(BuffID.Ichor, 60 * Main.rand.Next(10, 20));
                            break;
                        case 6:
                            target.AddBuff(BuffID.Confused, 60 * Main.rand.Next(1, 4));
                            break;
                        case 7:
                            Projectile.NewProjectileDirect(Player.GetSource_Accessory(item), target.Center, target.velocity, ProjectileID.ConfettiMelee, 0, 0, Player.whoAmI);
                            break;
                        case 8:
                            target.AddBuff(BuffID.Poisoned, 60 * Main.rand.Next(5, 10));
                            break;
                    }
                }
            }
        }
    }
}
