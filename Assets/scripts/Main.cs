using UnityEngine;

public class Main : MonoBehaviour
{

    public const int NORMAL_WIDTH = 1920;
    public const int NORMAL_HEIGHT = 1080;
    public const float DESIRED_BOARD_WIDTH = 10f;
    public static float GUI_RATIO;
    public static float GUI_RATIO_WIDTH;
    public static float GUI_RATIO_HEIGHT;
    public static int NATIVE_WIDTH;
    public static int NATIVE_HEIGHT;
    public static float BOARD_WIDTH;
    public static float BOARD_HEIGHT;
    public static float BOARD_RADIUS;
    public static Vector2 BOARD_CENTER;
    public static Level CURRENT_LEVEL;

    public static bool Clicked { get { return click; } }

    public static Vector2 TouchScreenLocation { get { return touchScreenLocation; } }
    public static Vector2 TouchBoardLocation { get { return ScreenLocationToBoardLocation(touchScreenLocation); } }

    public static bool Touching { get { return touching; } }

    static bool click;
    static Vector2 touchScreenLocation;
    static bool touching;

    void Start ()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        NATIVE_WIDTH = Screen.width;
        NATIVE_HEIGHT = Screen.height;
        
        GUI_RATIO_WIDTH = (float)NATIVE_WIDTH / (float)NORMAL_WIDTH;
        GUI_RATIO_HEIGHT = (float)NATIVE_WIDTH / (float)NORMAL_WIDTH;
        GUI_RATIO = Mathf.Min (GUI_RATIO_WIDTH, GUI_RATIO_HEIGHT);
        
        //just makes sure the game view width is always 10 units wide
        float newOrthoSize = ((DESIRED_BOARD_WIDTH * NATIVE_HEIGHT) / NATIVE_WIDTH) / 2f;
        Camera.main.orthographicSize = newOrthoSize;
        
        BOARD_HEIGHT = 2f * Camera.main.orthographicSize;
        BOARD_WIDTH = BOARD_HEIGHT * Camera.main.aspect;
        
        Vector3 newCameraLocation = Camera.main.transform.position;
        newCameraLocation.y = newOrthoSize;
        newCameraLocation.x = BOARD_WIDTH / 2f;
        Camera.main.transform.position = newCameraLocation;

        BOARD_RADIUS = (Mathf.Sqrt (Mathf.Pow (BOARD_WIDTH, 2) + Mathf.Pow (BOARD_HEIGHT, 2))) / 2f;
        BOARD_CENTER = new Vector2 (BOARD_WIDTH / 2f, BOARD_HEIGHT / 2f);

        GameObject.Find ("background").GetComponent<Background> ().Reposition ();

        Debug.Log (string.Format ("GUI_RATIO: {0}", GUI_RATIO));
        Debug.Log (string.Format ("GUI_RATIO_WIDTH: {0}", GUI_RATIO_WIDTH));
        Debug.Log (string.Format ("GUI_RATIO_HEIGHT: {0}", GUI_RATIO_HEIGHT));
        Debug.Log (string.Format ("NATIVE_WIDTH: {0}", NATIVE_WIDTH));
        Debug.Log (string.Format ("NATIVE_HEIGHT: {0}", NATIVE_HEIGHT));
        Debug.Log (string.Format ("BOARD_WIDTH: {0}", BOARD_WIDTH));
        Debug.Log (string.Format ("BOARD_HEIGHT: {0}", BOARD_HEIGHT));
        Debug.Log (string.Format ("BOARD_RADIUS: {0}", BOARD_RADIUS));
        Debug.Log (string.Format ("BOARD_CENTER: {0}", BOARD_CENTER));

        ChangeLevels (new StartScreen ());
    }
    
    void Update ()
    {
        if (Input.GetKeyUp (KeyCode.Escape)) {
            if (CURRENT_LEVEL is StartScreen) {
                Application.Quit ();
            } else {
                ChangeLevels (new StartScreen ());
            }
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

        CURRENT_LEVEL.Update ();
    }

    void OnGUI ()
    {
        CURRENT_LEVEL.OnGUI ();
    }

    public static float GetBaseDamage ()
    {
        return 1;
    }

    public static float GetDamageRatioForLength (float length)
    {
        return 1f - (1f * (length / BOARD_WIDTH));
    }

    public static Vector2 ScreenLocationToBoardLocation (Vector2 inputLocation)
    {
        return new Vector2 (BOARD_WIDTH * (inputLocation.x / NATIVE_WIDTH), BOARD_HEIGHT * (inputLocation.y / NATIVE_HEIGHT));
    }

    public static void ChangeLevels (Level level)
    {
        if (CURRENT_LEVEL != null) {
            CURRENT_LEVEL.End ();
        }
        CURRENT_LEVEL = level;
        CURRENT_LEVEL.Begin ();
    }
}
