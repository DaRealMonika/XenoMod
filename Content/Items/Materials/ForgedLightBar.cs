using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tiles = XenoMod.Content.Tiles;

namespace XenoMod.Content.Items.Materials
{
    public class ForgedLightBar : ModItem
    {
        public override string Texture => "ModLoader/UnloadedItem";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.ForgedLightBar>();
            Item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 3)
                .AddIngredient<PureLight>(2)
                .AddIngredient(ItemID.CrystalShard, 10)
                .AddTile(TileID.LunarCraftingStation)
                .AddTile(TileID.EmpressButterflyJar)
                .Register();
        }
    }
}