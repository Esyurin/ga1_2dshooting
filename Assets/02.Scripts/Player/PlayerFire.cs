using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private const float MinCoolTime = 0.1f;

    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    public Transform LeftFirePoint;
    public Transform RightFirePoint;
    public Transform LeftSubFirePoint;
    public Transform RightSubFirePoint;

    [SerializeField] private float _coolTime = 0.3f;

    private float _timer = 0f;


    [SerializeField] private float _attackSpeed = 1f;

    private bool isAuto = false;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _coolTime && (isAuto || Input.GetKeyDown(KeyCode.Space)))
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

    public void AttackSpeedUp(float value)
    {
        _coolTime = Mathf.Max(_coolTime - value, 0.1f);
    }
}