using UnityEngine;
using System.Collections.Generic;

public class ArmoryUI : UI
{

	protected static Texture2D DaggerTexture = Resources.Load("media/ui/dagger") as Texture2D;
	protected static Rect DaggerRect { get { return new Rect((Main.NativeWidth / 5) * 2, (Main.NativeHeight / 5) * 0, Main.NativeWidth / 5, Main.NativeWidth / 10); } }

	protected static Texture2D ShortSwordTexture = Resources.Load("media/ui/short-sword") as Texture2D;
	protected static Rect ShortSwordRect { get { return new Rect((Main.NativeWidth / 5) * 2, (Main.NativeHeight / 5) * 1, Main.NativeWidth / 5, Main.NativeWidth / 10); } }

	// int selectedWeapon;
	string[] weaponNames;

	public ArmoryUI ()
	{
		// selectedWeapon = Weapon.Weapons.IndexOf(Main.CurrentWeapon);
		weaponNames = new string[Weapon.Weapons.Count];
		for(int i=0; i < Weapon.Weapons.Count; i++)
		{
			weaponNames[i] = Weapon.Weapons[i].Name;
		}
	}

	public override void OnGUI ()
	{
		base.OnGUI();

		GUI.DrawTexture(DaggerRect, DaggerTexture);
		if (Main.Clicked && DaggerRect.Contains(Main.TouchGuiLocation)) {
            Main.CurrentWeapon = Weapon.Weapons[0];
        } else if (Main.CurrentWeapon !=  Weapon.Weapons[0]) {
        	GUI.Box (DaggerRect, "");
		}

        GUI.DrawTexture(ShortSwordRect, ShortSwordTexture);
		if (Main.Clicked && ShortSwordRect.Contains(Main.TouchGuiLocation)) {
            Main.CurrentWeapon =  Weapon.Weapons[1];
        } else if (Main.CurrentWeapon !=  Weapon.Weapons[1]) {
        	GUI.Box (ShortSwordRect, "");
		}
	}
}
