using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;

public class WordSearchTile : UIBehaviour, IDragHandler
{
    [SerializeField] private TMPro.TextMeshProUGUI tileText;
    [SerializeField] private WordSearchPanel mainPanel;
    public int siblingIndex;
    
    public void SetText(char c)
    {
        tileText.text = c.ToString();
    }
    
    public void SetTextColor(Color color)
    {
        tileText.color = color;
    }

    public void Setup()
    {
        siblingIndex = transform.GetSiblingIndex();

        this.OnBeginDragAsObservable().Subscribe(pointer =>
        {
            mainPanel.SetLine(true);
        }).AddTo(this);
        
        this.OnDragAsObservable().Subscribe(pointer =>
        {
            var hoveringObject = pointer.pointerCurrentRaycast.gameObject;
            if (hoveringObject)
            {
                WordSearchTile tile = hoveringObject.GetComponent<WordSearchTile>();
                //Debug.Log("Dragging over " + hoveringObject.name);
                if (tile)
                {
                    int end = tile.siblingIndex;
                    //Debug.Log(end);
                    mainPanel.UpdateLine(siblingIndex, end);
                }

            }
            
        }).AddTo(this);
        this.OnEndDragAsObservable().Subscribe(pointer =>
        {
            mainPanel.SetLine(false);
            mainPanel.CheckPuzzle();
        }).AddTo(this);

    }
    
    public void OnDrag(PointerEventData eventData)
    {
    }
}