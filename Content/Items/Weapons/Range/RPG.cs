using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Weapons.Range
{
    public class RPG : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 166;
            Item.height = 54;
            Item.UseSound = SoundID.Item5;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 3);
            Item.damage = (int)(65 / 3);
            Item.crit = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 14;
            Item.useAmmo = ItemID.Grenade;
            Item.scale /= 2;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i <= 3; i++)
            {
                Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(MathF.PI / 6), type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-95f, -2f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Boomstick)
                .AddRecipeGroup(nameof(ItemID.Grenade), 20)
                .AddRecipeGroup("IronBar", 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
