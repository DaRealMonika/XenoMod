using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class GlowingMushroomGuard : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.GlowingMushroom, 20)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}