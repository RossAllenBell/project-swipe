using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyWave : Scene {

    public static Vector2 HealthBarSize = new Vector2(0.8f * Main.NativeWidth, 0.05f * Main.NativeHeight);
    public static Vector2 HealthBarPadding = new Vector2((Main.NativeWidth - HealthBarSize.x) / 2, HealthBarSize.y);

    Vector2 lastSwipeStart;
    Vector2 lastSwipeEnd;
    bool wasTouching;
    Swipe currentSwipe;

    Texture2D healthBarTexture;
    GUIStyle healthBarStyle;

    List<GameObject> enemies;
    float baseHealth;

    public override void Begin() {
        healthBarTexture = Resources.Load ("media/ui/health-bar") as Texture2D;
        healthBarStyle = new GUIStyle();
        healthBarStyle.normal.background = healthBarTexture;

        wasTouching = false;
        currentSwipe = null;

        enemies = new List<GameObject>();
        baseHealth = Main.StartingBaseHealth;
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

        if (baseHealth <= 0) {
            Main.ChangeScenes(new StartScreen());
        }
    }
    
    public override void OnGUI()
    {
        GUI.Box(new Rect(HealthBarPadding.x, HealthBarPadding.y, HealthBarSize.x * (baseHealth / Main.StartingBaseHealth), HealthBarSize.y), GUIContent.none, healthBarStyle);
    }
    
    public override void End() {
        foreach(GameObject enemy in enemies) {
            Object.Destroy(enemy);
        }
        enemies.Clear();
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

    public override void EnemyDie(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    protected void AddSpawnedEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    protected bool EnemiesAlive()
    {
        return enemies.Count > 0;
    }
    
}
