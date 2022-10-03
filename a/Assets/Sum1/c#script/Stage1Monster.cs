using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Monster : MonoBehaviour
{
    Rigidbody2D rigid;

    int heart = 3; //임의 체력 변수
    bool move = true; //움직임 변수
    public int random;
    public GameObject punch;
    public Animator animator;

    private GameObject target;
    public GameObject square;
    public GameObject circle;
    private GameObject hpbar1;
    private GameObject hpbar2;
    private GameObject hpbar3;

    float dis1 = 0.5f;

    private GameObject num1;
    private GameObject num2;

    float dis = 0.35f;

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

    void Start()
    {
        setting();

        hpbar1 = Instantiate(circle, new Vector2(transform.position.x - dis1, transform.position.y + 2), transform.rotation);
        hpbar2 = Instantiate(circle, new Vector2(transform.position.x, transform.position.y + 2), transform.rotation);
        hpbar3 = Instantiate(circle, new Vector2(transform.position.x + dis1, transform.position.y + 2), transform.rotation);
    }

    void setting()
    {
        if (heart > 0)
        {
            random = Random.Range(1, 15);
            nummaker();
        }
    }

    void nummaker()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("number");
        if (random > 9 && random <= 99)
        {
            int a = random / 10;
            int b = random % 10;
            num1 = Instantiate(square, transform.position, transform.rotation);
            num2 = Instantiate(square, transform.position, transform.rotation);
            SpriteRenderer spriteA = num1.GetComponent<SpriteRenderer>();
            spriteA.sprite = sprites[a];
            SpriteRenderer spriteB = num2.GetComponent<SpriteRenderer>();
            spriteB.sprite = sprites[b];
        }
        else if (random > 0 && random <= 9)
        {
            num1 = Instantiate(square, transform.position, transform.rotation);
            SpriteRenderer spriteR = num1.GetComponent<SpriteRenderer>();
            spriteR.sprite = sprites[random];
        }
    }

    void Awake() //start()보다 먼저 호출
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() //리자드바디용 업데이트함수
    {
        if (move == true)
            rigid.velocity = new Vector2(-1, rigid.velocity.y); //몬스터 자체에게 속도 줌
    }

    public void Update() //주인공에게 공격받았을 때
    {
        if (heart == 0)
        {
            Destroy(gameObject);
            if (random > 9 && random <= 99)
            {
                Destroy(num1);
                Destroy(num2);
                Destroy(hpbar1);
                Destroy(hpbar2);
                Destroy(hpbar3);
            }
            else if (random > 0 && random <= 9)
            {
                Destroy(num1);
                Destroy(hpbar1);
                Destroy(hpbar2);
                Destroy(hpbar3);
            }
            GameObject.Find("Stage").GetComponent<Stage>().Remain();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CastRay();

            if (target == this.gameObject)
            {
                if (punch.GetComponent<PunchScript>().result == random)
                {
                    OnDamaged();
                    setting();
                    heart--;
                    Sprite[] sprites = Resources.LoadAll<Sprite>("number");
                    if (heart == 2)
                    {
                        SpriteRenderer sprite1 = hpbar3.GetComponent<SpriteRenderer>();
                        sprite1.sprite = sprites[10];
                    }
                    else if (heart == 1)
                    {
                        SpriteRenderer sprite2 = hpbar2.GetComponent<SpriteRenderer>();
                        sprite2.sprite = sprites[10];
                    }
                    else if (heart == 0)
                    {
                        SpriteRenderer sprite1 = hpbar1.GetComponent<SpriteRenderer>();
                        sprite1.sprite = sprites[10];
                    }
                    animator.SetFloat("hit", heart);
                    punch.GetComponent<PunchScript>().re();
                    animator.SetFloat("hit", heart);
                    punch.GetComponent<PunchScript>().re();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamaged();
            setting();
            heart--;
            Sprite[] sprites = Resources.LoadAll<Sprite>("number");
            if (heart == 2)
            {
                SpriteRenderer sprite3 = hpbar1.GetComponent<SpriteRenderer>();
                sprite3.sprite = sprites[10];
            }
            else if (heart == 1)
            {
                SpriteRenderer sprite2 = hpbar2.GetComponent<SpriteRenderer>();
                sprite2.sprite = sprites[10];
            }
            else if (heart == 0)
            {
                SpriteRenderer sprite1 = hpbar2.GetComponent<SpriteRenderer>();
                sprite1.sprite = sprites[10];
            }
            animator.SetFloat("hit", heart);
            punch.GetComponent<PunchScript>().re();
        }

        if (random > 9)
        {
            Vector2 pos1 = new Vector2(transform.position.x - dis, transform.position.y + 4);
            Vector2 pos2 = new Vector2(transform.position.x + dis, transform.position.y + 4);
            num1.transform.position = pos1;
            num2.transform.position = pos2;
        }
        else if (random <= 9)
        {
            Vector2 pos1 = new Vector2(transform.position.x, transform.position.y + 4);
            num1.transform.position = pos1;
        }

        hpbar1.transform.position = new Vector2(transform.position.x - dis1, transform.position.y + 2);
        hpbar2.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        hpbar3.transform.position = new Vector2(transform.position.x + dis1, transform.position.y + 2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Attack();
        }
    }

    void OnDamaged() //피격
    {
        if (random > 9 && random <= 99)
        {
            Destroy(num1);
            Destroy(num2);
        }
        else if (random > 0 && random <= 9)
        {
            Destroy(num1);
        }

        CancelInvoke("Stop");
        move = false;
        rigid.velocity = Vector2.zero;
        Vector2 JumpVelocity = new Vector2(5, 3);
        rigid.AddForce(JumpVelocity, ForceMode2D.Impulse);
        punch.GetComponent<PunchScript>().pun = 0;

        Invoke("Stop", 2f); //2초 스턴
    }

    void Attack() //공격(몸빵)
    {
        move = false;
        rigid.velocity = Vector2.zero;
        Vector2 JumpVelocity = new Vector2(4, 3);
        rigid.AddForce(JumpVelocity, ForceMode2D.Impulse);
        GameObject.Find("Player").GetComponent<PlayerScript>().Attack1();

        Invoke("Stop", 1f); // 1초 스턴
    }

    void Stop() //스턴 함수
    {
        move = true;
    }
}