using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MushroomTop : ModItem
    {
        private static int BodyPiece;
        private static int LegPiece;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.defense = 1;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.White;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            BodyPiece = body != null ? body.type : 0;
            LegPiece = legs != null ? legs.type : 0;
            bool Body = BodyPiece == ModContent.ItemType<MushroomGuard>() || BodyPiece == ModContent.ItemType<GlowingMushroomGuard>();
            bool Legs = LegPiece == ModContent.ItemType<MushroomGreaves>() || LegPiece == ModContent.ItemType<GlowingMushroomGreaves>();
            return Body && Legs;
        }

        public override void UpdateArmorSet(Player player)
        {
            int Glow = 1;
            string[] Set = { "FullMushroomArmor", "2MushroomArmor", "1MushroomArmor" };
            if (BodyPiece == ModContent.ItemType<GlowingMushroomGuard>()) Glow++;
            if (LegPiece == ModContent.ItemType<GlowingMushroomGreaves>()) Glow++;

            player.restorationDelayTime = (int)Math.Round(player.restorationDelayTime * 0.15 / Glow);
            player.mushroomDelayTime = (int)Math.Round(player.mushroomDelayTime * 0.15 / Glow);
            player.potionDelayTime = (int)Math.Round(player.potionDelayTime * 0.15 / Glow);
            if (Glow > 1)
            {
                player.manaSickReduction = player.manaSickReduction * 0.05f * (Glow - 1);
                player.GetModPlayer<GlowingMushroomArmorPlayer>().ManaReduction = 0.03f * (Glow - 1);
            }

            player.statDefense += 1;
            player.setBonus = Language.GetTextValue(XenoMod.ArmorBonusLocal + Set[Glow - 1]);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Mushroom, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}