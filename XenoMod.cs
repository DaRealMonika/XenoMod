using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Items.Materials;

namespace XenoMod
{
	public class XenoMod : Mod
	{
		public const string ModLocal = "Mods.XenoMod.";
		public const string ItemNameLocal = ModLocal + "ItemName.";
		public const string ItemTipLocal = ModLocal + "ItemTooltip.";
		public const string BuffNameLocal = ModLocal + "BuffName.";
		public const string BuffDescLocal = ModLocal + "BuffDescription.";
		public const string DeathLocal = ModLocal + "DeathText.";
		public const string ArmorBonusLocal = ModLocal + "ArmorSetBonus.";

        public static bool FindTooltipIndex(List<TooltipLine> tooltips, string name, string mod, out int index)
        {
            TooltipLine tooltipLine = tooltips.FirstOrDefault(x => x.Name == name && x.Mod == mod);
            if (tooltipLine != null)
            {
                index = tooltips.IndexOf(tooltipLine);
                return true;
            }
            index = 0;
#if DEBUG
            ModContent.GetInstance<XenoMod>().Logger.WarnFormat("Tooltip line {0} from mod {1} not found!", name, mod);
#endif
            return false;
        }

        public static bool isMinion(Projectile proj)
		{
			return proj.minion || ProjectileID.Sets.MinionShot[proj.type];
		}

		public static bool isSentry(Projectile proj)
		{
			return proj.sentry || ProjectileID.Sets.SentryShot[proj.type];
		}

        public static bool isSummon(Projectile proj)
		{
			return isMinion(proj) || isSentry(proj);
		}

		public static bool isHostileNpc(NPC npc)
		{
			return !npc.friendly && npc.lifeMax > 5 && npc.chaseable && !npc.dontTakeDamage && !npc.immortal;
		}

        public override void Unload()
        {
            TKMaterialLoader.Unload();
        }

        public override void Load()
        {
            for (int i = 0; i < TKMaterialLoader.MaterialCount; i++)
            {
                ModTKMaterial material = TKMaterialLoader.GetTKMaterial(i);
                material.SetDefaults();
                AddContent(new WeaponParts(material, "Rod"));
                AddContent(new WeaponParts(material, "Binding"));
                AddContent(new WeaponParts(material, "Blade"));
                AddContent(new WeaponParts(material, "Tip"));
                AddContent(new WeaponParts(material, "Grip"));
                AddContent(new WeaponParts(material, "Barrel"));
                AddContent(new WeaponParts(material, "Cover"));
                AddContent(new WeaponParts(material, "Stone"));
                AddContent(new WeaponParts(material, "Hook"));
            }
        }
    }

    public static class TKMaterialLoader
    {
        internal static readonly IList<ModTKMaterial> materials = new List<ModTKMaterial>();
        private static int nextMaterial = 0;
        public static int MaterialCount => nextMaterial;
        internal static int ReserveTKMaterialID()
        {
            return nextMaterial++;
        }
        public static ModTKMaterial GetTKMaterial(int type)
        {
            return type >= MaterialCount ? null : materials[type];
        }
        internal static void Unload()
        {
            materials.Clear();
            nextMaterial = 0;
        }
    }
    public abstract class ModTKMaterial : ModType
    {
        public int Type { get; internal set; }

        public ModTranslation DisplayName { get; internal set; }

        public int stack = 999;
        public int rarity = ItemRarityID.White;
        public int itemId;
        public Dictionary<string, int> amount = new(){ ["Rod"] = 3, ["Binding"] = 2, ["Blade"] = 6, ["Tip"] = 1, ["Grip"] = 4, ["Barrel"] = 8, ["Cover"] = 5, ["Stone"] = 2, ["Hook"] = 3 };
        public string texture = "XenoMod/Content/Items/Materials/";
        public Color color = Color.White;
        public int stationId = -1;
        public float baseStr = 0;
        public float rodStr = 0;
        public float bindingStr = 0;
        public float bladeStr = 0;
        public float tipStr = 0;
        public float gripStr = 0;
        public float barrelStr = 0;
        public float coverStr = 0;
        public float stoneStr = 0;
        public float hookStr = 0;
        public float modifierSlots = 0;
        public virtual void SetDefaults(){}
        protected sealed override void Register()
        {
            ModTypeLookup<ModTKMaterial>.Register(this);
            Type = TKMaterialLoader.ReserveTKMaterialID();
            DisplayName = LocalizationLoader.GetOrCreateTranslation(Mod, "MaterialName." + Name);
            TKMaterialLoader.materials.Add(this);
        }
    }
}