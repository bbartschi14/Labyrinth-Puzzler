using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockArea : MonoBehaviour
{
    [SerializeField] private int doorID;
    [SerializeField] private IntGameEvent onPuzzleCorrect;

    [SerializeField] public float gridSize;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private List<Vector2> obstacles = new List<Vector2>();
    [SerializeField] private List<RockObject> rocks = new List<RockObject>();
    [SerializeField] private List<RockGoal> goals = new List<RockGoal>();

    public Vector2 GetNewPosition(Vector2 pos, Vector2 direction)
    {
        if (RockCollision(pos + direction))
        {
            return pos;
        }
        return GetNewPosition(pos + direction, direction);
    }

    public void CheckSolved(RockObject rock)
    {
        foreach (RockGoal goal in goals)
        {
            if (rock.position == goal.position
                && rock.rockID == goal.goalID)
            {
                rock.SetInteractable(false);
                goal.Complete();
            }
        }

        CheckAllSolved();
    }

    public void ResetAll()
    {
        foreach (var rock in rocks)
        {
            rock.ResetRock();
        }
    }

    private void CheckAllSolved()
    {
        foreach (RockGoal goal in goals)
        {
            if (goal.IsSolved()) onPuzzleCorrect.Raise(goal.doorID);;
        }
        
    }

    private bool RockCollision(Vector2 pos)
    {
        if (pos.x >= width || pos.y >= height || pos.x < 0 || pos.y < 0)
        {
            return true;
        } 
        
        if (obstacles.Contains(pos))
        {
            return true;
        }

        foreach (RockObject rock in rocks)
        {
            if (rock.position == pos)
            {
                return true;
            }
        }

        return false;
    }
    
}
