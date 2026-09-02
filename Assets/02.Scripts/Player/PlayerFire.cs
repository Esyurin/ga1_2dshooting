using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    public Transform LeftFirePoint;
    public Transform RightFirePoint;
    public Transform LeftSubFirePoint;
    public Transform RightSubFirePoint;
    
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
        Instantiate(BulletPrefab, LeftFirePoint.position, LeftFirePoint.rotation);
        Instantiate(BulletPrefab, RightFirePoint.position, RightFirePoint.rotation);
        Instantiate(SubBulletPrefab, LeftSubFirePoint.position, LeftSubFirePoint.rotation);
        Instantiate(SubBulletPrefab, RightSubFirePoint.position, RightSubFirePoint.rotation);
    }

    private void ToggleAuto()
    {
        isAuto = !isAuto;
    }
}