using UnityEngine;

public class EnemyWave2 : EnemyWave {
    
    float koboldSpawnCooldown = 0.7f;
    float lastKoboldSpawn = -10f;
    int enemies = 40;
    
    public override void Update() {
        if (lastKoboldSpawn < Time.time - koboldSpawnCooldown && enemies > 0) {
            lastKoboldSpawn = Time.time;
            AddSpawnedEnemy((GameObject)Object.Instantiate (Resources.Load ("kobold")));
            enemies--;
        }
        
        if (enemies == 0 && !EnemiesAlive()) {
            Main.NextWave = 3;
            Main.ChangeScenes(new StartScreen());
        }

        base.Update();
    }
    
}
