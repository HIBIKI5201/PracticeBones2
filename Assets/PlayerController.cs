using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̃R���|�[�l���g")]
    [SerializeField] private Rigidbody2D playerRB;

    [Header("�v���C���[�̃X�e�[�^�X")]
    [Tooltip("�v���C���[�̗̑�")]
    [SerializeField] private float _playerHealth;
    public static float Health;
    [Tooltip("�v���C���[�̈ړ��X�s�[�h")]
    [SerializeField] private float _moveSpeed;

    [Header("�͈͊O�ݒ�")]
    [Tooltip("�ʍs�~�߃^�C����TileMap")]
    public Tilemap tilemap;
    private bool _fieldOut;
    void Start()
    {
        Health = _playerHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health--;
            Debug.Log("�v���C���[���_���[�W���󂯂� \n �c��̗́F" + Health);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 hitPosition = collision.ClosestPoint(transform.position);

        Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile != null)
        {
            // ����̃^�C���ɑ΂��鏈��
            if (tile.name == "EnemySpawn") // �^�C���̖��O�Ŕ���
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
