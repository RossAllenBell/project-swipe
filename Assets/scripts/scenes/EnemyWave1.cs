using UnityEngine;

public class EnemyWave1 : EnemyWave {
    
    float spawnCooldown = 1f;
    float lastEnemySpawn = -10f;
    int enemies = 30;

    public override void Update() {
        base.Update();
        
        if (lastEnemySpawn < Time.time - spawnCooldown && enemies > 0) {
            lastEnemySpawn = Time.time;
            AddSpawnedEnemy((GameObject)Object.Instantiate (Resources.Load ("enemy")));
            enemies--;
        }

        if (enemies == 0 && !EnemiesAlive()) {
            Main.ChangeScenes(new EnemyWave2());
        }
    }
    
}
