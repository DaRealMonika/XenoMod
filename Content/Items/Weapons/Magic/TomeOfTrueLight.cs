using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using XenoMod.Content.Items.Materials;
using XenoMod.Content.Projectiles;

namespace XenoMod.Content.Items.Weapons.Magic
{
    public class TomeOfTrueLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.UseSound = SoundID.Item84;
            Item.mana = 45;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(gold: 25);
            Item.damage = 300;
            Item.knockBack = 6;
            Item.crit = 3;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FriendlyHallowBossSplitShotCore>();
            Item.shootSpeed = 5;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 velRain = new(5, 0);

            for (int i = 0; i < 6; i++)
            {
                Projectile.NewProjectileDirect(source, position, velRain.RotatedBy(i * MathF.PI / 3), ModContent.ProjectileType<FriendlyHallowBossEverlastingRainbow>(), damage*2, knockback/2, player.whoAmI);
            }

            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(MathF.PI * 2), ModContent.ProjectileType<FriendlyHallowBossRainbowStreak>(), damage*4, knockback/4, player.whoAmI);
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (XenoMod.FindTooltipIndex(tooltips, "Tooltip0", "Terraria", out int index))
            {
                float colorMult = Main.mouseTextColor / 255f;
                Color discoColor = Main.DiscoColor * colorMult;
                tooltips[index] = new(Mod, "Tooltip0", Language.GetTextValue(XenoMod.ItemTipLocal + "TomeOfTrueLight", discoColor.Hex3()));
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient<PureLight>(10)
                .AddIngredient<TomeOfLight>()
                .AddIngredient(ItemID.WaterBolt)
                .AddIngredient(ItemID.RazorbladeTyphoon)
                .AddIngredient(ItemID.CrystalStorm)
                .AddIngredient(ItemID.MagnetSphere)
                .AddIngredient(ItemID.LunarBar, 15)
                .AddIngredient(ItemID.FragmentSolar, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddIngredient(ItemID.FragmentNebula, 5)
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddTile(TileID.LunarCraftingStation)
                .AddTile(TileID.CrystalBall)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}
