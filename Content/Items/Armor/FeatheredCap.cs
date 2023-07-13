using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FeatheredCap : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
            Item.width = 18;
            Item.height = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.jumpSpeedBoost /= 0.05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FeatheredChestplate>() && legs.type == ModContent.ItemType<FeatheredGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue(XenoMod.ArmorBonusLocal + "FeatheredArmor");
            player.GetDamage(DamageClass.Summon) *= 1.1f;
            player.jumpSpeedBoost *= 1.18f;
            player.moveSpeed *= 1.15f;
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Feather, 10)
                .AddIngredient(ItemID.Cobweb, 5)
                .AddRecipeGroup("Wood", 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}