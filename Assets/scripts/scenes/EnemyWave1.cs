using UnityEngine;

public class EnemyWave1 : EnemyWave {
    
    float spawnCooldown = 1f;
    float lastKoboldSpawn = -10f;
    int enemies = 10;

    public override void Update() {
        if (lastKoboldSpawn < Time.time - spawnCooldown && enemies > 0) {
            lastKoboldSpawn = Time.time;
            AddSpawnedEnemy((GameObject)Object.Instantiate (Resources.Load ("kobold")));
            enemies--;
        }

        if (enemies == 0 && !EnemiesAlive()) {
            Main.NextWave = 2;
            Main.ChangeScenes(new StartScreen());
        }

        base.Update();
    }
    
}
