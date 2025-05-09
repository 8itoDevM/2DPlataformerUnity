using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AttackTimming : MonoBehaviour
{
    [SerializeField] Collider2D col;
    [SerializeField] float cooldown;
    [SerializeField] float cooldown_anim;
    bool canAtack = true;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = true;
    }

    public void Atk()
    {
        if (!canAtack)
        {
            return;
        }
        //Player.Instance.animator.SetTrigger("AtkTimming");
        StartCoroutine(CooldownAnim());
        Debug.Log("Ataque timing");
        StartCoroutine(CooldownAtk());
    }

    private IEnumerator CooldownAtk()
    {
        canAtack = false;
        yield return new WaitForSeconds(cooldown);
        Player.Instance.atacking_timing = false;
        canAtack = true;
    }

    private IEnumerator CooldownAnim()
    {
        yield return new WaitForSeconds(cooldown_anim);
        Player.Instance.atacking_timing = true;
    }


}
