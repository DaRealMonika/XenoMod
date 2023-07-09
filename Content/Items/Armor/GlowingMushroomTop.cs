using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GlowingMushroomTop : ModItem
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
            bool Body = BodyPiece == ModContent.ItemType<GlowingMushroomGuard>() || BodyPiece == ModContent.ItemType<MushroomGuard>();
            bool Legs = LegPiece == ModContent.ItemType<GlowingMushroomGreaves>() || LegPiece == ModContent.ItemType<MushroomGreaves>();
            return Body && Legs;
        }

        public override void UpdateArmorSet(Player player)
        {
            int Norm = 1;
            string[] Set = { "FullGlowingMushroomArmor", "2GlowingMushroomArmor", "1GlowingMushroomArmor" };
            if (BodyPiece == ModContent.ItemType<MushroomGuard>()) Norm++;
            if (LegPiece == ModContent.ItemType<MushroomGreaves>()) Norm++;

            player.manaSickReduction = player.manaSickReduction * 0.15f / Norm;
            player.GetModPlayer<GlowingMushroomArmorPlayer>().ManaReduction = 0.09f / Norm;
            if (Norm > 1)
            {
                player.restorationDelayTime = (int)Math.Round(player.restorationDelayTime * 0.05 * (Norm - 1));
                player.mushroomDelayTime = (int)Math.Round(player.mushroomDelayTime * 0.05 * (Norm - 1));
                player.potionDelayTime = (int)Math.Round(player.potionDelayTime * 0.05 * (Norm - 1));
            }

            player.statDefense += 1;
            player.setBonus = Language.GetTextValue(XenoMod.ArmorBonusLocal + Set[Norm - 1]);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class GlowingMushroomArmorPlayer : ModPlayer
    {
        public float ManaReduction;

        public override void ResetEffects()
        {
            ManaReduction = 0;
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            mult -= ManaReduction;
            //Main.NewText(1 - ManaReduction, Main.DiscoColor);
        }
    }
}