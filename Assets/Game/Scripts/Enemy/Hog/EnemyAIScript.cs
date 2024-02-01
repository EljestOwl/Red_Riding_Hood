using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _activeDistance = 50f;
    [SerializeField] private float _pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _nextWaypointDistance = 3f;
    [SerializeField] private float _jumpNodeHightRequirement = 0.8f;
    [SerializeField] private float _jumpForce = 0.3f;
    [SerializeField] private float _JumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    [SerializeField] private bool _followEnabled;
    [SerializeField] private bool _JumpEnabled;
    [SerializeField] private bool _diractionLookEnabled;

    private Path _path;
    private int _currentWaypoint = 0;
    bool isGrounded = false;
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
        if (TargetInDistance() && _followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (TargetInDistance() && _followEnabled && _seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
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

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + _JumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction Calculation
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction*_movementSpeed*Time.deltaTime;

        // Jump
        if (_JumpEnabled && isGrounded)
        {
            if (direction.y > _jumpNodeHightRequirement)
            {
                _rb.AddForce(Vector2.up * _jumpForce);
            }
        }

        // Movement
        _rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < _nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        // Directional Graphics Handeling
        if (_diractionLookEnabled)
        {
            if (_rb.velocity.x > 0.1f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (_rb.velocity.x < -0.1f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, _target.transform.position) < _activeDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

}
