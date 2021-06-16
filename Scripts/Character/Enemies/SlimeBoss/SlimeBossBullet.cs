using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossBullet : MonoBehaviour
{
    //public GameObject HitEffect;
    [SerializeField] private float speed = 20;
    [SerializeField] private int damage = 50;

    private Transform _player;
    private Vector3 _dir;

    private void Start()
    {
        _player = PlayerManager.instance.player.transform;
        Vector3 offset = new Vector3(0, 0.6f, 0);
        offset = _player.position + offset;       
        _dir = (offset - transform.position).normalized;
    }
    private void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;
        
        transform.Translate(_dir * distanceThisFrame, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Player")
            {
                other.transform.GetComponent<CharacterStats>().TakeDamage(damage);
                HitTarget(other.gameObject.transform);
            }
        }
    }

    public void HitTarget(Transform other)
    {
        Explode(other);
        Destroy(gameObject);
    }

    public void Explode(Transform other)
    {
        //Instantiate(HitEffect, other.position, other.rotation);
    }
}
