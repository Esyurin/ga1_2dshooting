using System;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;

    private Material _material;
    private float _offsetY;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _offsetY += _scrollSpeed * Time.deltaTime;

        // TODO: MaterialPropertyBlock을 이용한 최적화
        _material.mainTextureOffset = new Vector2(0, _offsetY);
    }
}