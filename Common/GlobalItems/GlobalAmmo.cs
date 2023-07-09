using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Common.GlobalItems
{
    public class GlobalAmmo : GlobalItem
    {
        public static int[] isGrenade = {ItemID.Grenade, ItemID.StickyGrenade, ItemID.BouncyGrenade, ItemID.Beenade, ItemID.PartyGirlGrenade};

        public bool SpiritGrenades(Item entity)
        {
            if (ModLoader.TryGetMod("SpiritMod", out Mod mod))
            {
                int[] isSpiritGrenade = { -1, -1 };
                if (mod.TryFind("GtechGrenade", out ModItem GtechGrenade)) isSpiritGrenade.SetValue(GtechGrenade.Type, 0);
                if (mod.TryFind("BismiteGrenade", out ModItem BismiteGrenade)) isSpiritGrenade.SetValue(BismiteGrenade.Type, 1);
                return isSpiritGrenade.Contains(entity.type);
            }
            return false;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return isGrenade.Contains(entity.type) || SpiritGrenades(entity);
        }

        public override void SetDefaults(Item item)
        {
            if (isGrenade.Contains(item.type) || SpiritGrenades(item))
            {
                item.ammo = ItemID.Grenade;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ammo == ItemID.Grenade && item.useStyle != ItemUseStyleID.None)
            {
                if (XenoMod.FindTooltipIndex(tooltips, "Ammo", "Terraria", out int index))
                {
                    tooltips.RemoveAt(index);
                }
            }
        }

        public override void Load()
        {
            On.Terraria.Item.CanFillEmptyAmmoSlot += Item_CanFillEmptyAmmoSlot;
        }

        private static bool Item_CanFillEmptyAmmoSlot(On.Terraria.Item.orig_CanFillEmptyAmmoSlot orig, Item self)
        {
            bool ret = orig(self);

            //This prevents the items from automatically going into the ammo slot on pickup, but they can still be placed there manually, same way as gel and fallen star
            if (isGrenade.Contains(self.type) || (self.ammo == ItemID.Grenade && self.useStyle != ItemUseStyleID.None))
            {
                return false;
            }

            return ret;
        }
    }
}
