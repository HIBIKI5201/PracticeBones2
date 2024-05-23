using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public static int _playerHealth;
    [SerializeField] float _moveSpeed;

    public Tilemap tilemap;
    private bool _fieldOut;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerHealth--;
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
            } else
            {
                _fieldOut = false;
            }
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;
        transform.position = new Vector2(horizontal + transform.position.x, vertical + transform.position.y);
        if(_fieldOut)
        {
            transform.position = new Vector2(-horizontal + transform.position.x, -vertical + transform.position.y);
        }
    }
}
