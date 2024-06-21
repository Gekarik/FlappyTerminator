using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour, IInteractable
{
    private float _bulletSpeed;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _direction * _bulletSpeed;
    }

    public void SpeedInitialize(float bulletSpeed,Vector2 direction)
    {
        _bulletSpeed = bulletSpeed;
        _direction = direction;
    }
}
