using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace XenoMod.Content.Items.Materials
{
    public class PureLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 24));
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            ItemID.Sets.ItemNoGravity[Type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 19;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ItemRarityID.Red;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(7)
                .AddIngredient(ItemID.FragmentSolar, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddIngredient(ItemID.FragmentNebula, 5)
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ItemID.CrystalShard, 20)
                .AddTile(TileID.LunarCraftingStation)
                .AddTile(TileID.EmpressButterflyJar)
                .Register();
        }
    }
}