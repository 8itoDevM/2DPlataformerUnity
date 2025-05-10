using UnityEngine;
using UnityEngine.Windows;

public class EnemyManager : MonoBehaviour
{
    public int life = 0;
    public bool alive = true;
    [SerializeField] float fall_multiplier;
    [SerializeField] float low_jump_mult;

    Rigidbody2D rb;
    EnemyEffect enemy_effect;
    EnemyAvoidEdging enemy_edging;
    EnemyMove enemy_move;
    EnemyAtack enemy_atack;

    void Start()
    {
        enemy_move = GetComponent<EnemyMove>();
        enemy_edging = GetComponent<EnemyAvoidEdging>();
        enemy_effect = GetComponent<EnemyEffect>();
        enemy_atack = GetComponent<EnemyAtack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y < 0)
        {
            FallFaster(fall_multiplier);
        }
        else if (rb.linearVelocity.y > 0)
        {
            FallFaster(low_jump_mult);
        }
    }

    public void GetHurt()
    {
        enemy_move.animator.SetTrigger("hurt");
        life--;
        Die();
    }

    public void FallFaster(float multi)
    {
        rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (multi - 1) * Time.fixedDeltaTime;
    }

    public void Die()
    {
        if (alive && life <= 0)
        {
            StopWalking();
            enemy_move.animator.SetTrigger("dead");
            alive = false;
        }
    }

    public void StopWalking()
    {
        Destroy(enemy_move);
        Destroy(enemy_edging);
    }
}
