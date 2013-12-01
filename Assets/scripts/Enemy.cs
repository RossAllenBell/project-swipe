using UnityEngine;

public class Enemy : MonoBehaviour
{

    public const float MinSpeed = 0.2f;
    public const float MaxSpeed = 0.5f;
    public const float HitFadeDur = 0.1f;
    public const float HitFade = 0.6f;
    public const float MaxHp = 6;
    public const float AttackCooldown = 0.75f;

    float lastHit;
    Vector2 destination;
    float speed;
    float hp;
    float lastAttack;
    
    void Start ()
    {
        transform.position = new Vector2 (-0.5f, (Random.value * Main.BoardHeight * 0.66f) + 0.5f);
        destination = new Vector2 (Main.BoardWidth - 1, transform.position.y);
        speed = (Random.value * (MaxSpeed - MinSpeed)) + MinSpeed;

        lastHit = -HitFadeDur;
        hp = MaxHp;
        lastAttack = 0;
    }
    
    void Update ()
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
            Destroy (gameObject);
        }
    }

    public void Hit (float damage)
    {
        lastHit = Time.time;
        hp -= damage;
    }
}
