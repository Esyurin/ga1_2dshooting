using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerFire : MonoBehaviour
{
    private const float MinCoolTime = 0.1f;

    [SerializeField] private Transform _bulletCollection;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Bullet _subBulletPrefab;

    [SerializeField] private Transform _leftFirePoint;
    [SerializeField] private Transform _rightFirePoint;
    [SerializeField] private Transform _leftSubFirePoint;
    [SerializeField] private Transform _rightSubFirePoint;

    [SerializeField] private float _coolTime = 0.3f;
    [SerializeField] private float _attackSpeed = 1f;

    private Bullet[] _bulletPrefabs = { };
    private Dictionary<Bullet, ObjectPool<Bullet>> _bulletPoolMap = new();

    private float _timer = 0f;
    private bool _isAuto = true;

    private void Awake()
    {
        _bulletPrefabs = new[] { _bulletPrefab, _subBulletPrefab };

        foreach (Bullet bullet in _bulletPrefabs)
        {
            ObjectPool<Bullet> enemyPool = new(
                () => SpawnBullet(bullet),
                OnGetBullet,
                OnReleaseBullet,
                OnDestroyBullet,
                true,
                10,
                100);
            _bulletPoolMap.Add(bullet, enemyPool);
        }
    }

    private Bullet SpawnBullet(Bullet bulletPrefab)
    {
        Bullet bullet = Instantiate(bulletPrefab, _leftFirePoint);
        bullet.SetPool(_bulletPoolMap[bulletPrefab]);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _coolTime && (_isAuto || Input.GetKeyDown(KeyCode.Space)))
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
        Bullet leftBullet = _bulletPoolMap[_bulletPrefab].Get();
        leftBullet.transform.position = _leftFirePoint.position;
        leftBullet.transform.parent = _bulletCollection;

        Bullet rightBullet = _bulletPoolMap[_bulletPrefab].Get();
        rightBullet.transform.position = _rightFirePoint.position;
        rightBullet.transform.parent = _bulletCollection;

        Bullet leftSubBullet = _bulletPoolMap[_subBulletPrefab].Get();
        leftSubBullet.transform.position = _leftSubFirePoint.position;
        leftSubBullet.transform.parent = _bulletCollection;

        Bullet rightSubBullet = _bulletPoolMap[_subBulletPrefab].Get();
        rightSubBullet.transform.position = _rightSubFirePoint.position;
        rightSubBullet.transform.parent = _bulletCollection;
    }

    private void ToggleAuto()
    {
        _isAuto = !_isAuto;
    }

    public void AttackSpeedUp(float value)
    {
        _coolTime = Mathf.Max(_coolTime - value, 0.1f);
    }
}