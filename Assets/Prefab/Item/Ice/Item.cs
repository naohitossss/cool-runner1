using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    public ItemData itemData;

    public float rotationSpeed = 50f;  // ��]���x

    void Update()
    {
        // Y���𒆐S�ɉ�]
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    public ItemData GetitemData (){
        return itemData;
    }



}
