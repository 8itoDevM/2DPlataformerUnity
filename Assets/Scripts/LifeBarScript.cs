using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LifeBarScript : MonoBehaviour
{
    public GameObject heart;
    public List<GameObject> heart_list;

    private void Start()
    {
        heart_list = new List<GameObject>();
        AddHearts();
    }

    public void AddHearts()
    {
        ClearHearts();
        for (int i = 0; i < Player.Instance.life; i++)
        {
            GameObject obj_temp = Instantiate(heart, transform);
            heart_list.Add(obj_temp);
        }
    }

    public void ClearHearts()
    {
        foreach (GameObject heartObj in heart_list)
        {
            Destroy(heartObj);
        }
        heart_list.Clear();
    }

    public void UpdateHearts()
    {
        // Clears all the hearts and add them again according to the player's life
        ClearHearts();
        AddHearts();
    }

}
