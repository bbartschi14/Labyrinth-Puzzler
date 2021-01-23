using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : Singleton<PuzzleGenerator>
{
    [SerializeField] private List<Unscramble> unscramblePuzzles;
    [SerializeField] private List<WordSearch> wordSearchPuzzles;
    
    public Puzzle GeneratePuzzle(PuzzleType type, bool useSpecific, Puzzle specificPuzzle)
    {
        if (useSpecific)
        {
            return Instantiate(specificPuzzle);
        }
        
        switch (type)
        {
            case PuzzleType.Unscramble:
                return Instantiate(unscramblePuzzles[Random.Range(0, unscramblePuzzles.Count)]);
            case PuzzleType.WordSearch:
                return Instantiate(wordSearchPuzzles[Random.Range(0, wordSearchPuzzles.Count)]);
            default:
                return null;
        }
        
        
    }

}
