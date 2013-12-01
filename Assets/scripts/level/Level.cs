using UnityEngine;

public abstract class Level {
	
    public virtual void Begin() {}
	
	public virtual void Update() {}
	
	public virtual void OnGUI() {}
	
	public virtual void End() {}

    public virtual void EnemyAttack(Enemy enemy) {}
	
}
