using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̃R���|�[�l���g")]
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private SpriteRenderer playerRenderer;

    [Header("UI�ݒ�")]
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image StaminaBar;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [Header("�v���C���[�̃X�e�[�^�X")]
    [Tooltip("�v���C���[�̗̑�")]
    [SerializeField] private float _playerHealth;
    public static float Health;
    [Tooltip("�v���C���[�̈ړ��X�s�[�h")]
    [SerializeField] private float _moveSpeed;
    [Header("Void���[�h�̃X�e�[�^�X")]
    [SerializeField] private float _voidTime;
    [SerializeField] private float _voidCoolTime;
    public float voidCoolTimer = 1;
    [SerializeField] private float _voidMoveSpeed;

    [Header("�v���C���[�̏��")]
    //�v���C���[�̏��
    public PlayerStatus _playerStatus;
    public enum PlayerStatus
    {
        Normal,
        Void
    }
    [Header("�͈͊O�ݒ�")]
    [Tooltip("�ʍs�~�߃^�C����TileMap")]
    public Tilemap tilemap;
    private bool _fieldOut;

    public static int Score;


    void Start()
    {
        Health = _playerHealth;
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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy�Ƃ̐ڐG�����m");
            if (_playerStatus != PlayerStatus.Void)
            {
                Health--;

                Debug.Log($"�v���C���[���_���[�W���󂯂�\n�c��̗́F{Health}");
                HealthBar.fillAmount = Health / _playerHealth;

                if (Health <= 0)
                {
                    GameObject.Find("GameManager").GetComponent<ReStartManager>().GameOver();
                }

                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Star�Ƃ̐ڐG�����m");

            if (_playerStatus != PlayerStatus.Void)
            {
                Score++;
                StarSpawner.StarCount--;

                Debug.Log($"���݂̃X�R�A��{Score}");
                ScoreText.text = $"Score : {Score}";

                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator VoidMode()
    {
        Debug.Log("StartVoidMode");
        
        _playerStatus = PlayerStatus.Void;

        playerRenderer.color = new Color(playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 0.5f);
        HealthBar.color = new Color(HealthBar.color.r, HealthBar.color.g, HealthBar.color.b, 0.5f);
        StaminaBar.color = new Color(StaminaBar.color.r, StaminaBar.color.g, StaminaBar.color.b, 0.5f);

        yield return new WaitForSeconds(_voidTime);

        Debug.Log("EndVoidMode");

        _playerStatus = PlayerStatus.Normal;

        playerRenderer.color = new Color(playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 1f);
        HealthBar.color = new Color(HealthBar.color.r, HealthBar.color.g, HealthBar.color.b, 1f);
        StaminaBar.color = new Color(StaminaBar.color.r, StaminaBar.color.g, StaminaBar.color.b, 1f);

        voidCoolTimer = 0;

        DOTween.To(() => voidCoolTimer, x => voidCoolTimer = x, 1, _voidCoolTime)
                .OnUpdate(() =>
                {
                    StaminaBar.fillAmount = voidCoolTimer;
                    StaminaBar.color = Color.yellow;
                })
                .OnComplete(() =>
                {
                    StaminaBar.color = Color.white;
                    Debug.Log("�N�[���^�C���I��");
                });

        // DOTween����������̂�҂�
        while (voidCoolTimer < 1)
        {
            yield return null;
        }
    }

    void Update()
    {
        if(!ReStartManager.gameOver)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                Vector2 moveAxis = Vector2.zero;

                if (_playerStatus == PlayerStatus.Normal)
                {
                    moveAxis = new Vector2(horizontal * _moveSpeed, vertical * _moveSpeed);
                }
                else if(_playerStatus == PlayerStatus.Void)
                {
                    moveAxis = new Vector2(horizontal * _voidMoveSpeed, vertical * _voidMoveSpeed);
                }

                playerRB.velocity = moveAxis;
                if(_fieldOut)
                {
                    playerRB.velocity = new Vector2(-moveAxis.x, -moveAxis.y);
                }
            }
            else
            {
                playerRB.velocity = Vector2.zero;
            }


            if(Input.GetKeyDown(KeyCode.V) && voidCoolTimer == 1)
            {
                StartCoroutine(VoidMode());
            }
        } else
        {
            playerRB.velocity = new Vector2 (0, 0);
        }
    }
}
