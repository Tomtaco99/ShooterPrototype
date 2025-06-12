using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : CollectibleBase
{
    public override void GetCollected()
    {
        FPSController._instance.AddAmmo(2);
        Destroy(gameObject);
    }
}
