using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FeatheredGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Feather, 15)
                .AddIngredient(ItemID.Cobweb, 7)
                .AddRecipeGroup("Wood", 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}