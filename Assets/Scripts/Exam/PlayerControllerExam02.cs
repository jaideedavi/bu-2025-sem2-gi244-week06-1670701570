using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerControllerExam02 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;
 
    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;
 
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }
 
    // Update is called once per frame
    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;

        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, xRange), transform.position.y, transform.position.z);

        if(shootAction.triggered)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 90, 0));
        }
 
    }
}
