using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bullet bullet))
            Destroy(bullet.gameObject);
    }
}