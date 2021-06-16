using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    public Vector3 lastRespawnPoint = Vector3.zero;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
