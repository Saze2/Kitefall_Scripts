using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
	//code from brackeys

	public Slider slider;
	//public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		//fill.color = gradient.Evaluate(1f);
	}

	public void SetHealth(int health)
	{
		slider.value = health;

		//fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
