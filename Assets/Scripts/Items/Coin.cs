using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    [SerializeField] private IntVariable coinCount;
    public override void Collect()
    {
        coinCount.ApplyChange(1);
    }
}
