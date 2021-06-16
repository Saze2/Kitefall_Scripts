using UnityEngine;

public class CheckPoint : Interactable
{
    [SerializeField] private Transform RespawnPoint = null;
    public GameObject Light;

    RespawnManager man;
    PlayerStats stats;

    private void Start()
    {
        man = RespawnManager.instance;
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }

    public override void Interact()
    {
        base.Interact();

        man.lastRespawnPoint = RespawnPoint.position;
        stats.SetHealthOnSpawn();

        Light.SetActive(true);

        //reload scene?
    }

}
    
