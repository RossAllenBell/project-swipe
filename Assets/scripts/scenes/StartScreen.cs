using UnityEngine;

public class StartScreen : Scene
{
    readonly Rect startGameRect;
	readonly GUIStyle startGameStyle;
    readonly float panSpeed;

    bool starting = false;

    public StartScreen ()
    {
        startGameRect = new Rect (0, 0, Main.NativeWidth / 5, Main.NativeHeight / 5);
		startGameStyle = new GUIStyle();
		startGameStyle.fontSize = Main.FontLargest;
		startGameStyle.normal.textColor = Color.black;
		startGameStyle.alignment = TextAnchor.MiddleCenter;
        panSpeed = 4.0f;
    }
    
    public override void Update ()
    {
        if (!starting && Main.Clicked && startGameRect.Contains (Main.TouchGuiLocation)) {
            starting = true;
        }

        if (starting) {
            if (Camera.main.transform.position == Main.WaveCenter) {
                Main.ChangeScenes (WaveLookup.GetWave(Main.NextWave));
            } else {
                Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, Main.WaveCenter, panSpeed * Time.deltaTime);
            }
        } else if(Camera.main.transform.position != Main.BaseCenter) {
            Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, Main.BaseCenter, panSpeed * Time.deltaTime);
        }
    }
    
    public override void OnGUI ()
    {
        if (!starting) {
			GUI.Label(startGameRect, "Start", startGameStyle);
        }
    }
    
}
