using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーのコンポーネント")]
    [SerializeField] private Rigidbody2D playerRB;

    [Header("UI設定")]
    [SerializeField] private Image HealthBar;

    [Header("プレイヤーのステータス")]
    [Tooltip("プレイヤーの体力")]
    [SerializeField] private float _playerHealth;
    public static float Health;
    [Tooltip("プレイヤーの移動スピード")]
    [SerializeField] private float _moveSpeed;

    [Header("範囲外設定")]
    [Tooltip("通行止めタイルのTileMap")]
    public Tilemap tilemap;
    private bool _fieldOut;
    void Start()
    {
        Health = _playerHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health--;
            Debug.Log("プレイヤーがダメージを受けた \n 残り体力：" + Health);
            HealthBar.fillAmount = Health / _playerHealth;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 hitPosition = collision.ClosestPoint(transform.position);

        Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile != null)
        {
            // 特定のタイルに対する処理
            if (tile.name == "EnemySpawn") // タイルの名前で判定
            {
                _fieldOut = true;
            }
            else
            {
                _fieldOut = false;
            }
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        float vertical = Input.GetAxisRaw("Vertical") * _moveSpeed;
        playerRB.velocity = new Vector2(horizontal, vertical);
        if(_fieldOut)
        {
            playerRB.velocity = new Vector2(-horizontal, -vertical);
        }
        if(horizontal == 0 && vertical == 0)
        {
            playerRB.velocity = Vector2.zero;
        }
    }
}
