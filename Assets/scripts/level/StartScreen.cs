using UnityEngine;

public class StartScreen : Level
{
    readonly Rect startGameRect;
    readonly Texture2D startButtonTexture;

    public StartScreen ()
    {
        startGameRect = new Rect (Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 3, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 3);
        startButtonTexture = Resources.Load ("media/ui/start-button") as Texture2D;
    }
    
    public override void Update ()
    {
        if (Main.Clicked && startGameRect.Contains (Main.TouchScreenLocation)) {
            Main.ChangeLevels (new Week1());
        }
    }
    
    public override void OnGUI ()
    {
        GUI.DrawTexture (startGameRect, startButtonTexture);
    }
    
}
