using System.Collections;
using UnityEngine;

public class MoveCommand
{
    private GameObject _player;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _moveTime;

    public MoveCommand(GameObject player, Vector3 startPosition, Vector3 endPosition, float moveTime)
    {
        _player = player;
        _startPosition = startPosition;
        _endPosition = endPosition;
        _moveTime = moveTime;
    }

    public IEnumerator Execute()
    {
        _player.transform.position = _startPosition;
        float elapsedTime = 0f;
        
        while (elapsedTime <= _moveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _moveTime);
            _player.transform.position = Vector3.Lerp(_startPosition, _endPosition, t);
            
            yield return null;
        }
        
        _player.transform.position = _endPosition;
    }
}