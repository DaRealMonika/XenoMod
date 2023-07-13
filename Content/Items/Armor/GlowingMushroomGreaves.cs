using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class GlowingMushroomGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.defense = 1;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.White;
            Item.width = 18;
            Item.height = 18;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}