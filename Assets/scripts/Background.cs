using UnityEngine;

public class Background : MonoBehaviour
{

    public void Reposition ()
    {
        transform.position = new Vector2 (Main.BOARD_WIDTH / 2f, Main.BOARD_WIDTH / 2f);
    }

}
