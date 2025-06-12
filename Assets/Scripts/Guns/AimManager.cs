using UnityEngine;
using UnityEngine.InputSystem;

public class AimManager : MonoBehaviour
{
    [SerializeField]
    private PlayerGunSelector _gunSelector;
    public Vector3 initialPosition;

    public void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, _gunSelector.currentGun.AimPosition, _gunSelector.currentGun.AimSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, initialPosition, _gunSelector.currentGun.AimSpeed * Time.deltaTime);
        }
    }
}
