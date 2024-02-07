using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
	[Header("Pathfinding")]
	public Transform target;
	[SerializeField] private float _activeDistance = 50f;
	[SerializeField] private float _pathUpdateSeconds = 0.5f;

	[Header("Physics")]
	[SerializeField] private Transform _groundCheckTransform;
	[SerializeField] private Vector2 _groundCheckSize;
	[SerializeField] private LayerMask _groundLayerMask;
	public float movementSpeed = 7.5f;
	[SerializeField] private float _nextWaypointDistance = 3f;
	[SerializeField] private float _jumpNodeHightRequirement = 0.6f;
	public float jumpForce = 10;

	[Header("Custom Behavior")]
	public bool followEnabled;
	public bool JumpEnabled;
	public bool diractionLookEnabled;

	private Path _path;
	private Vector2 _direction;
	private int _facingDirection = 1;
	private int _currentWaypoint = 0;
	Seeker _seeker;
	Rigidbody2D _rb;

	private void Start()
	{
		_seeker = GetComponent<Seeker>();
		_rb = GetComponent<Rigidbody2D>();

		InvokeRepeating("UpdatePath", 0f, _pathUpdateSeconds);
	}

	private void FixedUpdate()
	{
		if (TargetInDistance() && followEnabled)
		{
			CheckIfGrounded();
			CalculateDirection();
			Movement();
			Jump();
			PathFollow();
			CheckIfShouldFlip();
		}
	}

	private void UpdatePath()
	{
		if (TargetInDistance() && followEnabled && _seeker.IsDone())
		{
			_seeker.StartPath(_rb.position, target.position, OnPathComplete);
		}
	}
	private void CalculateDirection()
	{
		_direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
	}

	private void Movement()
	{
		_rb.velocity = new Vector2(_facingDirection * movementSpeed, _rb.velocity.y);
	}

	private void Jump()
	{
		if (JumpEnabled && CheckIfGrounded())
		{
			if (_direction.y > _jumpNodeHightRequirement)
			{
				_rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
			}
		}
	}

	private void PathFollow()
	{
		if (_path == null)
		{
			return;
		}

		// Reached end of path
		if (_currentWaypoint >= _path.vectorPath.Count)
		{
			return;
		}

		// Next Waypoint
		float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
		if (distance < _nextWaypointDistance)
		{
			_currentWaypoint++;
		}
	}

	private void CheckIfShouldFlip()
	{
		if (diractionLookEnabled)
		{
			if (_facingDirection == 1 && _direction.x < -0.1f)
			{
				Flip();
			}
			else if (_facingDirection == -1 && _direction.x > 0.1f)
			{
				Flip();
			}
		}

	}

	private void Flip()
	{
		_facingDirection *= -1;
		transform.Rotate(0.0f, 180.0f, 0.0f);
	}

	private bool TargetInDistance()
	{
		return Vector2.Distance(transform.position, target.transform.position) < _activeDistance;
	}

	private void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			_path = p;
			_currentWaypoint = 0;
		}
	}

	private bool CheckIfGrounded()
	{
		return Physics2D.OverlapBox(_groundCheckTransform.position, _groundCheckSize, 0, _groundLayerMask);
	}

	private void OnDrawGizmos()
	{
		// Draw GroundCheck:
		Gizmos.DrawWireCube(_groundCheckTransform.position, new Vector3(_groundCheckSize.x, _groundCheckSize.y, 0));
	}

}