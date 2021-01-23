using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemContainer : Singleton<ItemContainer>
{
    [SerializeField] private GameObject tilePrefab;

    private List<GameObject> tiles = new List<GameObject>();
    
    public void AddTile(char c)
    {
        GameObject tile = Instantiate(tilePrefab, transform);
        tiles.Add(tile);
        tile.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = c.ToString();
        LeanTween.value(gameObject, alpha => tile.GetComponent<CanvasGroup>().alpha = alpha, 0f, 1f, .4f)
            .setEaseOutCubic();
        Image im = tile.GetComponent<Image>();
        LeanTween.value(gameObject, color => im.color = color, Color.green, im.color, .4f)
            .setEaseOutCubic();
    }

    public GameObject GetTile(char c)
    {
        foreach (var tile in tiles)
        {
            if (tile.GetComponentInChildren<TMPro.TextMeshProUGUI>().text == c.ToString())
            {
                return tile;
            }
        }

        return null;
    }

    public void RemoveTile(GameObject tile)
    {
        tiles.Remove(tile);
        LeanTween.value(gameObject, alpha => tile.GetComponent<CanvasGroup>().alpha = alpha, 1f, 0f, .5f)
            .setEaseOutCubic().setDelay(.15f);
        Image im = tile.GetComponent<Image>();
        
        LeanTween.value(gameObject, color => im.color = color, Color.green, im.color, .5f)
            .setEaseOutCubic().setDelay(.16f).setOnComplete(_ =>
            {
                tile.transform.parent = null;
                Destroy(tile);
            });
        
        
    }
    
}
