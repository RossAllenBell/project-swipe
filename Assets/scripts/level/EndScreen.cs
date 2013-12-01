using UnityEngine;

public class EndScreen : Level
{
    readonly Rect endGameRect;
    readonly Texture2D endButtonTexture;
    
    public EndScreen ()
    {
        endGameRect = new Rect (Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 3, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 3);
        endButtonTexture = Resources.Load ("media/ui/end-button") as Texture2D;
    }
    
    public override void Update ()
    {
        if (Main.Clicked && endGameRect.Contains (Main.TouchScreenLocation)) {
            Main.ChangeLevels (new StartScreen());
        }
    }
    
    public override void OnGUI ()
    {
        GUI.DrawTexture (endGameRect, endButtonTexture);
    }
    
}
