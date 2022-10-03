// 스테이지의 엔딩씬. 별/플레이어 불러오기

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingscene : MonoBehaviour
{   
    private GameObject endingback;
    private GameObject endingPlayer;
    private GameObject endingstar1;
    private GameObject endingstar2;
    private GameObject endingstar3;
    
    public void endingStart()
    {
        endingback = Resources.Load<GameObject>("ending/endingBackground");
        Instantiate(endingback, new Vector3(-0.08f,-0.02f,-5.14f), Quaternion.identity); // 배경이미지 생성
        endingPlayer = Resources.Load<GameObject>("ending/realendingplayer");
        Instantiate(endingPlayer, new Vector3(-0.18f,-7.89f,-7.17f), Quaternion.identity); //주인공이미지생성
        endingstar1 = Resources.Load<GameObject>("ending/realendingstar1");
        Instantiate(endingstar1, new Vector3(-4.54f,1.12f, -7.27f), endingstar1.transform.rotation); // 별 생성
        endingstar2 = Resources.Load<GameObject>("ending/realendingstar2");
        Instantiate(endingstar2, new Vector3(-0.01f,2.69f, -7.27f), Quaternion.identity);
        endingstar3 = Resources.Load<GameObject>("ending/realendingstar3");
        Instantiate(endingstar3, new Vector3(4.54f,1.12f, -7.27f), endingstar3.transform.rotation);
    }
}
  