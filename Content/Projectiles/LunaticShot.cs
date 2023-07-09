using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Projectiles
{
    public class LunaticShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.NebulaSphere);

            AIType = ProjectileID.NebulaSphere;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.timeLeft > 30 && Projectile.alpha > 0)
                Projectile.alpha -= 25;

            if (Projectile.timeLeft > 30 && Projectile.alpha < 128 && Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                Projectile.alpha = 128;

            if (Projectile.alpha < 0)
                Projectile.alpha = 0;

            if (++Projectile.frameCounter > 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                    Projectile.frame = 0;
            }

            Lighting.AddLight(Projectile.Center, 0.5f, 0.1f, 0.3f);
            return true;
        }
    }
}
