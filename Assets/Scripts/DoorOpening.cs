using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public float timeToClose = 2f;

    bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isOpen == false)
        {
            isOpen = true;
            gameObject.transform.Rotate(0f, 90f, 0f);
            Invoke("CloseDoor", timeToClose);
        }
    }

    void CloseDoor()
    {
        isOpen = false;
        gameObject.transform.Rotate(0f, -90f, 0f);
    }
}
