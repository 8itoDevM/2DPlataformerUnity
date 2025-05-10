using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public int damage = 0;

    //Striaght foward logic here, just need implementation
    public void Attack()
    {
        Player.Instance.life -= damage;
    }

    public void DetectPlayer()
    {

    }
}
