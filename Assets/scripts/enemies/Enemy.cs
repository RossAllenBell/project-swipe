﻿using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public const float HitFadeDur = 0.1f;
    public const float HitFade = 0.6f;
    public const float DefaultMinSpeed = 0.4f;
    public const float DefaultMaxSpeed = 0.6f;
    public const float DefaultMaxHp = 10f;
    public const float DefaultAttackCooldown = 0.75f;

    virtual protected float MinSpeed { get{return DefaultMinSpeed;} }
    virtual protected float MaxSpeed { get{return DefaultMaxSpeed;} }
    virtual protected float MaxHp { get{return DefaultMaxHp;} }
    virtual protected float AttackCooldown { get{return DefaultAttackCooldown;} }

    float lastHit;
    Vector2 destination;
    float speed;
    float hp;
    float lastAttack;
    
    public virtual void Start ()
    {
        transform.position = new Vector2 (-0.5f, (Random.value * Main.BoardHeight * 0.66f) + 0.5f);
        destination = new Vector2 (Main.BoardWidth - 1, transform.position.y);
        speed = (Random.value * (MaxSpeed - MinSpeed)) + MinSpeed;

        lastHit = -HitFadeDur;
        hp = MaxHp;
        lastAttack = 0;
    }
    
    public virtual void Update ()
    {
        if (Vector2.Distance(transform.position, destination) > 0) {
            transform.position = Vector2.MoveTowards (transform.position, destination, speed * Time.deltaTime);
        } else if(Time.time - lastAttack >= AttackCooldown) {
            Main.EnemyAttack(this);
            lastAttack = Time.time;
        }

        float fade = 1 - (HitFade * Mathf.Max (0, 1 - ((Time.time - lastHit) / HitFadeDur)));
        gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fade);

        if (hp <= 0) {
            Main.CurrentScene.EnemyDie(gameObject);
            Destroy (gameObject);
        }
    }

    public virtual void Hit (float damage)
    {
        lastHit = Time.time;
        hp -= damage;
    }
}