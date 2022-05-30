using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectByCollision : MonoBehaviour
{
    private bool isTriggered;
    [SerializeField] private string nameOfObjectDetect = "";
    [SerializeField] private GameObject ObjectToActive;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        ObjectToActive.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            ObjectToActive.SetActive(true);
        }
        else if(!isTriggered)
        {
            ObjectToActive.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains(nameOfObjectDetect))
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains(nameOfObjectDetect))
        {
            isTriggered = false;
        }
    }
}
