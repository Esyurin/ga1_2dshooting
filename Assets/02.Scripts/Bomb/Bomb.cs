using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }
}