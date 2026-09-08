using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private static readonly int IsExploded = Animator.StringToHash("IsExploded");
    [SerializeField] private float _moveSpeed = 1f;

    private Animator _animator;

    private bool _isExploded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        CheckExplosion();
    }

    private void Move()
    {
        if (_isExploded) return;

        transform.Translate(Vector3.up * (Time.deltaTime * _moveSpeed));
    }

    private void CheckExplosion()
    {
        if (!_isExploded) return;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("BombExplosion") && stateInfo.normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (!other.TryGetComponent(out Enemy enemy))
        {
            Debug.LogError("Enemy Component is missing in Enemy Object");
            return;
        }

        enemy.Release();
        _animator.SetTrigger(IsExploded);
        _isExploded = true;
    }
}