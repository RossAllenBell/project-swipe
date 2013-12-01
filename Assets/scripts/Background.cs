using UnityEngine;

public class Background : MonoBehaviour
{

    public void Reposition ()
    {
        transform.position = new Vector2 (Main.BoardWidth / 2f, Main.BoardWidth / 2f);
    }

}
