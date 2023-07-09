using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Humanizer;

namespace XenoMod.Content.Items.Materials
{
    [Autoload(false)]
    public class WeaponHandleOld : ModItem
    {

        public override string Texture => "ModLoader/UnloadedItem"; // no sprite yet

        public int MatType;
        public static string[] MatName = { "Wood", "Copper", "Tin", "Iron", "Lead", "Silver", "Tungsten", "Gold", "Platinum", "Hellstone" };
        public static float[] MatStrength = { 1, 1.5f, 1.5f, 2, 2, 3, 3, 4, 4, 5 };

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(string.Empty);
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.rare = ItemRarityID.White;
        }

        public override bool CanStack(Item item2)
        {
            return (Item.ModItem as WeaponHandleOld).MatType == (item2.ModItem as WeaponHandleOld).MatType;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (MatType > MatName.Length-1) MatType = 0;
            string mat = Language.GetTextValue(XenoMod.ModLocal + "MatTiers." + MatName[MatType]);
            string color = Color.Gray.Hex3();
            if (MatStrength[MatType] > 0) color = Color.ForestGreen.Hex3();
            if (MatStrength[MatType] < 0) color = Color.DarkRed.Hex3();
            string[] desc = { color, MatStrength[MatType].ToString() };
            if (XenoMod.FindTooltipIndex(tooltips, "ItemName", "Terraria", out int index)) {
                tooltips.RemoveAt(index);
                tooltips.Insert(index, new(Mod, "ItemName", Language.GetTextValue(XenoMod.ItemNameLocal + "WeaponHandle").FormatWith(mat)));
            }
            if (XenoMod.FindTooltipIndex(tooltips, "Tooltip0", "Terraria", out int index1))
            {
                tooltips.RemoveAt(index1);
                tooltips.Insert(index1, new(Mod, "Tooltip0", Language.GetTextValue(XenoMod.ItemTipLocal + "WeaponHandle").FormatWith(desc)));
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddRecipeGroup("Wood", 3)
                .AddTile(TileID.WorkBenches);
            (recipe.createItem.ModItem as WeaponHandleOld).MatType = 0;
            recipe.Register();
            recipe = CreateRecipe() // tmp recipe
                .AddIngredient(ItemID.IronBar, 3)
                .AddTile(TileID.Anvils);
            (recipe.createItem.ModItem as WeaponHandleOld).MatType = 3;
            recipe.Register();
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Set("mat", MatType, true);
        }

        public override void LoadData(TagCompound tag)
        {
            MatType = tag.GetInt("mat");
        }
    }
}