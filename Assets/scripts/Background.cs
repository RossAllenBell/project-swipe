using UnityEngine;

public class Background : MonoBehaviour
{

    public static void Reposition ()
    {
        GameObject sky = GameObject.Find ("sky");
        GameObject trees = GameObject.Find ("trees");
        GameObject ground = GameObject.Find ("ground");
        GameObject fort = GameObject.Find ("fort");

        //this magically works because the background sprite is known to be twice the board length
        sky.transform.position = new Vector2 (Main.BoardWidth, Main.BoardWidth / 2f);
        //place the trees so that the top of the highest tree is the top of the board
        trees.transform.position = new Vector2 (Main.BoardWidth, Main.BoardHeight - (trees.GetComponent<SpriteRenderer>().bounds.size.y / 2));
        //place the ground so that it covers about 75% of the board
        ground.transform.position = new Vector2 (Main.BoardWidth, (Main.BoardHeight * 0.75f) - (ground.GetComponent<SpriteRenderer>().bounds.size.y / 2));
        //the base should rest against the bottom right corner
        fort.transform.position = new Vector2 ((Main.BoardWidth * 2) - (fort.GetComponent<SpriteRenderer>().bounds.size.x / 2), fort.GetComponent<SpriteRenderer>().bounds.size.y / 2);
    }

}
