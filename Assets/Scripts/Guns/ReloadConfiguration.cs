using UnityEngine;

[CreateAssetMenu(fileName = "Ammo configuration", menuName = "Guns/Ammo configuration", order = 3)]
public class ReloadConfiguration : ScriptableObject
{
    public int maxAmmo;
    public int currentAmmo;
    public int magSize;
    public int currentMagAmmo;
    public void Reload()
    {
        int maxAmount = Mathf.Min(magSize, currentAmmo);
        int freeBulletsInMag = magSize - currentMagAmmo;
        int reloadAmount = Mathf.Min(maxAmount, freeBulletsInMag);

        currentMagAmmo += reloadAmount;
        currentAmmo -= reloadAmount;
    }

    public bool CanReload()
    {
        return currentMagAmmo < magSize && currentAmmo != 0;
    }

    public void MaxOutAmmo()
    {
        currentAmmo = maxAmmo;
        currentAmmo = magSize;
    }
}
