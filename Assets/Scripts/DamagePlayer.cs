using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int ammount = 0;

    // Script meant to give damage to the player through trigger, won't be used for enemies
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Player.Instance.life -= ammount;
            Player.Instance.life_bar.UpdateHearts();
            Player.Instance.animator.SetTrigger("dead");
        }
    }
}
