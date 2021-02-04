using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color visitedColor;

    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private List<Vector2Int> activeCells;
    [SerializeField] private List<Vector2Int> visitedCells;

    private Vector2Int activeCell;
    
    
    void Start()
    {
        SetupMap();
    }

    private void SetupMap()
    {
        for (int col = 0; col < mapSize.x; col++)
        {
            for (int row = 0; row < mapSize.y; row++)
            {
                if (activeCells.Contains(new Vector2Int(row, col)))
                {
                    transform.GetChild(row).GetChild(col).GetComponent<Image>().enabled = true;
                    transform.GetChild(row).GetChild(col).GetComponent<Image>().color = normalColor;
                } else
                {
                    transform.GetChild(row).GetChild(col).GetComponent<Image>().enabled = false;                    
                    transform.GetChild(row).GetChild(col).GetComponent<Image>().color = normalColor;
                }
            }
        }
    }

    public void ResetMap()
    {
        SetupMap();
        SetActiveCell(new Vector2(activeCell.y, activeCell.x));
    }

    public void SetActiveCell(Vector2 cell)
    {
        //visitedCells.Add(new Vector2Int((int)cell.x, (int)cell.y));
        transform.GetChild(activeCell.x).GetChild(activeCell.y).GetComponent<Image>().color = visitedColor;
        
        activeCell = new Vector2Int((int) cell.y, (int) cell.x);
        transform.GetChild(activeCell.x).GetChild(activeCell.y).GetComponent<Image>().color = activeColor;
    }
    
}
