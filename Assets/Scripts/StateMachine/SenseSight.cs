using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseSight : SenseBase
{
    [SerializeField] public float viewAngle;
    
    public bool CheckPlayerInFustrum()
    {
        return false;
    }

}
