using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Jump() {
        Vector3 move = new Vector3(0f, 100f, 0f);
        rb.AddForce(move);
    }
}
