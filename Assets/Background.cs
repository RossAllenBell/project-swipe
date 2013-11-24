using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public void reposition() {
		transform.position = new Vector2(Main.BOARD_WIDTH / 2f, Main.BOARD_WIDTH / 2f);
	}

}
