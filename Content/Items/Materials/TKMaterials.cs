using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace XenoMod.Content.Items.Materials
{
	[Autoload(false)]
	public class Test : ModTKMaterial
    {
        public override void SetDefaults()
        {
            rarity = ItemRarityID.Master;
            color = Main.DiscoColor;
            itemId = ItemID.AngelStatue;
            stationId = TileID.WorkBenches;
            baseStr = 9000;
            bladeStr = 50;
            rodStr = -50;
            bindingStr = 50;
            coverStr = 50.5f;
            barrelStr = -50.3786f;
            modifierSlots = 50;
        }
	}
	[Autoload(false)]
	public class Wood : ModTKMaterial
    {
        public override void SetDefaults()
        {
            color = Color.Chocolate;
            itemId = ItemID.Wood;
            stationId = TileID.WorkBenches;
            rodStr = 5;
            bindingStr = 0.5f;
            bladeStr = 2;
            gripStr = -2;
            barrelStr = -15;
            coverStr = -5;
            stoneStr = 1;
            hookStr = 1;
            modifierSlots = 1;
        }
	}
	[Autoload(false)]
	public class Ebonwood : Wood 
    {
        public override void SetDefaults() 
        {
            base.SetDefaults();
            color = Color.MediumPurple;
            itemId = ItemID.Ebonwood;
            rodStr += 0.2f;
            bladeStr += 0.3f;
        }
	}
	[Autoload(false)]
	public class Mahogany : Wood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = Color.PaleVioletRed;
            itemId = ItemID.RichMahogany;
            rodStr += 0.1f;
            bladeStr -= 0.2f;
            coverStr += 3;
            stoneStr += 2.5f;
            modifierSlots += 0.3f;
        }
	}
	[Autoload(false)]
	public class Pearlwood : Wood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = Color.Tan;
            itemId = ItemID.Pearlwood;
            bladeStr -= 0.2f;
            coverStr += 4.2f;
            stoneStr += 3.8f;
            modifierSlots += 0.6f;
        }
	}
	[Autoload(false)]
	public class Shadewood : Ebonwood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = Color.CadetBlue;
            itemId = ItemID.Shadewood;
            rodStr -= 0.1f;
            bladeStr += 0.1f;
        }
	}
	[Autoload(false)]
	public class SpookyWood : Wood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = Color.Purple;
            itemId = ItemID.SpookyWood;
            bladeStr -= (bladeStr - 0.3f);
            coverStr -= 3;
            stoneStr -= 3;
            hookStr += 4.3f;
        }
	}
	[Autoload(false)]
	public class DynastyWood : Wood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = Color.SaddleBrown;
            itemId = ItemID.DynastyWood;
        }
	}
	[Autoload(false)]
	public class BorealWood : Wood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(99, 75, 58);
            itemId = ItemID.BorealWood;
        }
	}
	[Autoload(false)]
	public class PalmWood : Wood
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(160, 117, 64);
            itemId = ItemID.PalmWood;
        }
	}
	[Autoload(false)]
	public class Copper : ModTKMaterial
    {
        public override void SetDefaults()
        {
            color = new(198, 116, 75);
            itemId = ItemID.CopperBar;
            baseStr = 1;
            rodStr = 3;
        }
	}
	[Autoload(false)]
	public class Tin : Copper
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(163, 149, 119);
            itemId = ItemID.TinBar;
        }
	}
	[Autoload(false)]
	public class Iron : Copper
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(163, 143, 119);
            itemId = ItemID.IronBar;
            baseStr += 1;
            rodStr += 2.3f;
        }
	}
	[Autoload(false)]
	public class Lead : Iron
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = Color.DarkSlateGray;
            itemId = ItemID.LeadBar;
        }
	}
	[Autoload(false)]
	public class Silver : Iron
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(211, 211, 225);
            itemId = ItemID.SilverBar;
            baseStr += 1;
            rodStr += 2.3f;
        }
	}
	[Autoload(false)]
	public class Tungsten : Silver
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(145, 163, 141);
            itemId = ItemID.TungstenBar;
        }
	}
	[Autoload(false)]
	public class Gold : Silver
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(255, 210, 66);
            itemId = ItemID.GoldBar;
            baseStr += 1;
            rodStr += 2.3f;
        }
	}
	[Autoload(false)]
	public class Platinum : Gold
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(183, 183, 229);
            itemId = ItemID.PlatinumBar;
        }
	}
	[Autoload(false)]
	public class Demonite : Gold
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            rarity = ItemRarityID.Blue;
            color = new(117, 65, 193);
            itemId = ItemID.DemoniteBar;
            baseStr += 1;
            rodStr += 2.3f;
        }
    }
	[Autoload(false)]
    public class Crimtane : Demonite
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            color = new(142, 28, 28);
            itemId = ItemID.CrimtaneBar;
        }
    }
    public class Hellstone : Demonite
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            rarity = ItemRarityID.Orange;
            color = new(255, 132, 0);
            itemId = ItemID.HellstoneBar;
            baseStr += 1;
            rodStr += 2.3f;
        }
    }
}