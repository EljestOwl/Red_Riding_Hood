using Unity.VisualScripting;
using UnityEngine;

public class EnableEnemy : MonoBehaviour
{
	[SerializeField] GameObject _wolf;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_wolf.SetActive(true);
			Destroy(gameObject);
		}
	}
}
