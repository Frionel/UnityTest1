using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private enum Direction
    {
        Down,
        Left,
        Right,
        Up
    }

    public GameObject shadow;
    public string _movements;
    public float _stepSize = 2.0f;
    public float _stepSpeed = 1.0f;
    public float _stepDelay = 0.5f;
    public float _pingPongDelay = 1.0f;

    private List<Direction> _directions = new List<Direction>();
    private int _currentDirectionIdx = 0;
    private bool _reversing = false;
    private Vector3 _lastPos = Vector3.zero;

    // Use this for initialization
    void Start () {
        GameObject shadowInstance = Instantiate(shadow);
        shadowInstance.name = "Shadow_Platform";

        ShadowMovement script = shadowInstance.GetComponent<ShadowMovement>();
        script._gameObject = gameObject;
        script._maxScale = 2.5f;

        if (_movements.Length != 0)
        {
            initDirections();
            nextMovement();
        }
    }

    private void initDirections()
    {
        foreach(string dirString in _movements.Split(','))
        {
            if(dirString == "d")
            {
                _directions.Add(Direction.Down);
            }
            else if(dirString == "l")
            {
                _directions.Add(Direction.Left);
            }
            else if(dirString == "r")
            {
                _directions.Add(Direction.Right);
            }
            else if(dirString == "u")
            {
                _directions.Add(Direction.Up);
            }
            else
            {
                Debug.Assert(false, "Incorrect direction");
            }
        }
    }
	
    private void nextMovement()
    {
        Direction currentDirection = _directions[_currentDirectionIdx];
        currentDirection = _reversing ? GetOpposedDirection(currentDirection) : currentDirection;
        Vector3 mov = GetMovementVector(currentDirection);

        _currentDirectionIdx = _reversing ? _currentDirectionIdx - 1 : _currentDirectionIdx + 1;
        bool isLastMove = (_reversing && _currentDirectionIdx == 0) || (!_reversing && _currentDirectionIdx == _directions.Count - 1);

        if (_currentDirectionIdx == _directions.Count)
        {
            _currentDirectionIdx = _directions.Count - 1;
            _reversing = true;
        }
        else if (_currentDirectionIdx == -1)
        {
            _currentDirectionIdx = 0;
            _reversing = false;
        }

        iTween.MoveBy(gameObject,
            iTween.Hash("x", mov.x,
                "y", mov.y,
                "z", mov.z,
                "time", _stepSpeed,
                "easeType", iTween.EaseType.linear,
                "delay", _stepDelay + (isLastMove ? _pingPongDelay : 0.0f),
                "onComplete", "nextMovement"));
    }

    private Vector3 GetMovementVector(Direction dir)
    {
        float horizontalMultiplier = dir == Direction.Left ? -1.0f : (dir == Direction.Right ? 1.0f : 0.0f);
        float verticalMultiplier = dir == Direction.Down ? -1.0f : (dir == Direction.Up ? 1.0f : 0.0f);

        Vector3 mov = Vector3.zero;
        mov.x = _stepSize * horizontalMultiplier;
        mov.y = _stepSize * verticalMultiplier;        

        return mov;
    }

    private void FixedUpdate()
    {
        _lastPos = transform.position;
    }

    private Direction GetOpposedDirection(Direction dir)
    {
        if (dir == Direction.Left) return Direction.Right;
        if (dir == Direction.Right) return Direction.Left;
        if (dir == Direction.Down) return Direction.Up;

        return Direction.Down;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            return;
        }

        if(_lastPos == Vector3.zero)
        {
            return;
        }

        float offsetMagicNumberWtf = 0.7f; // !!!!!!!!!!!!!!!!
        Vector3 movDiff = transform.position - _lastPos;
        collision.gameObject.transform.position += movDiff* offsetMagicNumberWtf;

        _lastPos = Vector3.zero;
    }
}
