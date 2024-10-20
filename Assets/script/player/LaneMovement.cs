using Unity.VisualScripting;
using UnityEngine;

public class LaneMovement : MonoBehaviour
{
    private CharacterController controller;

    // 3つの移動可能な位置（レーン）
    private Vector3[] lanes = new Vector3[3]; // 0: 左, 1: 中央, 2: 右
    private int currentLane = 1;              // 現在のレーン位置（初期は中央）

    public float moveSpeed = 5f;              // 前進速度
    public float laneChangeSpeed = 15f;       // 横移動（レーン変更）の速度
    public float rotationSpeed = 12f;         // 回転速度
    public float runSpeed = 3f;               // スプリント時の速度
    public float knockbackForce =0.1111f;       // ノックバックの強さ
    public float knockbackDuration = 3f;      // ノックバックの持続時間
    private float knockbackTimer = 0f;        // ノックバックの経過時間
    public float energyTimer;                 //エナジーアイテムの持続時間
    private Vector3 knockbackDirection;
    private bool isGrounded;                  // 地面にいるかどうかのフラグ

    public float jumpHeight = 2f;             // ジャンプの高さ
    public float gravity = -9.81f;            // 重力
    private Vector3 velocity;                 // プレイヤーの移動方向と速度

    public LayerMask groundLayer;             // 地面のレイヤーマスク
    public LayerMask obstacleLayer;           // 障害物のレイヤーマスク
    public Transform startPoint;              // 初期位置の基準
    private Animator anim;
    private Vector3 moveDirection;            // 前進の移動方向
    private Vector3 targetLanePosition;       // 移動先のレーン位置

    private enum CharacterState { Normal, Energy ,Knockback, Jumping }
    private CharacterState state = CharacterState.Normal; // キャラクター状態

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Vector3 center = startPoint.position;

        // 初期レーン位置を設定（左、中央、右）
        lanes[0] = new Vector3(center.x - 4.45f, transform.position.y, transform.position.z);
        lanes[1] = new Vector3(center.x, transform.position.y, transform.position.z);
        lanes[2] = new Vector3(center.x + 4.45f, transform.position.y, transform.position.z);

        // 初期位置を中央レーンに設定
        transform.position = lanes[currentLane];
    }

    void Update()
    {
        // 地面判定
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundLayer);

        switch (state)
        {
            case CharacterState.Normal:
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    // ジャンプ開始
                    velocity.y = Mathf.Sqrt(jumpHeight * -10f * gravity);
                    anim.SetTrigger("Jump");
                    state = CharacterState.Jumping;
                }

                // 前進する動作
                moveDirection = Vector3.forward * moveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection *= runSpeed;
                }

                // レーン変更の処理
                HandleLaneChange();

                // Y方向の速度（重力）を適用
                velocity.y += gravity * Time.deltaTime;

                // 全体の移動（前進＋重力）を適用
                controller.Move((moveDirection + velocity) * Time.deltaTime);

                // 移動方向に合わせて回転
                RotateTowardsMovementDirection();

                // アニメーション更新
                UpdateAnim();
                break;
            case CharacterState.Energy:
                energyTimer -= Time.deltaTime;
                if(energyTimer < 0) state = CharacterState.Normal;
                if (CanJump())
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -10f * gravity);
                    anim.SetTrigger("Jump");
                    state = CharacterState.Jumping;
                }
                moveDirection = Vector3.forward * moveSpeed;
                
                moveDirection *= runSpeed;

                HandleLaneChange();

                velocity.y += gravity * Time.deltaTime;

                controller.Move((moveDirection + velocity) * Time.deltaTime);

                RotateTowardsMovementDirection();

                UpdateAnim();
                break;

            case CharacterState.Knockback:
                knockbackTimer -= Time.deltaTime;
                bool afterJump = false;

                if (knockbackTimer > 2.5f)
                {
                    // ノックバック方向に移動
                    controller.Move((knockbackDirection+ velocity) * knockbackForce  *Time.deltaTime);
                }
                else if (knockbackTimer > 0f)
                {
                    // 一時停止
                    controller.Move(Vector3.zero);
                    if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
                    {
                        currentLane--;
                        targetLanePosition = new Vector3(lanes[currentLane].x, transform.position.y, transform.position.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1)
                    {
                        currentLane++;
                        targetLanePosition = new Vector3(lanes[currentLane].x, transform.position.y, transform.position.z);
                    }
                    else if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                        afterJump = true;
                    }

                }
                else
                {
                    // ノックバック終了
                    state = CharacterState.Normal;
                    if(afterJump) state = CharacterState.Jumping;
                }
                break;

            case CharacterState.Jumping:
                if (isGrounded && velocity.y < 0)
                {
                    // ジャンプ終了
                    velocity.y = -2f;
                    state = CharacterState.Normal;
                }

                // Y方向の速度（重力）を適用
                velocity.y += gravity * Time.deltaTime;

                // 全体の移動（前進＋重力）を適用
                controller.Move((moveDirection + velocity) * Time.deltaTime);
                break;
        }
    }

    // レーン変更を処理する関数
    public bool CanJump() {
        bool canJump = false;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            canJump = true;
        }
        return canJump;

    }
    void HandleLaneChange()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
            targetLanePosition = new Vector3(lanes[currentLane].x, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < lanes.Length - 1)
        {
            currentLane++;
            targetLanePosition = new Vector3(lanes[currentLane].x, transform.position.y, transform.position.z);
        }

        // 横方向のみの移動
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetLanePosition, laneChangeSpeed * Time.deltaTime);
        controller.Move(new Vector3(newPosition.x - transform.position.x, 0, 0));
    }

    //エナジーアイテムの機能処理
    public void DrinkEnergy(float time) {
        energyTimer = time;
        state = CharacterState.Energy;
    }

    // 移動方向に基づいてキャラクターを回転させる関数
    void RotateTowardsMovementDirection()
    {
        Vector3 combinedMoveDirection = new Vector3(targetLanePosition.x - transform.position.x, 0, moveDirection.z);
        if (combinedMoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(combinedMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy") || ((1 << hit.gameObject.layer) & obstacleLayer) != 0)
        {
            anim.SetTrigger("Hit");
            knockbackDirection = (transform.position - hit.transform.position).normalized;
            knockbackDirection.y = 0;
            state = CharacterState.Knockback;
            knockbackTimer = knockbackDuration;
        }
    }

    private void UpdateAnim()
    {
        float currentSpeed = controller.velocity.magnitude;
        anim.SetFloat("Speed", currentSpeed);
    }
}



