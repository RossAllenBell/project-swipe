using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

    public const int NormalWidth = 1920;
    public const int NormalHeight = 1080;
    public const float BoardWidth = 10f;
    public static float GuiRatio;
    public static float GuiRatioWidth;
    public static float GuiRatioHeight;
    public static int NativeWidth;
    public static int NativeHeight;
    public static float VisibleBoardHeight;
    public static Vector3 WaveCenter;
    public static Vector3 BaseCenter;
    public const float BasicallyZero = 0.0001f;
    public static Scene CurrentScene;
    public static float StartingBaseHealth = 10000f;

	public static int FontLargest = 150;
	public static int FontLarge = 100;

    public static bool Clicked { get { return click; } }

	public static Vector2 TouchLocation { get { return touchLocation; } }
	public static Vector2 TouchGuiLocation { get { return TouchLocationToGuiLocation(touchLocation); } }
    public static Vector2 TouchBoardLocation { get { return TouchLocationToBoardLocation(touchLocation); } }
	public static float CameraX { get { return Camera.main.transform.position.x - (BoardWidth / 2); } }

    public static bool Touching { get { return touching; } }

    static bool click;
    static Vector2 touchLocation;
    static bool touching;

	public static Vector2 HealthBarSize = new Vector2(0.8f * Main.NativeWidth, 0.05f * Main.NativeHeight);
	public static Vector2 HealthBarPadding = new Vector2((Main.NativeWidth - HealthBarSize.x) / 2, HealthBarSize.y);

    public static int NextWave;
	public static int Money;

	public static Rect MoneyRect;
	public static GUIStyle MoneyStyle;

	static UI currentUI;
	public static UI CurrentUI { get { return currentUI; } }

    public static Weapon CurrentWeapon;
    public static List<Weapon> PurchasedWeapons;

    void Start ()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        NativeWidth = Screen.width;
        NativeHeight = Screen.height;
        
        GuiRatioWidth = (float)NativeWidth / (float)NormalWidth;
        GuiRatioHeight = (float)NativeWidth / (float)NormalWidth;
        GuiRatio = Mathf.Min (GuiRatioWidth, GuiRatioHeight);

		FontLargest = (int) (FontLargest * GuiRatio);
		FontLarge = (int) (FontLarge * GuiRatio);
        
		//just makes sure the game view width is always 10 units wide
        float newOrthoSize = ((BoardWidth * NativeHeight) / NativeWidth) / 2f;
        Camera.main.orthographicSize = newOrthoSize;
        
        VisibleBoardHeight = 2f * Camera.main.orthographicSize;
        
        WaveCenter = new Vector3 (BoardWidth / 2f, VisibleBoardHeight / 2f, Camera.main.transform.position.z);
		BaseCenter = new Vector3 (BoardWidth * 1.5f, VisibleBoardHeight / 2f, Camera.main.transform.position.z);
        
        Camera.main.transform.position = BaseCenter;

        Background.Reposition();

		HealthBarSize = new Vector2(0.8f * NativeWidth, 0.05f * NativeHeight);
		HealthBarPadding = new Vector2((NativeWidth - HealthBarSize.x) / 2, HealthBarSize.y);

		MoneyRect = new Rect(HealthBarPadding.x + HealthBarSize.x + (50 * GuiRatio), HealthBarPadding.y, 1, 1);
		MoneyStyle = new GUIStyle();
		MoneyStyle.fontSize = Main.FontLarge;
		MoneyStyle.normal.textColor = Color.yellow;
		MoneyStyle.alignment = TextAnchor.UpperLeft;

        NextWave = 1;
		Money = 0;
        ChangeScenes (new StartScreen ());

        CurrentWeapon = Weapon.Weapons[0];
        PurchasedWeapons = new List<Weapon>();
        PurchasedWeapons.Add(CurrentWeapon);
    }
    
    void Update ()
    {
        if (Input.GetKeyUp (KeyCode.Escape)) {
            if (CurrentScene is StartScreen) {
				if (currentUI != null) {
					currentUI = null;
				} else {
                	Application.Quit ();
				}
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
            touchLocation = new Vector2 (tempLocation.x, tempLocation.y);
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
		GUI.Label(MoneyRect, Money.ToString (), MoneyStyle);
        CurrentScene.OnGUI ();
    }

    public static void EnemyAttack(Enemy enemy) {
        CurrentScene.EnemyAttack(enemy);
    }

    public static float GetBaseDamage ()
    {
        return CurrentWeapon.Damage;
    }

    public static float GetDamageRatioForLength (float length)
    {
        return 1f - ((1f - CurrentWeapon.LengthMitigation) * (length / BoardWidth));
    }

	public static Vector2 TouchLocationToGuiLocation (Vector2 touchLocation)
	{
		return new Vector2 (touchLocation.x, NativeHeight - touchLocation.y);
	}

	public static Vector2 TouchLocationToBoardLocation (Vector2 touchLocation)
	{
		return new Vector2 ((BoardWidth * (touchLocation.x / NativeWidth)) + Main.CameraX, VisibleBoardHeight * (touchLocation.y / NativeHeight));
	}

	public static Vector2 BoardLocationToGuiLocation (Vector2 boardLocation)
	{
		return new Vector2 (NativeWidth * ((boardLocation.x - Main.CameraX) / BoardWidth), NativeHeight - (NativeHeight * (boardLocation.y / VisibleBoardHeight)));
	}

    public static void ChangeScenes (Scene scene)
    {
        if (CurrentScene != null) {
            CurrentScene.End ();
        }
        CurrentScene = scene;
        CurrentScene.Begin ();
    }

	public static void SetCurrentUI (UI ui)
	{
		if (currentUI == null) {
			currentUI = ui;
		}
	}

	public static void ClearCurrentUI ()
	{
		currentUI = null;
	}
}
