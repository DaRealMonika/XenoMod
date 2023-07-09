using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Waist)]
    public class HellFlameDoll : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 24);
            Item.DefaultToAccessory(34, 34);
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe()
                .AddIngredient(ItemID.LivingFireBlock, 100)
                .AddIngredient(ItemID.LivingCursedFireBlock, 100)
                .AddIngredient(ItemID.LivingDemonFireBlock, 100)
                .AddIngredient(ItemID.LivingFrostFireBlock, 100)
                .AddIngredient(ItemID.Silk, 15)
                .AddTile(TileID.Loom)
                .Register();
            Recipe recipe2 = CreateRecipe()
                .AddIngredient(ItemID.LivingFireBlock, 100)
                .AddIngredient(ItemID.LivingIchorBlock, 100)
                .AddIngredient(ItemID.LivingDemonFireBlock, 100)
                .AddIngredient(ItemID.LivingFrostFireBlock, 100)
                .AddIngredient(ItemID.Silk, 15)
                .AddTile(TileID.Loom)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 1;
            player.GetModPlayer<HellFlameDollPlayer>().Doll = true;
            player.GetModPlayer<HellFlameDollPlayer>().item = Item;
        }
    }

    public class HellFlameDollPlayer : ModPlayer
    {
        public bool Doll;
        public Item item;

        public override void ResetEffects()
        {
            Doll = false;
            item = null;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Doll)
            {
                if (XenoMod.isSummon(proj) && XenoMod.isHostileNpc(target))
                {
                    target.AddBuff(BuffID.Oiled, 60 * Main.rand.Next(8, 15));
                    if (Main.rand.NextBool(4)) target.AddBuff(BuffID.OnFire3, 60 * Main.rand.Next(6, 10));
                    if (Main.rand.NextBool(4)) target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(6, 10));
                    if (Main.rand.NextBool(4)) target.AddBuff(BuffID.Frostburn2, 60 * Main.rand.Next(6, 10));
                    if (Main.rand.NextBool(4)) target.AddBuff(BuffID.ShadowFlame, 60 * Main.rand.Next(6, 10));
                }
            }
        }
    }
}
