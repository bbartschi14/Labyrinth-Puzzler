using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private Vector2 currentGridCoordinate;
    [SerializeField] private Vector2GameEvent onMovedToNewRoom;

    private void Start()
    {
        MoveToNewRoom(currentGridCoordinate);
        onMovedToNewRoom.Raise(currentGridCoordinate);
        
    }

    public void MoveToNewRoom(Vector2 coords)
    {
        currentGridCoordinate = coords;
        Vector3 newPos = new Vector3(coords.x * gridSize.x, 0f, coords.y * gridSize.y);
        LeanTween.move(gameObject, newPos, .35f).setEaseInOutCubic();
        onMovedToNewRoom.Raise(coords);

    }

    public void MoveRelative(Direction dir)
    {
        Vector2 newCoords = currentGridCoordinate;
        switch (dir)
        {
            case Direction.Up:
                newCoords += Vector2.up;
                break;
            case Direction.Down:
                newCoords += Vector2.down;
                break;
            case Direction.Left:
                newCoords += Vector2.left;
                break;
            case Direction.Right:
                newCoords += Vector2.right;
                break;
            default:
                break;
        }
        MoveToNewRoom(newCoords);
    }

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}



