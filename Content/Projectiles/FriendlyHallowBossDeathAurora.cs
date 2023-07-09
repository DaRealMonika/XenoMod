using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Projectiles
{
    public class FriendlyHallowBossDeathAurora : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HallowBossDeathAurora);
            
            AIType = ProjectileID.HallowBossDeathAurora;
            Projectile.Size *= 20;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.DamageType = DamageClass.Default;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D value = TextureAssets.Projectile[Type].Value;
            Vector2 origin = value.Size() / 2f;
            float num = Main.GlobalTimeWrappedHourly % 10f / 10f;
            Vector2 vector = Projectile.Center - Main.screenPosition;
            float[] array = new float[15];
            float[] array2 = new float[15];
            float[] array3 = new float[15];
            float[] array4 = new float[15];
            float[] array5 = new float[15];
            float num2 = 0.5f;
            int num3 = 210;
            num2 = Utils.GetLerpValue(0f, 60f, Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(num3, num3 - 60, Projectile.timeLeft, clamped: true);
            float amount = Utils.GetLerpValue(0f, 60f, Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(num3, 90f, Projectile.timeLeft, clamped: true);
            amount = MathHelper.Lerp(0.2f, 0.5f, amount);
            float num4 = 800f / (float)value.Width;
            float num5 = num4 * 0.8f;
            float num6 = (num4 - num5) / 15f;
            float num7 = 30f;
            float num8 = 300f;
            Vector2 vector2 = new Vector2(3f, 6f);
            for (int i = 0; i < 15; i++)
            {
                _ = (float)(i + 1) / 50f;
                float num9 = (float)Math.Sin(num * (MathF.PI * 2f) + MathF.PI / 2f + (float)i / 2f);
                array[i] = num9 * (num8 - (float)i * 3f);
                array2[i] = (float)Math.Sin(num * (MathF.PI * 2f) * 2f + MathF.PI / 3f + (float)i) * num7;
                array2[i] -= (float)i * 3f;
                array3[i] = (float)i / 15f * 2f + num;
                array3[i] = (num9 * 0.5f + 0.5f) * 0.6f + num;
                array4[i] = (float)(1.0 - Math.Pow(1f * (float)i / 15f, 2.0));
                array5[i] = num5 + (float)(i + 1) * num6;
                array5[i] *= 0.3f;
                Color color = Main.hslToRgb(array3[i] % 1f, 1f, 0.5f) * num2 * amount;
                color.A /= 4;
                float rotation = MathF.PI / 2f + num9 * (MathF.PI / 4f) * -0.3f + MathF.PI * (float)i;
                Main.EntitySpriteDraw(value, vector + new Vector2(array[i], array2[i]), null, color, rotation, origin, new Vector2(array5[i], array5[i]) * vector2, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}
