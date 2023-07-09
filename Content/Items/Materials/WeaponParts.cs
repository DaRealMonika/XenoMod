using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Materials
{
    [Autoload(false)]
    public class WeaponParts : ModItem
    {
        public readonly ModTKMaterial MatType;
        public readonly string Kind;
        public override string Name => MatType + Kind;
        protected bool HasTexture;
        public override string Texture => (HasTexture = ModContent.HasAsset(base.Texture)) ? base.Texture : ModContent.HasAsset(MatType.texture + "Base" + Kind) ? MatType.texture + "Base" + Kind : "ModLoader/UnloadedItem";
        protected override bool CloneNewInstances => true;
        public WeaponParts() { }
        internal WeaponParts(ModTKMaterial material, string kind) : base()
        {
            MatType = material;
            Kind = kind;
        }

        public override void SetStaticDefaults()
        {
            string tip = $"{{$Mods.XenoMod.ItemTooltip.{Kind}}}";
            if (!HasTexture) tip += $"\n{{$Mods.XenoMod.Common.GeneratedSprite}}";
            DisplayName.SetDefault($"{{${MatType.DisplayName.Key}}}{{$Mods.XenoMod.ItemName.{Kind}}}");
            Tooltip.SetDefault(tip);
        }

        public override void SetDefaults()
        {
            Item.maxStack = MatType.stack;
            Item.rare = MatType.rarity;
            if (!HasTexture) Item.color = MatType.color;
            Item.width = 20;
            Item.height = 20;
        }

        public override void AddRecipes()
        {
            if (MatType.stationId >= 0) {
                CreateRecipe()
                .AddIngredient(MatType.itemId, MatType.amount[Kind])
                .AddTile(MatType.stationId)
                .Register();
            }
        }
    }
}