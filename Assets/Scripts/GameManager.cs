using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager InstanceGameManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (InstanceGameManager != null && InstanceGameManager != this)
        {
            Destroy(this);
        }
        else
        {
            InstanceGameManager = this;
        }
    }

    #endregion

}

