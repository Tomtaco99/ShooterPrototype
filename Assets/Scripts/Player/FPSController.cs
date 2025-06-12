using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class FPSController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.FPSActions _fps;
    private Vector3 _playerVelocity;
    private Rigidbody _rb;
    private bool _jump;
    private float _rotationX;
    private PlayerGunSelector _gunSelector;
    public float speed = 5f;
    public float sprintingSpeed;
    public float aimingSpeed;
    public float jumpForce = 10f;
    public Transform CamPivot;
    public float sensitivityX, sensitivityY;
    public bool autoReload;
    public Animator anim;
    private bool _isReloading;
    private bool _canJump;
    [SerializeField]
    private Transform _feet;
    [SerializeField]
    private Transform gunPivot;
    public static FPSController _instance { get; private set; }
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInput();
        _fps = _playerInput.FPS;
        _rb = GetComponent<Rigidbody>();
        _fps.Jump.started += _=> Jump();
        _jump = false;
        _canJump = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _gunSelector = GetComponent<PlayerGunSelector>();
        _isReloading = false;
        gunPivot = anim.transform;
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        PlayerStats.PlayerStatsInstance.OnDeath += Die;
    }

    private void OnEnable()
    {
        _fps.Enable();
    }

    private void OnDisable()
    {
        _fps.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Move(_fps.Movement.ReadValue<Vector2>(), _rb.velocity.y);
        _gunSelector.SwapWeapon(Mouse.current.scroll.y.value / 120);
        if (Keyboard.current.digit1Key.wasReleasedThisFrame)
            _gunSelector.SwapWeapon(0);
        if (Keyboard.current.digit2Key.wasReleasedThisFrame)
            _gunSelector.SwapWeapon(1);
        if (Keyboard.current.digit3Key.wasReleasedThisFrame)
            _gunSelector.SwapWeapon(2);
        if (Keyboard.current.digit4Key.wasReleasedThisFrame)
            _gunSelector.SwapWeapon(3);
        if (Keyboard.current.digit5Key.wasReleasedThisFrame)
            _gunSelector.SwapWeapon(4);
        if (ShouldReload() || WantsToReload())
        {
            anim.SetTrigger("Reload");
            _audioSource.Play();
            _isReloading = true;
        }
        _gunSelector.currentGun.Tick(!_isReloading && Mouse.current.leftButton.isPressed && _gunSelector.currentGun != null && Application.isFocused);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _playerVelocity;
        Debug.DrawLine(_feet.position, _feet.position + Vector3.down*0.1f, Color.red);
        if (!Physics.Raycast(_feet.position, Vector3.down, 0.1f)) return;
        if (_jump)
        {
            _jump = false;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            AllowJump();
        }
    }

    private void LateUpdate()
    {
        Look(_fps.Aim.ReadValue<Vector2>());
    }

    void Move(Vector2 input, float gravity)
    {
        var dir = Vector3.zero;
        var transform1 = transform;
        dir += transform1.right * input.x;
        dir += transform1.forward * input.y;
        dir.Normalize();
        dir *= Mouse.current.rightButton.isPressed ? aimingSpeed : Keyboard.current.leftShiftKey.isPressed ? sprintingSpeed : speed;
        dir.y = gravity;
        _playerVelocity = dir;
    }

    void Jump()
    {
        if (!_canJump) return;
        _canJump = false;
        _jump = true;
    }

    void Look(Vector2 mouse)
    {
        _rotationX -= (mouse.y * Time.deltaTime) * sensitivityY;
        _rotationX = math.clamp(_rotationX, -80f, 80f);
        CamPivot.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.Rotate(Vector3.up * (mouse.x * Time.deltaTime * sensitivityX));
    }

    public void AllowJump()
    {
        if (!_canJump) _canJump = true;
    }

    bool ShouldReload()
    {
        return !_isReloading && autoReload && _gunSelector.currentGun.reloadConfiguration.currentMagAmmo == 0 &&
               _gunSelector.currentGun.reloadConfiguration.CanReload();
    }

    bool WantsToReload()
    {
        return !_isReloading && Keyboard.current.rKey.wasReleasedThisFrame && _gunSelector.currentGun.reloadConfiguration.CanReload();
    }

    void Reload()
    {
        _gunSelector.currentGun.reloadConfiguration.Reload();
    }

    public void EndReload()
    {
        Reload();
        _isReloading = false;
    }

    public void SwapCancel()
    {
        
    }

    public void Die(Vector3 pos)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    public void AddAmmo(int ammount)
    {
        _gunSelector.currentGun.reloadConfiguration.currentAmmo +=
            _gunSelector.currentGun.reloadConfiguration.magSize * ammount;
    }
}
