using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    [SerializeField] GameObject movingObject;
    [SerializeField] private GameObject[] _pathPoints;
    private int _numberOfPoints;
    public int movementSpeed = 5;

    private Vector3 _actualPosition;
    private int _x;

    private void Start()
    {
        _numberOfPoints = _pathPoints.Length;
        _x = 0;
    }

    private void Update()
    {
        _actualPosition = movingObject.transform.position;
        movingObject.transform.position = Vector3.MoveTowards(_actualPosition, _pathPoints[_x].transform.position, movementSpeed*Time.deltaTime);

        if (_actualPosition == _pathPoints[_x].transform.position && _x != _numberOfPoints -1)
        {
            _x++;
        }
    }
}
