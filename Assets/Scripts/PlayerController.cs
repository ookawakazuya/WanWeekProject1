using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]   
public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] float moveSpeed = 5f;      //移動速度
    [SerializeField] float jumpForce = 5f;      //ジャンプ力
    [SerializeField] float gravity = -9.8f;     //重力加速度


    [Header("視点設定")]
    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0,1.6f,0);

    [Header("その他の設定")]
    [SerializeField] float fallLimitY = -5f;    //落下判定の高さ

    CharacterController controller;
    Vector3 velocity;                           //Y方向の速度
    float xRotation = 0f;                       //上下方向の回転
    bool isGrounded;

    StageManager stageManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stageManager = FindObjectOfType<StageManager>();
    }

    void Update()
    {
        // クリア or　失敗時は動かさない 
        if (stageManager == null || stageManager.CurrentState != StageState.Playing) return;

        HandleMovement();
        UpdateCameraPosition();
        CheckFall();

        // 落下判定
        if (transform.position.y < fallLimitY)
        {
            stageManager.OnPlayerFailed();
        }
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    void HandleMovement() 
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        //入力処理
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //カメラの向きの基準を設定
        Vector3 move = transform.right * x + transform.forward * z;

        //実際の移動処理
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    /// <summary>
    /// カメラをプレイヤーに追従
    /// </summary>
    void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.rotation = transform.rotation; // 視点はプレイヤー方向に固定
        }
    }


    /// <summary>
    /// 落下判定
    /// </summary>
    void CheckFall()
    {
        if(transform.position.y < fallLimitY)
        {
            stageManager.OnPlayerFailed();
        }
    }

    /// <summary>
    /// ゴール判定
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            stageManager.OnStageCleared();
        }
    }

}
