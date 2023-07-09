using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace XenoMod.Content.Items.Consumables
{
    public class PlanteraSpawn : ModItem
    {
        public override string Texture => "ModLoader/UnloadedItem";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

            NPCID.Sets.MPAllowedEnemies[NPCID.Plantera] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 19;
            Item.maxStack = 20;
            Item.value = Item.buyPrice(gold: 25);
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneJungle && player.ZoneDirtLayerHeight && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.AnyNPCs(NPCID.Plantera);
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = NPCID.Plantera;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.MudBlock, 20)
                .AddIngredient(ItemID.ChlorophyteBar, 10)
                .AddIngredient(ItemID.Vine, 15)
                .AddIngredient(ItemID.JungleGrassSeeds, 10)
                .AddIngredient(ItemID.JungleSpores, 15)
                .AddIngredient(ItemID.Stinger, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}