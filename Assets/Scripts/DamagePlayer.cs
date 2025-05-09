using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int ammount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Player.Instance.life -= ammount;
            Player.Instance.life_bar.UpdateHearts();
        }
    }
}
