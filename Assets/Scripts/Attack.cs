using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Collider2D col;
    public float cooldown;
    public float cooldown_anim;
    public bool canAtack = true;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void Atk()
    {
        if (!canAtack)
        {
            return;
        }
        Player.Instance.animator.SetTrigger("Atk");
        if (!Player.Instance.grounded)
        {
            Player.Instance.rb.linearVelocity = new Vector2(Player.Instance.rb.linearVelocityX / 2, Player.Instance.rb.linearVelocityY / 2);
        }
        else
        {
            Player.Instance.rb.linearVelocity = new Vector2(Player.Instance.move_input.x / 1.5f, 0);
        }
        

        StartCoroutine(CooldownAnim());

        StartCoroutine(CooldownAtk());
    }

    private IEnumerator CooldownAtk()
    {
        canAtack = false;
        yield return new WaitForSeconds(cooldown);
        col.enabled = false;
        canAtack = true;
    }

    private IEnumerator CooldownAnim()
    {

        yield return new WaitForSeconds(cooldown_anim);
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
