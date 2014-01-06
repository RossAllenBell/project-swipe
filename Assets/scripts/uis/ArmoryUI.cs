using UnityEngine;

public class ArmoryUI : UI
{

	int selectedWeapon = 0;

	public override void OnGUI ()
	{
		base.OnGUI();

		selectedWeapon = GUILayout.SelectionGrid(selectedWeapon, new string[]{"Dagger", "Short Sword"}, 1);
	}
}
