﻿using UnityEngine;

public class Main : MonoBehaviour
{

    public const int NormalWidth = 1920;
    public const int NormalHeight = 1080;
    public const float DesiredBoardWidth = 10f;
    public static float GuiRatio;
    public static float GuiRatioWidth;
    public static float GuiRatioHeight;
    public static int NativeWidth;
    public static int NativeHeight;
    public static float BoardWidth;
    public static float BoardHeight;
    public static float BoardRadius;
    public static Vector3 BoardCenter;
    public static Vector3 BaseCenter;
    public const float BasicallyZero = 0.0001f;
    public static Scene CurrentScene;
    public const float StartingBaseHealth = 100f;

    public static bool Clicked { get { return click; } }

    public static Vector2 TouchScreenLocation { get { return touchScreenLocation; } }
    public static Vector2 TouchBoardLocation { get { return ScreenLocationToBoardLocation(touchScreenLocation); } }

    public static bool Touching { get { return touching; } }

    static bool click;
    static Vector2 touchScreenLocation;
    static bool touching;

    public static int NextWave;
	public static int Money;

	public static Rect MONEY_RECT;
	public static GUIStyle MONEY_STYLE;

    void Start ()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        NativeWidth = Screen.width;
        NativeHeight = Screen.height;
        
        GuiRatioWidth = (float)NativeWidth / (float)NormalWidth;
        GuiRatioHeight = (float)NativeWidth / (float)NormalWidth;
        GuiRatio = Mathf.Min (GuiRatioWidth, GuiRatioHeight);
        
		//just makes sure the game vic Reciew width is always 10 units wide
        float newOrthoSize = ((DesiredBoardWidth * NativeHeight) / NativeWidth) / 2f;
        Camera.main.orthographicSize = newOrthoSize;
        
        BoardHeight = 2f * Camera.main.orthographicSize;
        BoardWidth = BoardHeight * Camera.main.aspect;
        
        BoardRadius = (Mathf.Sqrt (Mathf.Pow (BoardWidth, 2) + Mathf.Pow (BoardHeight, 2))) / 2f;
        BoardCenter = new Vector3 (BoardWidth / 2f, BoardHeight / 2f, Camera.main.transform.position.z);
        BaseCenter = new Vector3 (BoardWidth * 1.5f, BoardHeight / 2f, Camera.main.transform.position.z);
        
        Camera.main.transform.position = BaseCenter;

        Background.Reposition();

        Debug.Log (string.Format ("GUI_RATIO: {0}", GuiRatio));
        Debug.Log (string.Format ("GUI_RATIO_WIDTH: {0}", GuiRatioWidth));
        Debug.Log (string.Format ("GUI_RATIO_HEIGHT: {0}", GuiRatioHeight));
        Debug.Log (string.Format ("NATIVE_WIDTH: {0}", NativeWidth));
        Debug.Log (string.Format ("NATIVE_HEIGHT: {0}", NativeHeight));
        Debug.Log (string.Format ("BOARD_WIDTH: {0}", BoardWidth));
        Debug.Log (string.Format ("BOARD_HEIGHT: {0}", BoardHeight));
        Debug.Log (string.Format ("BOARD_RADIUS: {0}", BoardRadius));
        Debug.Log (string.Format ("BOARD_CENTER: {0}", BoardCenter));

		MONEY_RECT = new Rect(BoardCenter.x, BoardCenter.y, 500, 500);
		MONEY_STYLE = new GUIStyle();
		MONEY_STYLE.fontSize = (int) (40 * GuiRatio);
		MONEY_STYLE.normal.textColor = Color.red;
		MONEY_STYLE.alignment = TextAnchor.MiddleCenter;

        NextWave = 1;
		Money = 0;
        ChangeScenes (new StartScreen ());
    }
    
    void Update ()
    {
        if (Input.GetKeyUp (KeyCode.Escape)) {
            if (CurrentScene is StartScreen) {
                Application.Quit ();
            } else {
                ChangeScenes (new StartScreen ());
            }
        }

        if (Input.GetKey (KeyCode.Space)) {
            Time.timeScale = 5.0f;
        } else {
            Time.timeScale = 1.0f;
        }

        if (Input.touchCount > 0 | Input.GetMouseButton (0)) {
            Vector2 tempLocation = Input.touchCount > 0 ? Input.GetTouch (0).position : (Vector2)Input.mousePosition;
            touchScreenLocation = new Vector2 (tempLocation.x, tempLocation.y);
            click = !touching;
            touching = true;
        } else {
            click = false;
            touching = false;
        }

        CurrentScene.Update ();
    }

    void OnGUI ()
    {
		GUI.Label(MONEY_RECT, Money.ToString (), MONEY_STYLE);
        CurrentScene.OnGUI ();
    }

    public static void EnemyAttack(Enemy enemy) {
        CurrentScene.EnemyAttack(enemy);
    }

    public static float GetBaseDamage ()
    {
        return 1;
    }

    public static float GetDamageRatioForLength (float length)
    {
        return 1f - (1f * (length / BoardWidth));
    }

    public static Vector2 ScreenLocationToBoardLocation (Vector2 inputLocation)
    {
        return new Vector2 (BoardWidth * (inputLocation.x / NativeWidth), BoardHeight * (inputLocation.y / NativeHeight));
    }

    public static void ChangeScenes (Scene scene)
    {
        if (CurrentScene != null) {
            CurrentScene.End ();
        }
        CurrentScene = scene;
        CurrentScene.Begin ();
    }
}
