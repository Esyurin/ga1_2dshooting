using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform LeftFirePoint;
    public Transform RightFirePoint;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    
    private void Fire()
    {
        GameObject leftBullet = Instantiate(BulletPrefab, LeftFirePoint.position, LeftFirePoint.rotation);
        GameObject rightBullet = Instantiate(BulletPrefab, RightFirePoint.position, RightFirePoint.rotation);
    }
}