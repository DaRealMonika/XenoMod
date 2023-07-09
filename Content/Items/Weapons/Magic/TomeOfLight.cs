using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Projectiles;

namespace XenoMod.Content.Items.Weapons.Magic
{
    public class TomeOfLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.UseSound = SoundID.Item84;
            Item.material = true;
            Item.mana = 45;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(gold: 25);
            Item.damage = 200;
            Item.knockBack = 3;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FriendlyHallowBossSplitShotCore>();
            Item.shootSpeed = 5;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vel1 = velocity.RotatedBy(0.3);
            Vector2 vel2 = velocity.RotatedBy(-0.3);

            Projectile.NewProjectileDirect(source, position, vel1, type, damage, knockback, player.whoAmI, ai1: 1);
            Projectile.NewProjectileDirect(source, position, vel2, type, damage, knockback, player.whoAmI, ai1: 1);

            return true;
        }
    }
}
