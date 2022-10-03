using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonclick : MonoBehaviour
{
    public Button btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn0;
    public GameObject punch;

    [SerializeField]
    private GameObject num1;
    [SerializeField]
    private GameObject num2;
    [SerializeField]
    private GameObject num3;
    [SerializeField]
    private GameObject num4;
    [SerializeField]
    private GameObject num5;
    [SerializeField]
    private GameObject num6;
    [SerializeField]
    private GameObject num7;
    [SerializeField]
    private GameObject num8;
    [SerializeField]
    private GameObject num9;
    [SerializeField]
    private GameObject num0;

    void Start() // 클릭하면
    {
        btn1.onClick.AddListener(() => btnprint(num1));
        btn2.onClick.AddListener(() => btnprint(num2));
        btn3.onClick.AddListener(() => btnprint(num3));
        btn4.onClick.AddListener(() => btnprint(num4));
        btn5.onClick.AddListener(() => btnprint(num5));
        btn6.onClick.AddListener(() => btnprint(num6));
        btn7.onClick.AddListener(() => btnprint(num7));
        btn8.onClick.AddListener(() => btnprint(num8));
        btn9.onClick.AddListener(() => btnprint(num9));
        btn0.onClick.AddListener(() => btnprint(num0));
    }

    //1번 함수
    void btnprint(GameObject Num)
    {
        Debug.Log(Num); // 콘솔에 넘버 찍고
        GameObject number = Instantiate(Num, new Vector3(-3.08f,-3.67f,0), Quaternion.identity); // 숫자이미지생성하고
        StartCoroutine(flying(number));//날아감
        if (Num == num1){
            punch.GetComponent<PunchScript>().num = 1;
        }else if (Num == num2){
            punch.GetComponent<PunchScript>().num = 2;
        }else if (Num == num3){
            punch.GetComponent<PunchScript>().num = 3;
        }else if (Num == num4){
            punch.GetComponent<PunchScript>().num = 4;
        }else if (Num == num5){
            punch.GetComponent<PunchScript>().num = 5;
        }else if (Num == num6){
            punch.GetComponent<PunchScript>().num = 6;
        }else if (Num == num7){
            punch.GetComponent<PunchScript>().num = 7;
        }else if (Num == num8){
            punch.GetComponent<PunchScript>().num = 8;
        }else if (Num == num9){
            punch.GetComponent<PunchScript>().sign = 1; // 여기 나중에 9 0 으로 바꾸기
            punch.GetComponent<PunchScript>().num = -1;
        }else {
            punch.GetComponent<PunchScript>().sign = 2;
            punch.GetComponent<PunchScript>().num = -1;
        }
        punch.GetComponent<PunchScript>().calculator();
    }
    
    //날려보내는 함수
    public IEnumerator flying(GameObject NumberImage){
        Debug.Log(NumberImage);
        Vector3 destination = new Vector3(-5.86f, 2.34f, -1.06f); //날려보내기
        while(transform.position != destination)
        {
            //Vector3 speed = Vector3.zero; 
            NumberImage.transform.position = Vector3.Lerp(NumberImage.transform.position, destination, 0.04f);        
            yield return null;
        }
    }

    //스크롤바 바꾸기
    public void scollbar(){
        //
    }
}

