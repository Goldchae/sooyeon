//몬스터2 둥둥 해보기
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dongdongMonster2 : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("wiw");
        rb.velocity = Vector2.zero;
        Vector2 JumpVelocity = new Vector2(0, 10);
        rb.AddForce(JumpVelocity, ForceMode2D.Impulse );
    }
}
