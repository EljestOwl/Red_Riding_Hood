using Unity.VisualScripting;
using UnityEngine;

public class ChangeEnemyStatsTrigger : MonoBehaviour
{
	[SerializeField] GameObject _entity;

	[Header("Pathfinding")]
	[SerializeField] Transform _target;

	[Header("Physics")]
	[SerializeField] float _movementSpeed;
	[SerializeField] float _jumpForce;

	[Header("Behavior")]
	[SerializeField] bool _followEnabled = true;
	[SerializeField] bool _JumpEnabled = true;
	[SerializeField] bool _diractionLookEnabled = true;
	[SerializeField] bool _gameObjectEnabled = true;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			// Get script:
			EnemyAI _entityAI = _entity.GetComponent<EnemyAI>();

			// Change the Stats:
			if (_target != null)
			{
				_entityAI.target = _target;
			}
			if (_movementSpeed != 0)
			{
				_entityAI.movementSpeed = _movementSpeed;
			}
			if (_jumpForce != 0)
			{
				_entityAI.jumpForce = _jumpForce;
			}

			// Set entity behavior
			_entityAI.followEnabled = _followEnabled;
			_entityAI.JumpEnabled = _JumpEnabled;
			_entityAI.diractionLookEnabled = _diractionLookEnabled;

			// Check if should Enable Gameobject
			if (!_entity.activeSelf && _gameObjectEnabled)
			{
				_entity.SetActive(true);
			}
			if (_entity.activeSelf && !_gameObjectEnabled)
			{
				_entity.SetActive(false);
			}
		}
	}

}
