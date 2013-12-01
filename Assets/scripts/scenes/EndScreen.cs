using UnityEngine;

public class EndScreen : Scene
{
    readonly Rect endGameRect;
    readonly Texture2D endButtonTexture;
    
    public EndScreen ()
    {
        endGameRect = new Rect (Main.NativeWidth / 3, Main.NativeHeight / 3, Main.NativeWidth / 3, Main.NativeHeight / 3);
        endButtonTexture = Resources.Load ("media/ui/end-button") as Texture2D;
    }
    
    public override void Update ()
    {
        if (Main.Clicked && endGameRect.Contains (Main.TouchScreenLocation)) {
            Main.ChangeScenes (new StartScreen());
        }
    }
    
    public override void OnGUI ()
    {
        GUI.DrawTexture (endGameRect, endButtonTexture);
    }
    
}
