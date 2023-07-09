using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExampleMod.Content
{
    public class RecipeGroups : ModSystem
    {
        public static RecipeGroup Grenades;
        public override void Unload()
        {
            Grenades = null;
        }
        public override void AddRecipeGroups()
        {
            Grenades = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Grenade)}",
            ItemID.Grenade, ItemID.StickyGrenade, ItemID.BouncyGrenade, ItemID.PartyGirlGrenade, ItemID.Beenade);
            RecipeGroup.RegisterGroup(nameof(ItemID.Grenade), Grenades);
        }
    }
}