using UnityEngine;

public class EnemyEffect : GetAttacked
{
    Rigidbody2D rb;
    EnemyManager enemy_manager;

    public override void Effect()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_manager = GetComponent<EnemyManager>();
        base.Effect();
        rb.AddForce(new Vector2(Mathf.Sign(rb.position.x - Player.Instance.transform.position.x), 2.3f) * 1.5f, ForceMode2D.Impulse);
        enemy_manager.life -= 1;
        enemy_manager.FuckingDie();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("taco"))
            Effect();
    }
}
