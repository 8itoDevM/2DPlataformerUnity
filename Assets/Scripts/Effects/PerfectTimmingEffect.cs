using UnityEngine;

public class PerfectTimmingEffect : GetAttacked
{
    EnemyManager enemy_manager;

    public override void Effect()
    {
        Player.Instance.atk_script.enabled = false;
        float temp_jump_force = Player.Instance.jump_force;
        float temp_speed = Player.Instance.speed;

        enemy_manager = transform.parent.GetComponent<EnemyManager>();

        enemy_manager.life -= 1;
        enemy_manager.FuckingDie();

        Player.Instance.jump_force += 7;
        Player.Instance.speed += 1;
        Player.Instance.grounded = true;
        if (enemy_manager.alive)
            Player.Instance.Jump();

        Player.Instance.jump_force = temp_jump_force;
        Player.Instance.speed = temp_speed;
        Player.Instance.atk_script.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tacotimming"))
        {
            Player.Instance.can_timming_atk = true;

            if(Player.Instance.atacking_timing)
                Effect();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("tacotimming"))
        {
            Player.Instance.can_timming_atk = true;

            if (Player.Instance.atacking_timing)
                Effect();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("tacotimming"))
            Player.Instance.can_timming_atk = false;
    }
}
