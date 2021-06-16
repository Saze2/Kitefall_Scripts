using UnityEngine;

public class Arrow : MonoBehaviour
{
	CharacterCombat combat;
	Transform player;
	private Transform target;
	[SerializeField] private float speed = 20;
	public GameObject HitEffect;

	private void Start()
    {
		player = PlayerManager.instance.player.transform;
		combat = player.GetComponent<CharacterCombat>();

		target = combat.attackTarget;
	}

    void Update()
    {
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget( target);
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

    public void HitTarget(Transform Explosiontarget)
    {
		Explode(Explosiontarget);
		combat.DoDamage(Explosiontarget);
		Destroy(gameObject);
	}

	public void Explode(Transform ExplosionTarget)
    {
		Instantiate(HitEffect, ExplosionTarget.position, ExplosionTarget.rotation);
	}
}
