using UnityEngine;
using System.Collections.Generic;

public class ArmoryUI : UI
{

	int selectedWeapon;
	string[] weaponNames;

	public ArmoryUI ()
	{
		selectedWeapon = Weapon.Weapons.IndexOf(Main.CurrentWeapon);
		weaponNames = new string[Weapon.Weapons.Count];
		for(int i=0; i < Weapon.Weapons.Count; i++)
		{
			weaponNames[i] = Weapon.Weapons[i].Name;
		}
	}

	public override void OnGUI ()
	{
		base.OnGUI();

		selectedWeapon = GUILayout.SelectionGrid(selectedWeapon, weaponNames, 1);
		Main.CurrentWeapon = Weapon.Weapons[selectedWeapon];
	}
}
