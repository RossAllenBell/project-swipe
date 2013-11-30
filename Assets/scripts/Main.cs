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
    public const float SPAWN_COOLDOWN = 1f;
    float lastEnemySpawn;
    Vector2 lastSwipeStart;
    Vector2 lastSwipeEnd;
    bool touching;
    Swipe currentSwipe;

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

        touching = false;
        lastEnemySpawn = -SPAWN_COOLDOWN;
        currentSwipe = null;
    }
    
    void Update ()
    {

        if (Input.touchCount > 0 | Input.GetMouseButton (0)) {
            Vector2 location = Input.touchCount > 0 ? Input.GetTouch (0).position : (Vector2)Input.mousePosition;
            if (!touching) {
                lastSwipeStart = location;

                currentSwipe = ((GameObject)Instantiate (Resources.Load ("swipe"))).GetComponent<Swipe> ();
            }
            lastSwipeEnd = location;
            currentSwipe.SetStartAndEnd (InputLocationToBoardLocation (lastSwipeStart), InputLocationToBoardLocation (lastSwipeEnd));
            touching = true;
        } else {
            if (touching) {
                Swipe (InputLocationToBoardLocation (lastSwipeStart), InputLocationToBoardLocation (lastSwipeEnd));
                currentSwipe.Destroy ();
            }
            touching = false;
        }

        if (lastEnemySpawn < Time.time - SPAWN_COOLDOWN) {
            lastEnemySpawn = Time.time;
            Instantiate (Resources.Load ("enemy"));
        }

    }
    
    public void Swipe (Vector2 start, Vector2 end)
    {
        RaycastHit2D[] hitObjects = Physics2D.LinecastAll (start, end);
        float damage = GetBaseDamage () * GetDamageRatioForLength (Vector2.Distance (start, end));
        foreach (RaycastHit2D hitObject in hitObjects) {
            GameObject hitGameObject = hitObject.collider.gameObject;
            if (hitGameObject.tag == "Enemy") {
                hitGameObject.GetComponent<Enemy> ().Hit (damage);
            }
        }
    }

    public float GetBaseDamage ()
    {
        return 1;
    }

    public float GetDamageRatioForLength (float length)
    {
        return 1f - (1f * (length / BOARD_WIDTH));
    }

    public static Vector2 InputLocationToBoardLocation (Vector2 inputLocation)
    {
        return new Vector2 (BOARD_WIDTH * (inputLocation.x / NATIVE_WIDTH), BOARD_HEIGHT * (inputLocation.y / NATIVE_HEIGHT));
    }
}
