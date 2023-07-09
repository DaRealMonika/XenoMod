﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Content.Projectiles
{
    public class FriendlyHallowBossEverlastingRainbow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HallowBossLastingRainbow);
            
            AIType = ProjectileID.HallowBossLastingRainbow;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = Main.hslToRgb((Projectile.ai[1] + 1f) % 1f, 1f, 0.5f, byte.MaxValue) * Projectile.Opacity;
            Lighting.AddLight(Projectile.Center, color.ToVector3());

            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = TextureAssets.Projectile[Type].Height() / Main.projFrames[Type];
            int frameY = frameHeight * Projectile.frame;
            Rectangle sourceRect = new(0, frameY, texture.Width / 2, frameHeight);
            Vector2 projOrigin = sourceRect.Size() / 2f;

            int numIs0 = 0;
            int iterationAmount = -1;
            float maxScale = 0.7f;
            float scaleDiv = 20f;
            float rotationMulti = 0f;

            int trailLength = 60;
            float shineScale = 1f;

            for (int i = trailLength; (iterationAmount > 0 && i < numIs0) || (iterationAmount < 0 && i > numIs0); i += iterationAmount)
            {
                if (i >= Projectile.oldPos.Length)
                {
                    continue;
                }

                Color trailColor = color;
                trailColor *= Utils.GetLerpValue(0f, 20f, Projectile.timeLeft, clamped: true);

                float colorMulti = numIs0 - i;
                if (iterationAmount < 0)
                {
                    colorMulti = trailLength - i;
                }

                trailColor *= colorMulti / ((float)ProjectileID.Sets.TrailCacheLength[Type] * 1.5f);
                Vector2 trailOldPos = Projectile.oldPos[i];

                float trailRotation = Projectile.oldRot[i];
                SpriteEffects trailSpriteEffects = ((Projectile.oldSpriteDirection[i] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

                if (trailOldPos == Vector2.Zero)
                {
                    continue;
                }

                Vector2 trailPos = trailOldPos + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, trailPos, new Rectangle(sourceRect.X + (TextureAssets.Projectile[Type].Width() / 2), sourceRect.Y, sourceRect.Width, sourceRect.Height), trailColor, trailRotation + Projectile.rotation * rotationMulti * (i - 1) * (-spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt()), projOrigin, MathHelper.Lerp(Projectile.scale, maxScale, (float)i / scaleDiv), trailSpriteEffects, 0);
            }

            Color fairyQueenWeaponColor = Color.White;

            Vector2 shinyPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(texture, shinyPos, sourceRect, fairyQueenWeaponColor * Projectile.Opacity, Projectile.rotation, projOrigin, Projectile.scale * 0.9f, spriteEffects, 0);
            Texture2D textureExtra98 = TextureAssets.Extra[98].Value;
            Vector2 shinyOrigin = textureExtra98.Size() / 2f;
            Color colorTopBot = fairyQueenWeaponColor * 0.5f;
            Color ColorLeftRight = fairyQueenWeaponColor;
            float scaleWarping = Utils.GetLerpValue(15f, 30f, Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(Projectile.timeLeft, Projectile.timeLeft - 40f, Projectile.timeLeft, clamped: true) * (1f + 0.2f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 30f / 0.5f * ((float)Math.PI * 2f) * 3f)) * 0.8f;
            Vector2 scale1 = new Vector2(0.5f, 5f) * scaleWarping * shineScale;
            Vector2 scale2 = new Vector2(0.5f, 2f) * scaleWarping * shineScale;
            colorTopBot *= scaleWarping;
            ColorLeftRight *= scaleWarping;

            scale1 *= 0.4f;
            scale2 *= 0.4f;

            // Flashy star bits
            Main.EntitySpriteDraw(textureExtra98, shinyPos, null, ColorLeftRight, MathHelper.PiOver2, shinyOrigin, scale1, spriteEffects, 0);
            Main.EntitySpriteDraw(textureExtra98, shinyPos, null, ColorLeftRight, 0f, shinyOrigin, scale2, spriteEffects, 0);
            Main.EntitySpriteDraw(textureExtra98, shinyPos, null, colorTopBot, MathHelper.PiOver2, shinyOrigin, scale1 * 0.6f, spriteEffects, 0);
            Main.EntitySpriteDraw(textureExtra98, shinyPos, null, colorTopBot, 0f, shinyOrigin, scale2 * 0.6f, spriteEffects, 0);

            Color projColor = Projectile.GetAlpha(lightColor);
            float projScale = Projectile.scale;
            float projRotation = Projectile.rotation;

            projColor.A /= 2;

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), sourceRect, projColor, projRotation, projOrigin, projScale, spriteEffects, 0);

            /* Debug Hitbox viewer
            Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                Projectile.Hitbox, Color.Orange * 0.5f, 0, Projectile.Hitbox.Size() / 2, Projectile.scale, spriteEffects, 0);
            */

            return false;
        }
    }
}
