using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Projectiles
{
    public class PlayerSlash : ModProjectile
    {
        public Color color = Color.White;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZoologistStrikeGreen);
            
            AIType = ProjectileID.ZoologistStrikeGreen;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return color;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(color.PackedValue);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            color = new Color() { PackedValue = reader.ReadUInt32()};
        }
    }
}
