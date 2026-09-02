using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _bulletYPositionMax = 6f;
    
    private void Update()
    {
        Move();        

        if (transform.position.y > _bulletYPositionMax)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(_bulletSpeed * Time.deltaTime * Vector3.up);
    }
}
