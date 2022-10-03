using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int heart;
    float speed = 5f;
    int move = 0;
    float pos1 = -7;
    public Animator animator;
    public GameObject punch;
    public int random;

    private GameObject target;
    public GameObject square;

    private GameObject num1;
    private GameObject num2;

    float dis = 0.35f;

    bool healmode;

    void CastRay()
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
        }
    }

    void setting()
    {
        random = Random.Range(1, 15);
        nummaker();
    }

    void nummaker()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("number");
        if (random > 9 && random <= 99)
        {
            int a = random / 10;
            int b = random % 10;
            num1 = Instantiate(square, new Vector2(transform.position.x - dis, transform.position.y + 2), transform.rotation);
            num2 = Instantiate(square, new Vector2(transform.position.x + dis, transform.position.y + 2), transform.rotation);
            SpriteRenderer spriteA = num1.GetComponent<SpriteRenderer>();
            spriteA.sprite = sprites[a];
            SpriteRenderer spriteB = num2.GetComponent<SpriteRenderer>();
            spriteB.sprite = sprites[b];
        }
        else if (random > 0 && random <= 9)
        {
            num1 = Instantiate(square, new Vector2(transform.position.x, transform.position.y + 2), transform.rotation);
            SpriteRenderer spriteR = num1.GetComponent<SpriteRenderer>();
            spriteR.sprite = sprites[random];
        }
    }

    void Start()
    {
        healmode = false;
        heart = 100;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position.x - pos1 <= 7 && move == 1)
            Run();

        if (transform.position.x >= -7 && move == 2)
            Re();

        if (heart == 0)
        {
            Destroy(gameObject);
            GameObject.Find("Stage").GetComponent<Stage>().Fail();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CastRay();

            if (target == this.gameObject && healmode == false)
            {
                healmode = true;
                setting();
            }
            else if (target == this.gameObject && healmode == true)
            {
                if (punch.GetComponent<PunchScript>().result == random)
                {
                    heart += random;
                    if (random > 9 && random <= 99)
                    {
                        Destroy(num1);
                        Destroy(num2);
                    }
                    else if (random > 0 && random <= 9)
                    {
                        Destroy(num1);
                    }
                    random = 0;
                    punch.GetComponent<PunchScript>().re();
                }
                else
                {
                    Debug.Log("다시");
                    if (random > 9 && random <= 99)
                    {
                        Destroy(num1);
                        Destroy(num2);
                    }
                    else if (random > 0 && random <= 9)
                    {
                        Destroy(num1);
                    }
                    random = 0;
                    punch.GetComponent<PunchScript>().re();
                }
                healmode = false;
            }
        }
    }

    void Run()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        animator.SetBool("fight", true);
    }

    void Re()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        animator.SetBool("fight", true);
    }

    public void Inv1()
    {
        move = 1;
        Invoke("Inv2", 5f);
    }

    public void Inv2()
    {
        move = 2;
        Invoke("Inv3", 5f);
    }

    public void Inv3()
    {
        GameObject.Find("Stage").GetComponent<Stage>().StageMove();
        animator.SetBool("fight", false);
    }

    public void Attack1()
    {
        heart -= 0;
    }

    public void Attack2()
    {
        heart -= 0;
    }

    public void Attack3()
    {
        heart -= 0;
    }
}

