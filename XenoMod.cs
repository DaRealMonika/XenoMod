using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using XenoMod.Content.Items.Materials;

namespace XenoMod
{
	public class XenoMod : Mod
	{
		public const string ModLocal = "Mods.XenoMod.";
        public const string CommonLocal = ModLocal + "Common.";
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
			LoadTKContent();
		}
		//probably best to put this mess of code in its own method
		public void LoadTKContent()
		{
			List<Type> materialTypes = new();
			List<Type> partTypes = new();
			foreach (Mod mod in ModLoader.Mods) {
				foreach (Type type in (
					from t in AssemblyManager.GetLoadableTypes(mod.Code)
					where !t.IsAbstract && !t.ContainsGenericParameters
					where t.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.EmptyTypes) != null
					select t).OrderBy((Type type) => type.FullName, StringComparer.InvariantCulture)) {

					if (type.IsAssignableTo(typeof(ModTKMaterial))) {
						materialTypes.Add(type);
					} else if (type.IsAssignableTo(typeof(ModTKPart))) {
						partTypes.Add(type);
					}
				}
			}
			LoaderUtils.ForEachAndAggregateExceptions(
				materialTypes,
				(matType) => {
					ModTKMaterial mat = (ModTKMaterial)Activator.CreateInstance(matType, nonPublic: true);
					AddContent(mat);
					mat.SetDefaults();
					LoaderUtils.ForEachAndAggregateExceptions(
						partTypes,
						(partType) => {
							ModTKPart part = (ModTKPart)Activator.CreateInstance(partType, nonPublic: true);
							part.MatType = mat;
							AddContent(part);
						}
					);
				}
			);
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

        public int rarity = ItemRarityID.White;
        public int itemId;
        public Dictionary<string, float> amountOverride = new();
        public float amountMult = 1f;
        public string texture = "XenoMod/Content/Items/Materials/";
        public Color color = Color.White;
        public int stationId = -1;
        public float baseStr = 0;
        public float rodStr = 0;
        public float bindingStr = 0;
        public float chainStr = 0;
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