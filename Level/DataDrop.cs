using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TeddyData>() != null)
        {
            Destroy(other.gameObject);
            PlayerStats.DataScore += 1;
        }
    }
}
