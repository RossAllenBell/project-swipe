using UnityEngine;

public abstract class UI
{

	protected static Texture2D BackTexture = Resources.Load("media/ui/back") as Texture2D;
	protected static Rect BackRect { get { return new Rect(0, Main.NativeHeight - (Main.NativeWidth / 5), (Main.NativeWidth / 5), (Main.NativeWidth / 5)); } }

	public virtual void OnGUI()
	{
		GUI.Box (new Rect (0,0,Main.NativeWidth,Main.NativeHeight), "");
		GUI.DrawTexture(BackRect, BackTexture);

		if (Main.Clicked && BackRect.Contains(Main.TouchGuiLocation)) {
            Main.ClearCurrentUI();
        }
	}
}
