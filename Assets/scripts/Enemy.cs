using UnityEngine;

public class Enemy : MonoBehaviour
{

    public const float MIN_SPEED = 0.2f;
    public const float MAX_SPEED = 0.5f;
    public const float HIT_FADE_DUR = 0.1f;
    public const float HIT_FADE = 0.6f;
    public const float MAX_HP = 6;
    public const float ATTACK_COOLDOWN = 0.75f;

    float lastHit;
    Vector2 destination;
    float speed;
    float hp;
    float lastAttack;
    
    void Start ()
    {
        transform.position = new Vector2 (-0.5f, (Random.value * Main.BOARD_HEIGHT * 0.66f) + 0.5f);
        destination = new Vector2 (Main.BOARD_WIDTH - 1, transform.position.y);
        speed = (Random.value * (MAX_SPEED - MIN_SPEED)) + MIN_SPEED;

        lastHit = -HIT_FADE_DUR;
        hp = MAX_HP;
        lastAttack = 0;
    }
    
    void Update ()
    {
        if (Vector2.Distance(transform.position, destination) > 0) {
            transform.position = Vector2.MoveTowards (transform.position, destination, speed * Time.deltaTime);
        } else if(Time.time - lastAttack >= ATTACK_COOLDOWN) {
            Main.EnemyAttack(this);
            lastAttack = Time.time;
        }

        float fade = 1 - (HIT_FADE * Mathf.Max (0, 1 - ((Time.time - lastHit) / HIT_FADE_DUR)));
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
