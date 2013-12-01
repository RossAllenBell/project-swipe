using UnityEngine;

public class Background : MonoBehaviour
{

    public void Reposition ()
    {
        //this magically works because the background sprite is known to be twice the board length
        transform.position = new Vector2 (Main.BoardWidth, Main.BoardWidth / 2f);
    }

}
