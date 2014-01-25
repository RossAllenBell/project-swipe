using UnityEngine;
using System.Collections.Generic;

public class ArmoryUI : UI
{

	public static readonly Texture2D SelectedTexture = Resources.Load("media/ui/selected") as Texture2D;
	public static readonly Texture2D NotSelectedTexture = Resources.Load("media/ui/not-selected") as Texture2D;
	public static readonly Texture2D GreyBox = Resources.Load("media/ui/grey-box") as Texture2D;

	public readonly GUIStyle BuyStyle;
	public readonly GUIStyle CantBuyStyle;

	public ArmoryUI()
	{
		BuyStyle = new GUIStyle();
		BuyStyle.fontSize = Main.FontLarge;
		BuyStyle.normal.textColor = Color.yellow;
		BuyStyle.alignment = TextAnchor.MiddleCenter;

		CantBuyStyle = new GUIStyle();
		CantBuyStyle.fontSize = Main.FontLarge;
		CantBuyStyle.normal.textColor = Color.black;
		CantBuyStyle.alignment = TextAnchor.MiddleCenter;
	}

	public override void OnGUI ()
	{
		base.OnGUI();

		for (int i=0; i < Weapon.Weapons.Count; i++) {
			Rect weaponRect = new Rect((Main.NativeWidth / 5) * 2, (Main.NativeHeight / 5) * i, Main.NativeWidth / 5, Main.NativeWidth / 10);
			GUI.DrawTexture(weaponRect, Weapon.Weapons[i].Texture);

			Rect selectWeaponRect = new Rect(((Main.NativeWidth / 5) * 2) - (Main.NativeHeight / 5), (Main.NativeHeight / 5) * i, Main.NativeHeight / 5, Main.NativeHeight / 5);
			if (Main.CurrentWeapon == Weapon.Weapons[i]) {
				GUI.DrawTexture(selectWeaponRect, SelectedTexture);
			} else {
				if (Main.PurchasedWeapons.Contains(Weapon.Weapons[i])) {
					GUI.DrawTexture(selectWeaponRect, NotSelectedTexture);
					if (Main.Clicked && selectWeaponRect.Contains (Main.TouchGuiLocation)) {
			            Main.CurrentWeapon = Weapon.Weapons[i];
			        }
				} else {
					Rect buyWeaponRect = new Rect(((Main.NativeWidth / 5) * 3) + 10, ((Main.NativeHeight / 5) * i) + 10, (Main.NativeHeight / 5) - 20, (Main.NativeHeight / 5) - 20);
					GUI.DrawTexture(buyWeaponRect, GreyBox);
					GUI.Label(buyWeaponRect, Weapon.Weapons[i].Cost.ToString(), Weapon.Weapons[i].Cost <= Main.Money? BuyStyle : CantBuyStyle);
					if (Weapon.Weapons[i].Cost <= Main.Money && Main.Clicked && buyWeaponRect.Contains (Main.TouchGuiLocation)) {
			            Main.PurchasedWeapons.Add(Weapon.Weapons[i]);
			            Main.Money -= Weapon.Weapons[i].Cost;
			        }
				}
			}
		}
	}
}
