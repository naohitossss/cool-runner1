using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ItemData itemData;

    public float rotationSpeed = 50f;  // ��]���x

    void Update()
    {
        // Y���𒆐S�ɉ�]
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
