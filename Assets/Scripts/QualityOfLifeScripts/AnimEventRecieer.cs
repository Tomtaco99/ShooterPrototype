using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventRecieer : MonoBehaviour
{
    [SerializeField]
    private FPSController _fpsController;

    public void EndReload()
    {
        _fpsController.EndReload();
    }
    
}
