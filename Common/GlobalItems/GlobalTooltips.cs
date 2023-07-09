using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using XenoMod.Content.Items.Materials;

namespace XenoMod.Common.GlobalItems
{
    public class GlobalTooltips : GlobalItem
    {
        private bool Materials(Item entity, out ModTKMaterial mat)
        {
            for (int i = 0; i < TKMaterialLoader.MaterialCount; i++)
            {
                ModTKMaterial material = TKMaterialLoader.GetTKMaterial(i);
                mat = material;
                if (material.itemId == entity.type) return true;
            }
            mat = null;
            return false;
        }
        private bool Parts(Item entity, out ModTKMaterial mat, out string kind)
        {
            if (entity.ModItem is ModTKPart part)
            {
                mat = part.MatType;
                kind = part.BaseName;
                return true;
            }
            mat = null;
            kind = null;
            return false;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            bool debug;
#if DEBUG
            debug = true;
#endif
            return entity.type == ItemID.EmpressButterflyJar || Materials(entity, out ModTKMaterial _) || Parts(entity, out ModTKMaterial _, out string _) || debug; // you don't need to define a type for discards
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            void MatTips(ModTKMaterial mat, string kind = "All")
            {
                string tip = Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.BaseExpand");
                string tipExpand = Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Base");

                float rod = mat.rodStr != 0 ? mat.rodStr : mat.baseStr;
                if (rod != 0 && (kind == "All" || kind == "Rod")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Rod", rod > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), rod);

                float binding = mat.bindingStr != 0 ? mat.bindingStr : mat.baseStr;
                if (binding != 0 && (kind == "All" || kind == "Binding")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Binding", binding > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), binding);

                float blade = mat.bladeStr != 0 ? mat.bladeStr : mat.baseStr;
                if (blade != 0 && (kind == "All" || kind == "Blade")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Blade", blade > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), blade);

                float spearTip = mat.tipStr != 0 ? mat.tipStr : mat.baseStr;
                if (spearTip != 0 && (kind == "All" || kind == "Tip")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Tip", spearTip > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), spearTip);

                float grip = mat.gripStr != 0 ? mat.gripStr : mat.baseStr;
                if (grip != 0 && (kind == "All" || kind == "Grip")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Grip", grip > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), grip);

                float barrel = mat.barrelStr != 0 ? mat.barrelStr : mat.baseStr;
                if (barrel != 0 && (kind == "All" || kind == "Barrel")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Barrel", barrel > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), barrel);

                float cover = mat.coverStr != 0 ? mat.coverStr : mat.baseStr;
                if (cover != 0 && (kind == "All" || kind == "Cover")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Cover", cover > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), cover);

                float stone = mat.stoneStr != 0 ? mat.stoneStr : mat.baseStr;
                if (stone != 0 && (kind == "All" || kind == "Stone")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Stone", stone > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), stone);

                float hook = mat.hookStr != 0 ? mat.hookStr : mat.baseStr;
                if (hook != 0 && (kind == "All" || kind == "Hook")) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Hook", hook > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), hook);

                float modifiers = mat.modifierSlots;
                if (modifiers != 0) tipExpand += "\n" + Language.GetTextValue(XenoMod.ModLocal + "MaterialTooltip.Modifiers", modifiers > 0 ? Color.ForestGreen.Hex3() : Color.DarkRed.Hex3(), modifiers);

                if (XenoMod.FindTooltipIndex(tooltips, "Tooltip0", "Terraria", out int index))
                {
                    tooltips.Insert(index + 1, new(Mod, "WeaponModifiers", ItemSlot.ShiftInUse ? tipExpand : tip));
                }
                else if (XenoMod.FindTooltipIndex(tooltips, "ItemName", "Terraria", out int index1))
                {
                    tooltips.Insert(index1 + 1, new(Mod, "WeaponModifiers", ItemSlot.ShiftInUse ? tipExpand : tip));
                }
            }

            if (item.type == ItemID.EmpressButterflyJar)
            {
                if (XenoMod.FindTooltipIndex(tooltips, "Placeable", "Terraria", out int index))
                {
                    tooltips.Insert(index + 1, new(Mod, "SpecialCraft", Language.GetTextValue(XenoMod.ItemTipLocal + "EmpressButterflyJar")));
                }
            }

            if (Materials(item, out ModTKMaterial mat)) MatTips(mat);

            if (Parts(item, out ModTKMaterial parts, out string kind)) MatTips(parts, kind);
#if DEBUG
            /*string txt = string.Empty;
            for (int i = 0; i < TextureAssets.Item.Length; i++)
            {
                if (TextureAssets.Item[i].Name == "UnloadedItem")
                {
                    if (txt == null) txt = i.ToString();
                    else txt += "\n" + i;
                    //break;
                }
            }*/
            tooltips.Add(new(Mod, "Debug", item.ModItem?.Texture ?? ""));
#endif
        }
    }
}
