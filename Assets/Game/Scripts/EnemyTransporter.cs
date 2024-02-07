using UnityEngine;

public class EnemyTransporter : MonoBehaviour
{
	[SerializeField] private GameObject _enemy;
	[SerializeField] private Transform _teleportToPoint;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			_enemy.SetActive(false);
			_enemy.transform.position = _teleportToPoint.position;
			_enemy.SetActive(true);
		}
	}
}
