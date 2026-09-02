using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform LeftFirePoint;
    public Transform RightFirePoint;

    private float _timer = 0f;
    public float Cooltime = 0.3f;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= Cooltime && Input.GetKeyDown(KeyCode.Space))
        {
            _timer = 0f;
            Fire();
        }
    }
    
    private void Fire()
    {
        GameObject leftBullet = Instantiate(BulletPrefab, LeftFirePoint.position, LeftFirePoint.rotation);
        GameObject rightBullet = Instantiate(BulletPrefab, RightFirePoint.position, RightFirePoint.rotation);
    }
}