using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform LeftFirePoint;
    public Transform RightFirePoint;

    private float _timer = 0f;
    public float Cooltime = 0.3f;

    private bool isAuto = false;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= Cooltime && (isAuto || Input.GetKeyDown(KeyCode.Space)))
        {
            _timer = 0f;
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleAuto();
        }
    }
    
    private void Fire()
    {
        GameObject leftBullet = Instantiate(BulletPrefab, LeftFirePoint.position, LeftFirePoint.rotation);
        GameObject rightBullet = Instantiate(BulletPrefab, RightFirePoint.position, RightFirePoint.rotation);
    }

    private void ToggleAuto()
    {
        isAuto = !isAuto;
    }
}