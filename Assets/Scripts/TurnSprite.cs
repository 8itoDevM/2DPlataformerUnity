using UnityEngine;

public class TurnSprite : MonoBehaviour
{
    public void TurnScale()
    {
        if (CanTurn() && (Player.Instance.move_input.x > 0 && transform.localScale.x < 0 || Player.Instance.move_input.x < 0 && transform.localScale.x > 0))
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    bool CanTurn()
    {
        if (!Player.Instance.atk_script.canAtack)
        {
            return false;
        }
        else{
            return true;
        }
    }
}
