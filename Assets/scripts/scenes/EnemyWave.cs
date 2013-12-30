using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyWave : Scene {

	public const float SwipeDamageDuration = 1f;

    Vector2 lastSwipeStart;
    Vector2 lastSwipeEnd;
    bool wasTouching;
    Swipe currentSwipe;

	List<SwipeDamage> swipes;
	public static GUIStyle SwipeDamageStyle;

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

		swipes = new List<SwipeDamage> ();
		SwipeDamageStyle = new GUIStyle();
		SwipeDamageStyle.fontSize = Main.FontLarge;
		SwipeDamageStyle.normal.textColor = Color.red;
		SwipeDamageStyle.alignment = TextAnchor.UpperLeft;

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
		GUI.Box(new Rect(Main.HealthBarPadding.x, Main.HealthBarPadding.y, Main.HealthBarSize.x * (baseHealth / Main.StartingBaseHealth), Main.HealthBarSize.y), GUIContent.none, healthBarStyle);

		for (int i = swipes.Count - 1; i >= 0; i--)
		{
			SwipeDamage swipe = swipes[i];
			if (swipe.startTime + SwipeDamageDuration < Time.time) {
				swipes.RemoveAt(i);
			} else {
				GUI.Label(new Rect(swipe.x, swipe.y, 1, 1), swipe.damage, SwipeDamageStyle);
			}
		}
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
		Vector2 middlePoint = Main.BoardLocationToGuiLocation((start + end) / 2f);
		swipes.Add(new SwipeDamage(Time.time, middlePoint.x, middlePoint.y, damage));
    }

    public override void EnemyAttack(Enemy enemy)
    {
        baseHealth = Mathf.Max(0, baseHealth - 100);
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
