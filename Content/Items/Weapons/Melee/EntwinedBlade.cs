using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace XenoMod.Content.Items.Weapons.Melee
{
    public class EntwinedBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = (int)(23 / 1.5);
            Item.DamageType = DamageClass.Melee;
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8;
            Item.crit = 4;
            Item.value = Item.buyPrice(gold: 25);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.scale = 2.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe1 = CreateRecipe()
                .AddRecipeGroup("Wood", 5)
                .AddIngredient(ItemID.GoldBar, 12)
                .AddRecipeGroup("IronBar", 12)
                .AddTile(TileID.Anvils)
                .Register();
            Recipe recipe2 = CreateRecipe()
                .AddRecipeGroup("Wood", 5)
                .AddIngredient(ItemID.PlatinumBar, 12)
                .AddRecipeGroup("IronBar", 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}