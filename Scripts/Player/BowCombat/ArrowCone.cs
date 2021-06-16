using UnityEngine;

public class ArrowCone : MonoBehaviour
{
    public GameObject HitEffect;
    [SerializeField] private float speed = 40;
    private int damage = 50;

    private void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(transform.forward * distanceThisFrame, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Enemy")
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
        Instantiate(HitEffect, other.position, other.rotation);
    }
}
