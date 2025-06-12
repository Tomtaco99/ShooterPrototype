using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField]
    private Transform _gunParent;
    [SerializeField]
    private List<WeaponSCO> _guns;
    [SerializeField]
    private Transform _camera;
    
    public WeaponSCO currentGun;
    private int currentIndex;
    private void Start()
    {
        currentIndex = 0;
        WeaponSCO cGun = _guns[currentIndex];
        if (cGun == null) return;
        currentGun = cGun;
        foreach (var gun in _guns)
        {
            gun.reloadConfiguration.currentAmmo = gun.reloadConfiguration.maxAmmo;
            gun.reloadConfiguration.currentMagAmmo = gun.reloadConfiguration.magSize;
            gun.Spawn(_gunParent, this, _camera, _camera.parent.GetComponent<RecoilManager>()).SetActive(false);
        }
        cGun.Enable(true);
    }

    public void SwapWeapon(float yValue)
    {
        if(yValue == 0) return;
        FPSController._instance.SwapCancel();
        currentGun.Enable(false);
        bool Decrement = yValue < 0f;
        for (int i = 0; i < Math.Abs(yValue); i++)
        {
            currentIndex += 1 * (Decrement ? -1 : 1);
            if (currentIndex < 0)
            {
                currentIndex = _guns.Count - 1;
            }
            else if (currentIndex >= _guns.Count)
            {
                currentIndex = 0;
            }
        }
        currentGun = _guns[currentIndex];
        currentGun.Enable(true);
    }

    public void SwapWeapon(int number)
    {
        if(number == currentIndex) return;
        if(number >= _guns.Count) return;
        FPSController._instance.SwapCancel();
        currentIndex = number;
        currentGun.Enable(false);
        currentGun = _guns[currentIndex];
        currentGun.Enable(true);
    }
}
