using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스 : NSBC(Not So Big Core)
//이름에서 알수 있듯 그라디우스의 빅 코어에서 따왔으나 이름과 달리 그렇게 크지는 않아 이렇게 불리게 되었다.
//패턴 1 : 1~4초의 랜덤한 시간이 지나면 레이저 4개를 발사한다.
//패턴 2 : 좌우로 이동하면서 쏘다가 멈추고 앞으로 돌진한다. 돌진이 끝나면 뒤로 천천히 돌아온다.
//패턴 3(상위 난이도) : 발사하는 레이저중 안쪽 2개의 레이저가 플레이어를 향해 발사한다.


public class Boss_NSBC_AI : MonoBehaviour
{
    Gun[] guns;
    public float moveSpeed = 1;

    public float moveDir = 1f;

    public float Pattern_Dur = 0f;
    int val = 0;

    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        val = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Random.InitState((int)(Time.time * 100));//랜덤 시드 초기화



        if (val == 0)
        { val = (int)Random.Range(1f, 4f); }//패턴 시간 랜덤으로 생성

        if (this.transform.position.y <= 5f)//보스 입장 뒤
        {
            //destructable.Boss_Entered = true;
            Pattern_Dur += Time.deltaTime;

            if (Pattern_Dur < val)//좌우 이동
            { moveLNR(); }


            else if (Pattern_Dur >= val)//쏘기
            {
                shoot();
                Pattern_Dur = 0f;
                val = 0;
            }
        }        
    }

    void moveLNR()
    {
        Vector2 pos = transform.position;

        pos.x += moveDir * moveSpeed * Time.fixedDeltaTime;

        transform.position = pos;
        if (pos.x <= -1.73f || pos.x >= 1.73f)
        { moveDir = -(moveDir); }
    }

    void shoot()
    {
        foreach (Gun gun in guns)
        {
            if (gun.gameObject.activeSelf)
            {
                gun.EnemyShoot();
            }
        }
    }


}
