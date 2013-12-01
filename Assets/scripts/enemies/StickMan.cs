using UnityEngine;

public class StickMan : Enemy
{
    protected override float MinSpeed { get{return Enemy.DefaultMinSpeed * 2f;} }
    protected override float MaxSpeed { get{return Enemy.DefaultMaxSpeed * 2f;} }
    protected override float MaxHp { get{return Enemy.DefaultMaxHp * 0.5f;} }
}
