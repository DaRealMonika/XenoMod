using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tiles = XenoMod.Content.Tiles;

namespace XenoMod.Content.Items.Materials
{
    public class FerroSteelBar : ModItem
    {
        public override string Texture => "ModLoader/UnloadedItem";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 20;
            Item.height = 20;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.FerroSteelBar>();
            Item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe()
                .AddRecipeGroup("IronBar")
                .AddIngredient(ItemID.Diamond, 3)
                .AddIngredient(ItemID.IronOre, 3)
                .AddTile(TileID.Anvils)
                .AddCondition(Recipe.Condition.NearWater)
                .Register();
            Recipe recipe2 = CreateRecipe()
                .AddRecipeGroup("IronBar")
                .AddIngredient(ItemID.Diamond, 3)
                .AddIngredient(ItemID.LeadOre, 3)
                .AddTile(TileID.Anvils)
                .AddCondition(Recipe.Condition.NearWater)
                .Register();
        }
    }
}