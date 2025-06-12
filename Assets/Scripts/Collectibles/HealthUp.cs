using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : CollectibleBase
{
    public int HealAmmount;
    public override void GetCollected()
    {
        PlayerStats.PlayerStatsInstance.Heal(HealAmmount);
        Destroy(gameObject);
    }
}
