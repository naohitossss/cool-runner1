using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink: MonoBehaviour
{
    public ItemData itemData;

    public float rotationSpeed = 50f;  // ��]���x

    public bool ifMax = false;

    void Update()
    {
        // Y���𒆐S�ɉ�]
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !ifMax)
        {
            Destroy(gameObject);
            other.GetComponent<HeatStroke>().currentStroke -= itemData.value;


        }

    }
}