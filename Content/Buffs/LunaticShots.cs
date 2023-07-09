using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Buffs
{
    public class LunaticShots : ModBuff
    {
        public int counter;
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LunaticShotsPlayer>().Lunatics = true;
            player.GetModPlayer<LunaticShotsPlayer>().index = buffIndex;
            if (player.GetModPlayer<LunaticShotsPlayer>().cooldown > 0) {
                player.GetModPlayer<LunaticShotsPlayer>().cooldown--;
            }
            player.buffTime[buffIndex] = 10;
            if (player.GetModPlayer<LunaticShotsPlayer>().Counter <= 0) player.ClearBuff(Type);
        }
    }

    public class LunaticShotsPlayer : ModPlayer
    {
        public bool Lunatics;
        public int Counter;
        public int index;
        public int cooldown;

        public override void ResetEffects()
        {
            Lunatics = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Lunatics && Counter > 0 && cooldown <= 0)
            {
                if (XenoMod.isSummon(proj) && XenoMod.isHostileNpc(target) && proj.type != ProjectileID.CultistBossIceMist && proj.type != ProjectileID.CultistBossLightningOrb && proj.type != ProjectileID.CultistBossLightningOrbArc && proj.type != ProjectileID.CultistBossFireBall && proj.type != ProjectileID.CultistBossFireBallClone)
                {
                    Counter--;
                    Vector2 vel = Player.DirectionTo(Main.MouseWorld) * 10;

                    int id = 0;
                    while(id == 0)
                    { // have yet to make friendly version of these
                        if (Main.rand.NextBool(4)) id = ProjectileID.CultistBossIceMist;
                        else if (Main.rand.NextBool(4)) id = ProjectileID.CultistBossFireBall;
                        else if (Main.rand.NextBool(4)) id = ProjectileID.CultistBossFireBallClone;
                    }

                    Projectile newProj = Projectile.NewProjectileDirect(Player.GetSource_Buff(index), Player.Center, vel, id, damage, knockback, Player.whoAmI);
                    newProj.hostile = false;
                    newProj.friendly = true;
                    newProj.penetrate = -1;
                    newProj.ignoreWater = true;
                    newProj.timeLeft = 1200;
                    newProj.DamageType = DamageClass.Summon;
                    cooldown = 50;
                }
            }
        }
    }
}
