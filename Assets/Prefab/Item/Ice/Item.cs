using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    public ItemData itemData;

    public float rotationSpeed = 50f;  // ‰ñ“]‘¬“x

    void Update()
    {
        // Y²‚ğ’†S‚É‰ñ“]
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    public ItemData GetitemData (){
        return itemData;
    }



}
