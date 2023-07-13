using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Materials
{
	[Autoload(false)]
	public class WeaponRod : ModTKPart
	{
		public override float MaterialAmount => 3;
		public override void SetDefaults() {
			base.SetDefaults();
			Item.width = 32;
			Item.height = 32;
		}
	}
	[Autoload(false)]
	public class WeaponBinding : ModTKPart
	{
		public override float MaterialAmount => 2;
	}
	[Autoload(false)]
	public class WeaponChain : ModTKPart
	{
        public override float MaterialAmount => 2;
    }
	[Autoload(false)]
	public class WeaponBlade : ModTKPart
	{
		public override float MaterialAmount => 6;
	}
	[Autoload(false)]
	public class WeaponTip : ModTKPart
	{
		public override float MaterialAmount => 1;
	}
	[Autoload(false)]
	public class WeaponGrip : ModTKPart
	{
		public override float MaterialAmount => 4;
	}
	[Autoload(false)]
	public class WeaponBarrel : ModTKPart
	{
		public override float MaterialAmount => 8;
	}
	[Autoload(false)]
	public class WeaponCover : ModTKPart
	{
		public override float MaterialAmount => 5;
	}
	[Autoload(false)]
	public class WeaponStone : ModTKPart
	{
		public override float MaterialAmount => 2;
	}
	[Autoload(false)]
	public class WeaponHook : ModTKPart
	{
		public override float MaterialAmount => 3;
	}
	public abstract class ModTKPart : ModItem
    {
		public override bool IsLoadingEnabled(Mod mod) => mod is XenoMod;
		public ModTKMaterial MatType { get; protected internal set; }
		public virtual string BaseName => GetType().Name;

		public override string Name => MatType + BaseName;

		public virtual float MaterialAmount => 3;
		public virtual string MaterialMultType => BaseName;

		protected bool HasTexture;
		//could alternatively just return the unloaded item path and require the fallback texture path to be manually overridden
		public virtual string FallbackTexture {
			get {
				string normalFallback = (GetType().Namespace + ".Base" + GetType().Name).Replace('.', '/');
				return ModContent.HasAsset(normalFallback) ? normalFallback : "ModLoader/UnloadedItem";
			}
		}
		public override string Texture => (HasTexture = ModContent.HasAsset(base.Texture)) ? base.Texture : FallbackTexture;
        protected override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault($"{{${MatType.DisplayName.Key}}}{{$Mods.{Mod.Name}.ItemName.{BaseName}}}");
            Tooltip.SetDefault($"{{$Mods.{Mod.Name}.ItemTooltip.{BaseName}}}");
        }

        public override void SetDefaults()
        {
			Item.maxStack = 999;
            Item.rare = MatType.overrideRarity ? MatType.rarity : new Item(MatType.itemId).rare;
            if (!HasTexture) Item.color = MatType.color;
            Item.width = 20;
            Item.height = 20;
        }

        public override void AddRecipes()
        {
            if (MatType.stationId >= 0) {
				float baseAmount = MatType.amountOverride.TryGetValue(MaterialMultType, out float amount) ? amount : MaterialAmount;
				CreateRecipe()
                .AddIngredient(MatType.itemId, (int)(baseAmount * MatType.amountMult))
                .AddTile(MatType.stationId)
                .Register();
            }
        }

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (!HasTexture) tooltips.Add(new(Mod, "GeneratedSprite", Language.GetTextValue(XenoMod.CommonLocal + "GeneratedSprite")));
		}
	}
}