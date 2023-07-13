using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FeatheredChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
            Item.width = 18;
            Item.height = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.aggro = (int)(player.aggro * 0.1);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.Feather, 20)
                .AddIngredient(ItemID.Cobweb, 10)
                .AddRecipeGroup("Wood", 30)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}