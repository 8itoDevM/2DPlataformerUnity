using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public int damage = 0;

    public void Attack()
    {
        Player.Instance.life -= damage;
    }

    public void DetectPlayer()
    {

    }
}
