using UnityEngine;

public class StartScreen : Scene
{
    readonly Rect startGameRect;
    readonly Texture2D startButtonTexture;

    public StartScreen ()
    {
        startGameRect = new Rect (Main.NativeWidth / 3, Main.NativeHeight / 3, Main.NativeWidth / 3, Main.NativeHeight / 3);
        startButtonTexture = Resources.Load ("media/ui/start-button") as Texture2D;
    }
    
    public override void Update ()
    {
        if (Main.Clicked && startGameRect.Contains (Main.TouchScreenLocation)) {
            Main.ChangeScenes (new EnemyWave1());
        }
    }
    
    public override void OnGUI ()
    {
        GUI.DrawTexture (startGameRect, startButtonTexture);
    }
    
}
