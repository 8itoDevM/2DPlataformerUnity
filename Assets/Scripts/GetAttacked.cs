using Unity.VisualScripting;
using UnityEngine;

public class GetAttacked : MonoBehaviour
{
    public virtual void Effect()
    {
        Debug.Log($"{gameObject.name} foi atacado");
    }
}
