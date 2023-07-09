using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class ThornyShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 4);
            Item.defense = 3;
            Item.DefaultToAccessory(30, 28);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient<ReinforcedShield>()
                .AddIngredient(ItemID.Stinger, 20)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.GetModPlayer<ThornyShieldPlayer>().Thorns = true;
        }
    }

    public class ThornyShieldPlayer : ModPlayer
    {
        public bool Thorns;

        public override void ResetEffects()
        {
            Thorns = false;
        }

        public override void PostUpdateEquips()
        {
            if (Thorns)
            {
                Player.thorns += 0.5f;
            }
        }
    }
}
