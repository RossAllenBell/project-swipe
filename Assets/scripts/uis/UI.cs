using UnityEngine;

public abstract class UI
{

	protected static Rect OkRect { get { return new Rect(Main.NativeWidth - (Main.NativeWidth / 10), 0, (Main.NativeWidth / 10), (Main.NativeHeight / 10)); } }
	protected static string OkText = "OK";

	public virtual void OnGUI()
	{
		GUI.Box (new Rect (0,0,Main.NativeWidth,Main.NativeHeight), "");
		if (GUI.Button(OkRect, OkText)) {
			Main.ClearCurrentUI();
		}
	}
}
