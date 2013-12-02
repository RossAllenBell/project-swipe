using UnityEngine;

public class EnemyWave2 : EnemyWave {
    
    float enemySpawnCooldown = 1f;
    float stickManSpawnCooldown = 1.7f;
    float lastEnemySpawn = -10f;
    float lastStickManSpawn = -10f;
    int enemies = 40;
    
    public override void Update() {
        if (lastEnemySpawn < Time.time - enemySpawnCooldown && enemies > 0) {
            lastEnemySpawn = Time.time;
            AddSpawnedEnemy((GameObject)Object.Instantiate (Resources.Load ("enemy")));
            enemies--;
        }

        if (lastStickManSpawn < Time.time - stickManSpawnCooldown && enemies > 0) {
            lastStickManSpawn = Time.time;
            AddSpawnedEnemy((GameObject)Object.Instantiate (Resources.Load ("stick-man")));
            enemies--;
        }
        
        if (enemies == 0 && !EnemiesAlive()) {
            Main.NextWave = 3;
            Main.ChangeScenes(new StartScreen());
        }

        base.Update();
    }
    
}
