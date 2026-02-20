using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAwareness _playerAwareness;
    private Vector2 _direction;

    public int maxHealth = 3;
    private int currentHealth;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwareness = GetComponent<PlayerAwareness>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwareness.AwareOfPlayer)
        {
            _direction = _playerAwareness.DirectionToPlayer;
        }
        else
        {
            _direction = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_direction == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_direction == Vector2.zero)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            _rigidbody.linearVelocity = transform.up * _speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(1);
        }
    }
}