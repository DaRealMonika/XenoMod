using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class ReinforcedShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 2);
            Item.defense = 1;
            Item.DefaultToAccessory(30, 28);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup("Wood", 20)
                .AddRecipeGroup("IronBar", 30)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
        }
    }
}
