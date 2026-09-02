using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    
    private void Fire()
    {
        GameObject bullet = Instantiate(BulletPrefab);
        bullet.transform.position = FirePoint.position;
    }
}