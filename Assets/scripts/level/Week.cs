using UnityEngine;
using System.Collections.Generic;

public abstract class Week : Level {

    public const float SPAWN_COOLDOWN = 1f;
    public const float MAX_BASE_HEALTH = 100f;

    public static Vector2 HEALTH_BAR_SIZE = new Vector2(0.8f * Main.NATIVE_WIDTH, 0.05f * Main.NATIVE_HEIGHT);
    public static Vector2 HEALTH_BAR_PADDING = new Vector2((Main.NATIVE_WIDTH - HEALTH_BAR_SIZE.x) / 2, HEALTH_BAR_SIZE.y);

    float lastEnemySpawn;
    Vector2 lastSwipeStart;
    Vector2 lastSwipeEnd;
    bool wasTouching;
    Swipe currentSwipe;

    Texture2D healthBarTexture;
    GUIStyle healthBarStyle;

    float baseHealth = MAX_BASE_HEALTH;
    List<GameObject> enemies;
    
    public override void Begin() {
        healthBarTexture = Resources.Load ("media/ui/health-bar") as Texture2D;
        healthBarStyle = new GUIStyle();
        healthBarStyle.normal.background = healthBarTexture;

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
    
    public override void OnGUI()
    {
        GUI.Box(new Rect(HEALTH_BAR_PADDING.x, HEALTH_BAR_PADDING.y, HEALTH_BAR_SIZE.x * (baseHealth / MAX_BASE_HEALTH), HEALTH_BAR_SIZE.y), GUIContent.none, healthBarStyle);
    }
    
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

    public override void EnemyAttack(Enemy enemy)
    {
        baseHealth = Mathf.Max(0, baseHealth - 1);
    }
    
}
