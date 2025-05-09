using UnityEngine;

public class FeetDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("enemy"))
        Player.Instance.can_timming_atk = false;
    }
}
