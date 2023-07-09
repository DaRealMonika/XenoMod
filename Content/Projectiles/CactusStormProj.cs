using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Projectiles
{
    public class CactusStormProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RollingCactusSpike);
            
            AIType = ProjectileID.RollingCactusSpike;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }
    }
}
