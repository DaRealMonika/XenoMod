using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Projectiles;

namespace XenoMod.Content.Items.Weapons.Magic
{
    public class CactusStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 25;
            Item.UseSound = SoundID.Item39;
            Item.mana = 13;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 36);
            Item.damage = 23;
            Item.knockBack = 2;
            Item.crit = 3;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CactusStormProj>();
            Item.shootSpeed = 12.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i <= 2; i++)
            {
                Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(0.2), ModContent.ProjectileType<CactusStormProj>(), damage, knockback, player.whoAmI);
            }

            return true;
        }

        // The following method makes the gun slightly inaccurate
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe()
                .AddIngredient(ItemID.Cactus, 15)
                .AddIngredient(ItemID.RollingCactus, 5)
                .AddIngredient(ItemID.SandBlock, 5)
                .AddIngredient(ItemID.DemoniteBar, 15)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe recipe2 = CreateRecipe()
                .AddIngredient(ItemID.Cactus, 15)
                .AddIngredient(ItemID.RollingCactus, 5)
                .AddIngredient(ItemID.SandBlock, 5)
                .AddIngredient(ItemID.CrimtaneBar, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
