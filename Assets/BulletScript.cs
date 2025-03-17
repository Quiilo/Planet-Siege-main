using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bullet_Damage = 10f;
    [SerializeField] float lifetime = 3f; // Bullet disappears after this time
    [SerializeField] float force = 20f;

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;

    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
               
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - transform.position).normalized;
        
        rb.linearVelocity = direction * force;

        // Rotate the bullet towards the cursor
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        // Destroy bullet after lifetime if it doesn't hit anything
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy_Stats>(out Enemy_Stats enemy))
        {
            enemy.TakeDamage(bullet_Damage);
        }
        Destroy(gameObject);
    }
}
