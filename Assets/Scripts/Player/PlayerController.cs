using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    private PlayerControls controls;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public int maxHealth = 5;
    private int currentHealth;

    public Image healthBar;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveDirection = Vector2.zero;

        controls.Player.Aim.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();

        controls.Player.Fire.performed += ctx => weapon.Fire();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Lose();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void Lose()
    {
        Debug.Log("Player Lost");
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = moveDirection * moveSpeed;

        // Convert mouse screen position to world position
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Rotation
        Vector2 aimDirection = worldMousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
