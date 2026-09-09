using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffectPrefab;
    [SerializeField] private AudioSource _hitAudioSource;
    [SerializeField] private AudioSource _deathAudioSource;

    [SerializeField] private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
            _deathAudioSource.Play();
            Destroy(gameObject);
        }
    }

    public void Heal(float value)
    {
        _health += value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        _hitAudioSource.Play();
    }
}