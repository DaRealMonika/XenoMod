using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Buffs;
using XenoMod.Content.Projectiles;

namespace XenoMod.Content.Items.Weapons.Summon
{
    public class RodOfLunatics : ModItem
    {
        public override string Texture => "ModLoader/UnloadedItem";

        public void makeProj(Projectile projectile)
        {
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.timeLeft = 100;
            projectile.DamageType = DamageClass.Summon;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 25;
            Item.UseSound = SoundID.Item9;
            Item.mana = 16;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.buyPrice(gold: 54, silver: 36);
            Item.damage = 76;
            Item.knockBack = 1;
            Item.DamageType = DamageClass.Summon;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<LunaticShot>();
            Item.shootSpeed = 25;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.GetModPlayer<LunaticShotsPlayer>().Counter = 17;
            player.AddBuff(ModContent.BuffType<LunaticShots>(), 10);

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.FragmentSolar, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddIngredient(ItemID.FragmentNebula, 5)
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
