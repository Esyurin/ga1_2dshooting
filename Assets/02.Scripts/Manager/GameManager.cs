using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _player;

    public GameObject Player => _player;
}