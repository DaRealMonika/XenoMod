using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using XenoMod.Content.Items.Materials;

namespace XenoMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class BlazingShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 24);
            Item.defense = 3;
            Item.SetWeaponValues(25, 10);
            Item.DamageType = DamageClass.Melee;
            Item.DefaultToAccessory();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient<BlazingCore>()
                .AddIngredient<ThornyShield>()
                .AddIngredient(ItemID.HellstoneBar, 20)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.GetModPlayer<ThornyShieldPlayer>().Thorns = true;
            player.GetModPlayer<BlazingShieldPlayer>().blazingShield = true;
            player.GetModPlayer<BlazingShieldPlayer>().item = Item;
        }
    }

    public class BlazingShieldPlayer : ModPlayer
    {
        // These indicate what direction is what in the timer arrays used
        public const int DashRight = 2;
        public const int DashLeft = 3;

        public const int DashCooldown = 50; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
        public const int DashDuration = 35; // Duration of the dash afterimage effect in frames

        // The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
        public const float DashVelocity = 15f;

        // The direction the player has double tapped.  Defaults to -1 for no dash double tap
        public int DashDir = -1;

        // The fields related to the dash accessory
        public bool blazingShield;
        public Item item;
        public int DashDelay = 0; // frames remaining till we can dash again
        public int DashTimer = 0; // frames remaining in the dash

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            blazingShield = false;
            item = null;

            // ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
            // When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
            // If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
            {
                DashDir = DashRight;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
            {
                DashDir = DashLeft;
            }
            else
            {
                DashDir = -1;
            }
        }

        public override void PostUpdateEquips()
        {
            if (Player.dashType < 3)
            {
                Player.dashType = 0;
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {/*
            Vector2[] left = new Vector2[6];
            Vector2[] right = new Vector2[6];

            for (int i = 0; i < 6; i++)
            {
                left[i] = new Vector2(Player.Center.X - ((i + (i > 0 ? 1 : 0)) * 30), Player.Center.Y);
            }

            for (int i = 0; i < 6; i++)
            {
                right[i] = new Vector2(Player.Center.X + ((i + (i > 0 ? 1 : 0)) * 30), Player.Center.Y);
            }

            void makeProj(Vector2[] direction, int i, int t)
            {
                int id = ProjectileID.GreekFire3;
                if (Main.rand.NextBool(33)) id = ProjectileID.GreekFire2;
                if (Main.rand.NextBool(33) && id == ProjectileID.GreekFire3) id = ProjectileID.GreekFire1;
                left[i].Y -= t;
                float vel = i == 0 ? -5.5f : -8.0f;
                double dmg = item.damage + damage;
                float kb = item.knockBack / 3;
                Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), direction[i], new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, vel)), id, (int)dmg, kb, Player.whoAmI);
                proj.friendly = true;
                proj.hostile = false;
            }

            for (int i = 0; i < 6; i++)
            {
                Tile tileLeft = Main.tile[(int)left[i].X, (int)left[i].Y];
                Tile tileRight = Main.tile[(int)right[i].X, (int)right[i].Y];

                for (int t = 0; t < 4; t++)
                {
                    if (!tileLeft.HasTile || (tileLeft.HasTile && (!WorldGen.SolidTile(tileLeft) || tileLeft.IsActuated)))
                    {
                        makeProj(left, i, t);
                    }
                    else tileLeft = Main.tile[(int)left[i].X, (int)left[i].Y - t];

                    if (!tileRight.HasTile || (tileRight.HasTile && (!WorldGen.SolidTile(tileRight) || tileRight.IsActuated)))
                    {
                        makeProj(right, i, t);
                    }
                    else tileRight = Main.tile[(int)right[i].X, (int)right[i].Y - t];
                }
            }*/
        }

        // This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
        // If they double tapped this frame, they'll move fast this frame
        public override void PreUpdateMovement()
        {
            // if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
            if (CanUseDash() && DashDir != -1 && DashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (DashDir)
                {
                    case DashLeft when Player.velocity.X > -DashVelocity:
                    case DashRight when Player.velocity.X < DashVelocity:
                        {
                            // X-velocity is set here
                            float dashDirection = DashDir == DashRight ? 1 : -1;
                            newVelocity.X = dashDirection * DashVelocity;
                            break;
                        }
                    default:
                        return; // not moving fast enough, so don't start our dash
                }

                // start our dash
                DashDelay = DashCooldown;
                DashTimer = DashDuration;
                Player.velocity = newVelocity;

                // Here you'd be able to set an effect that happens when the dash first activates
                // Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
            }

            if (DashDelay > 0)
                DashDelay--;

            if (DashTimer > 0)
            { // dash is active
              // This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
              // Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
              // Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
                Player.eocDash = DashTimer;
                Player.armorEffectDrawShadowEOCShield = true;

                if (DashTimer % 2 == 0)
                {
                    int id = ProjectileID.GreekFire3;
                    if (Main.rand.NextBool(33)) id = ProjectileID.GreekFire2;
                    if (Main.rand.NextBool(33) && id == ProjectileID.GreekFire3) id = ProjectileID.GreekFire1;
                    Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), Player.Center, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, -5)), id, item.damage, item.knockBack, Player.whoAmI);
                    proj.friendly = true;
                    proj.hostile = false;
                }

                // count down frames remaining
                DashTimer--;
            }
        }

        private bool CanUseDash()
        {
            return blazingShield
                && Player.dashType == 0 // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
                && !Player.setSolar // player isn't wearing solar armor
                && !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
        }
    }
}
