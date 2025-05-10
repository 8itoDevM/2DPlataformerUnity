using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LastTgManager lastTgManager;

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

    private void Start()
    {
        lastTgManager = GetComponent<LastTgManager>();
    }

    // Load a new scene and teleports you near to the correct trigger
    public void ChangeSceneAndTeleport(string go_to)
    {
        SceneManager.LoadScene(go_to);

        Vector3 teleportPos;

        if (lastTgManager.pendingTeleportPosition != null)
        {
            teleportPos = lastTgManager.pendingTeleportPosition.Value;
        }
        else
        {
            teleportPos = lastTgManager.lastTriggerPosition;
        }

        float direction = -Mathf.Sign(teleportPos.x);
        Vector3 newPos = new Vector3(teleportPos.x + direction, teleportPos.y, teleportPos.z);

        Player.Instance.transform.position = newPos;

        lastTgManager.pendingTeleportPosition = null; 
    }
}

