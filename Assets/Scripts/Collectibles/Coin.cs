using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectibleBase
{
    public override void GetCollected()
    {
        PlayerStats.PlayerStatsInstance.CoinCount++;
        Destroy(gameObject);
    }
}
