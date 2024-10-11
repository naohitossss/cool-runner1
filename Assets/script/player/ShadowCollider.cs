using UnityEngine;
using UnityEngine.UI;

public class ShadowCollider : MonoBehaviour
{
    public Transform lightTarget;
    public bool ifShadow;
    public Text texts;
    void Start()
    {

    }
    void Update()
    {
        Vector3 target = (Quaternion.Euler(lightTarget.eulerAngles) * Vector3.forward).normalized * -1;
        Ray ray = new Ray(transform.position, target);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log("碰撞对象" + hit.collider.name);
            //Debug.DrawLine(ray.origin, hit.point, Color.red);
            ifShadow = true;
        }
        else
        {
            ifShadow = false;
        }
        texts.text = "是否在阴影中：" + ifShadow;
    }
}

