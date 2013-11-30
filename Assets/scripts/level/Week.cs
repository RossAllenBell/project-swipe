using UnityEngine;
using System.Collections.Generic;

public abstract class Week : Level {

    public const float SPAWN_COOLDOWN = 1f;

    float lastEnemySpawn;
    Vector2 lastSwipeStart;
    Vector2 lastSwipeEnd;
    bool wasTouching;
    Swipe currentSwipe;

    List<GameObject> enemies;
    
    public override void Begin() {
        enemies = new List<GameObject>();
        wasTouching = false;
        lastEnemySpawn = -SPAWN_COOLDOWN;
        currentSwipe = null;
    }
    
    public override void Update() {
        if (Main.Touching) {
            if (!wasTouching) {
                lastSwipeStart = Main.TouchBoardLocation;
                
                currentSwipe = ((GameObject)Object.Instantiate (Resources.Load ("swipe"))).GetComponent<Swipe> ();
            }
            lastSwipeEnd = Main.TouchBoardLocation;
            currentSwipe.SetStartAndEnd (lastSwipeStart, lastSwipeEnd);
            wasTouching = true;
        } else {
            if (wasTouching) {
                Swipe (lastSwipeStart, lastSwipeEnd);
                currentSwipe.Destroy ();
            }
            wasTouching = false;
        }
        
        if (lastEnemySpawn < Time.time - SPAWN_COOLDOWN) {
            lastEnemySpawn = Time.time;
            enemies.Add((GameObject)Object.Instantiate (Resources.Load ("enemy")));
        }
    }
    
    public override void OnGUI() {}
    
    public override void End() {
        foreach(GameObject enemy in enemies) {
            Object.Destroy(enemy);
        }
    }
    
    public void Swipe (Vector2 start, Vector2 end)
    {
        RaycastHit2D[] hitObjects = Physics2D.LinecastAll (start, end);
        float damage = Main.GetBaseDamage () * Main.GetDamageRatioForLength (Vector2.Distance (start, end));
        foreach (RaycastHit2D hitObject in hitObjects) {
            GameObject hitGameObject = hitObject.collider.gameObject;
            if (hitGameObject.tag == "Enemy") {
                hitGameObject.GetComponent<Enemy> ().Hit (damage);
            }
        }
    }
    
}
