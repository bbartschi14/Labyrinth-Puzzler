using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameWonPanel;

    public void GameWon()
    {
        gameWonPanel.SetActive(true);
    }
}
