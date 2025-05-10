using UnityEngine;

public class DieScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Player.Instance.life -= Player.Instance.life;
        }
    }
}
