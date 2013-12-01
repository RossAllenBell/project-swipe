using UnityEngine;

public class StartScreen : Scene
{
    readonly Rect startGameRect;
    readonly Texture2D startButtonTexture;
    readonly float panSpeed;

    bool starting = false;

    public StartScreen ()
    {
        startGameRect = new Rect (Main.NativeWidth / 3, Main.NativeHeight / 3, Main.NativeWidth / 3, Main.NativeHeight / 3);
        startButtonTexture = Resources.Load ("media/ui/start-button") as Texture2D;
        panSpeed = 4.0f;
    }
    
    public override void Update ()
    {
        if (!starting && Main.Clicked && startGameRect.Contains (Main.TouchScreenLocation)) {
            starting = true;
        }

        if (starting) {
            if (Camera.main.transform.position == Main.BoardCenter) {
                Main.ChangeScenes (new EnemyWave1());
            } else {
                Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, Main.BoardCenter, panSpeed * Time.deltaTime);
            }
        } else if(Camera.main.transform.position != Main.BaseCenter) {
            Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, Main.BaseCenter, panSpeed * Time.deltaTime);
        }
    }
    
    public override void OnGUI ()
    {
        if (!starting) {
            GUI.DrawTexture (startGameRect, startButtonTexture);
        }
    }
    
}
