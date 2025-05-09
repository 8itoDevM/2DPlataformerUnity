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
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fall_multiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocityY > 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (low_jump_mult - 1) * Time.fixedDeltaTime;
        }
    }

    public void FuckingDie()
    {
        if(alive && life <= 0)
        {
            CrippleMaker();
            alive = false;
        }
    }

    public void CrippleMaker()
    {
        Destroy(enemy_move);
        Destroy(enemy_edging);
    }
}
