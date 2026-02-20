using UnityEngine;

using UnityEngine.InputSystem;
 
public class PlayerControllerExam03 : MonoBehaviour

{

    public float speed;

    public float xRange = 10;

    public GameObject projectilePrefab;
 
    public bool enableAutoFireMode;

    public float autoFireInterval = 1f;
 
    private float horizontalInput;

    private InputAction moveAction;

    private InputAction shootAction;
 
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }
 
    private void OnEnable()
    {
        if (moveAction != null) moveAction.Enable();

        if (shootAction != null)
        {

            shootAction.Enable();

            shootAction.performed += OnShootPerformed;

        }
    }
 
    private void OnDisable()
    {
        if (moveAction != null) moveAction.Disable();
        if (shootAction != null)
        {
            shootAction.performed -= OnShootPerformed;
            shootAction.Disable();
        }
        CancelInvoke(nameof(AutoFire));
    }
 
    // Update is called once per frame

    void Update()
    {

        if (moveAction != null)
        {
            horizontalInput = moveAction.ReadValue<Vector2>().x;
            transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);
        }
 
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

    }
 
    private void OnShootPerformed(InputAction.CallbackContext ctx)

    {

        // Toggle auto-fire mode on space (or whatever key is bound to Shoot)

        enableAutoFireMode = !enableAutoFireMode;
        if (enableAutoFireMode)
        {
            // Start repeating once, interval uses autoFireInterval (clamped to a sensible minimum)
            float interval = Mathf.Max(0.1f, autoFireInterval);
            InvokeRepeating(nameof(AutoFire), 0f, interval);
        }

        else
        {
            // Stop auto-fire
            CancelInvoke(nameof(AutoFire));
        }
    }
 
    void AutoFire()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
