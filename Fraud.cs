using UnityEngine;

public class Fraud : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypoint = 0;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
        transform.LookAt(_waypoints[_currentWaypoint].position);
    }
}


