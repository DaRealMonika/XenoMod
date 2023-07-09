using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Accessories
{
    public class SweetTooth : ModItem
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient<Fangs>()
                .AddIngredient(ItemID.HoneyComb)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FangsPlayer>().Fangs = true;
            player.GetModPlayer<SweetToothPlayer>().Fangs = true;
            player.GetModPlayer<SweetToothPlayer>().item = Item;
            player.honeyCombItem = Item;
        }
    }

    public class SweetToothPlayer : ModPlayer
    {
        public bool Fangs;
        public Item item;

        public void makeProj()
        {
            int ext1 = Main.rand.NextBool(33) ? 1 : 0;
            int ext2 = Main.rand.NextBool(33) ? 1 : 0;
            int ext3 = Main.rand.NextBool(33) && (Main.expertMode || Main.masterMode) ? 1 : 0; 
            int amount = 1 + ext1 + ext2 + ext3;

            for(int i = 0; i < amount; i++)
            {
                Vector2 vel = new(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10));
                Projectile.NewProjectileDirect(Player.GetSource_Accessory(item), Player.Center, vel, Player.beeType(), Player.beeDamage(13), Player.beeKB(0), Player.whoAmI);
            }
        }

        public override void ResetEffects()
        {
            Fangs = false;
            item = null;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Fangs)
            {
                makeProj();
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Fangs)
            {
                if (proj.DamageType == DamageClass.Melee && proj.type != ProjectileID.Bee && proj.type != ProjectileID.GiantBee)
                {
                    makeProj();
                }
            }
        }
    }
}
