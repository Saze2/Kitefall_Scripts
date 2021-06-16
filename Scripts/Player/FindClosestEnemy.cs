using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemy : MonoBehaviour
{
	public Transform targetEnemy;
	private Enemy[] allEnemies;

	public void FindClosestEnemyToPlayer(Vector3 hitPoint)
	{
		if (hitPoint == null) return;
		float distanceToClosestEnemy = Mathf.Infinity;
		Enemy closestEnemy = null;
		allEnemies = GameObject.FindObjectsOfType<Enemy>();

		foreach (Enemy currentEnemy in allEnemies)
		{
			float distanceToEnemy = (currentEnemy.transform.position - hitPoint).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestEnemy)
			{
				distanceToClosestEnemy = distanceToEnemy;
				closestEnemy = currentEnemy;

			}
		}
		targetEnemy = closestEnemy.transform;
	}

}

