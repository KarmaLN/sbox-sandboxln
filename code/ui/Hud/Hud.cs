using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public partial class HudBase : Panel
{
	public Panel HudBG;
	public HudBase()
    {
		HudBG = Add.Panel("HudBaseBG");
	}

}


public partial class Vitals : Panel
{
	public Label Health;
	public Label HealthImg;

	public Vitals()
	{
		HealthImg = Add.Label("", "healthImg");
		Health = HealthImg.Add.Label( "100", "healthText" );
	}

	public override void Tick()
	{

		base.Tick();

		var player = Local.Pawn;
		if ( player == null ) return;

		Health.Text = $"{player.Health.CeilToInt()}";
	}
}

public partial class Armour : Panel
{
	public Label Armor;
	public Label ArmorImg;

	public Armour()
	{
		ArmorImg = Add.Label("", "armorImg");
		Armor = ArmorImg.Add.Label("100", "armorText");
	}

	public override void Tick()
	{

		base.Tick();

		var player = Local.Pawn;
		if (player == null) return;

		Armor.Text = $"100";
	}
}

public partial class Ammo : Panel
{

}


public class Crosshair : Panel
{

	public static Crosshair Current;

	public Crosshair()
	{
		Current = this;
		StyleSheet.Load("/UI/SandboxHud.scss");

		Panel CrosshairExist = Add.Panel("element");
	}

}
