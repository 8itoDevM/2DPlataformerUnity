using UnityEngine;

public class TurnWeapon : MonoBehaviour
{
    public void Turn()
    {
        if (CanTurn() && (Player.Instance.move_input.x > 0 && transform.localPosition.x < 0 || Player.Instance.move_input.x < 0 && transform.localPosition.x > 0))
            transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.y);
    }

    bool CanTurn()
    {
        if (!Player.Instance.atk_script.canAtack)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
