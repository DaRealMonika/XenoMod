using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace XenoMod.Content.Items.Accessories
{
    public class Fangs : ModItem
    {
        public override string Texture => "ModLoader/UnloadedItem";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(gold: 1);
            Item.DefaultToAccessory();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FangsPlayer>().Fangs = true;
        }
    }

    public class FangsPlayer : ModPlayer {
        public bool Fangs;

        public override void ResetEffects()
        {
            Fangs = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Fangs && (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed))
            {
                float num = (float)damage * 0.075f;
                if ((int)num != 0 && !(Player.lifeSteal <= 0f))
                {
                    Player.lifeSteal -= num;
                    int num2 = Player.whoAmI;
                    Projectile.NewProjectile(item.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, Player.whoAmI, num2, num);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Fangs && (proj.DamageType == DamageClass.Melee || proj.DamageType == DamageClass.MeleeNoSpeed))
            {
                float num = (float)damage * 0.075f;
                if ((int)num != 0 && !(Player.lifeSteal <= 0f))
                {
                    Player.lifeSteal -= num;
                    int num2 = proj.owner;
                    Projectile.NewProjectile(proj.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, proj.owner, num2, num);
                }
            }
        }
    }
}
