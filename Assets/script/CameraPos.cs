using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SmartCamera : MonoBehaviour
{
    [Header("要跟随的人物")]
    public Transform target = null;

    [Header("鼠标滑动速度")]
    [Range(0, 1)]
    public float linearSpeed = 1;
    [Header("摄像机与玩家距离")]
    [Range(15, 20)]
    public float distanceFromTarget = 5;
    [Header("摄像机速度")]
    [Range(1, 50)]
    public float speed = 5;
    [Header("x轴偏向量")]
    public float xOffset = 0.5f;

    private float yMouse;
    private float xMouse;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            gameObject.layer = target.gameObject.layer = 2;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            xMouse += Input.GetAxis("Mouse X") * linearSpeed;
            yMouse -= Input.GetAxis("Mouse Y") * linearSpeed;
            yMouse = Mathf.Clamp(yMouse, -30, 80);//限制垂直方向的角度

            distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel") * 10;//拉近或拉远人物镜头
            distanceFromTarget = Mathf.Clamp(distanceFromTarget, 15, 20);

            // 用户操作的鼠标旋转和相机旋转的切换
            Quaternion targetRotation = Quaternion.Euler(yMouse, xMouse, 0);

            // 相机移动的目标位置
            CamCheck(out RaycastHit hit, out float dis);
            Vector3 targetPosition = target.position + targetRotation * new Vector3(xOffset, 0, -dis) + target.GetComponent<CharacterController>().center * 1.75f;

            // 对速度进行插值，使之更有冲刺感
            //speed = target.GetComponent<Rigidbody>().velocity.magnitude > 0.1f ?
            //    Mathf.Lerp(speed, 7, 5f * Time.deltaTime) :
            //    Mathf.Lerp(speed, 25, 5f * Time.deltaTime);

            // 使用Lerp插值，实现相机的跟随
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 25f);
        }
    }

    private void CamCheck(out RaycastHit raycast, out float dis)
    {
        // 用来检测碰撞
#if UNITY_EDITOR
        Debug.DrawLine(target.position + target.GetComponent<CharacterController>().center * 1.75f,
            target.position + target.GetComponent<CharacterController>().center * 1.75f +
            (transform.position - target.position - target.GetComponent<CharacterController>().center * 1.75f).normalized * distanceFromTarget,
            Color.blue);
#endif

        // 如果碰撞到物体，获取碰撞点信息，重新计算距离，否则返回默认值
        if (Physics.Raycast(target.position + target.GetComponent<CharacterController>().center * 1.75f,
            (transform.position - target.position - target.GetComponent<CharacterController>().center * 1.75f).normalized,
            out raycast, distanceFromTarget, ~Physics.IgnoreRaycastLayer))
        {
            dis = Vector3.Distance(target.position + target.GetComponent<CharacterController>().center * 1.75f + new Vector3(xOffset, 0, 0), raycast.point);
        }
        else
        {
            dis = distanceFromTarget;
        }
    }

    public void CursorArise()//cursor可视不可视
    {
        if (Input.GetKeyUp(KeyCode.Escape) && Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        
    }
}
