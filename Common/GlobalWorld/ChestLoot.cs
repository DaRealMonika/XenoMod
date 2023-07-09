using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XenoMod.Common.GlobalWorld
{
    public class ChestLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            int[] itemsToPlaceInGoldChest = { ModContent.ItemType<Content.Items.Accessories.CounterStrikePendant>() };
            int[] itemsToReplaceInGoldChest =
            {
                ItemID.HermesBoots,
                ItemID.CloudinaBottle,
                ItemID.BandofRegeneration,
                ItemID.MagicMirror,
                ItemID.Mace,
                ItemID.ShoeSpikes,
                ItemID.FlareGun
            };

            for(int chestIndex = 0; chestIndex < Main.chest.Length; chestIndex++) {
                Chest chest = Main.chest[chestIndex];

                if (chest != null && ((Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 11 * 36) || (Main.tile[chest.x, chest.y].TileType == TileID.Containers2 && Main.tile[chest.x, chest.y].TileFrameX == 4 * 36)))
                {
                    if (itemsToReplaceInGoldChest.Contains(chest.item[0].type) && Main.rand.NextBool(9))
                    {
                        if (chest.item[0].type == ItemID.FlareGun)
                        {
                            Item lastItem = chest.item.Last(l => !l.IsAir);
                            chest.item[1].TurnToAir();
                            (chest.item[1], lastItem) = (lastItem, chest.item[1]);
                        }
                        chest.item[0].SetDefaults(Main.rand.Next(itemsToPlaceInGoldChest));
                    }
                }
            }
        }
    }
}
