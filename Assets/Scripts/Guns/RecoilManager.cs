using UnityEngine;

public class RecoilManager : MonoBehaviour
{
    public Vector3 _currentRotation;
    public Vector3 _targetRotation;
    [SerializeField]
    private PlayerGunSelector _gunSelector;

    public void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero,
            _gunSelector.currentGun.shootConfiguration.recoilRecoverySpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _gunSelector.currentGun.shootConfiguration.SnapStrength * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }
    
}
