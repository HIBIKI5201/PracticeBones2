using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowTarget : MonoBehaviour
{
    [Header("ターゲット設定")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offset;

    [Header("UIの設定")]
    [SerializeField] private RectTransform rectTransform;

    void Start()
    {

    }

    private void RefreshPosition()
    {
        if (targetTransform)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
            rectTransform.position = screenPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefreshPosition();
    }
}
