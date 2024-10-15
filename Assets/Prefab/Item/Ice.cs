using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ItemData itemData;

    public float rotationSpeed = 50f;  // ‰ñ“]‘¬“x

    void Update()
    {
        // Y²‚ğ’†S‚É‰ñ“]
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
            other.GetComponent<HeatStroke>().currentStroke -= itemData.value;


    }

    }
}
