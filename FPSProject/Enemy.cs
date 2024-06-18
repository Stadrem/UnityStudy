using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.AI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //열거형
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die,
        Return,
        Patrol
    }

    //현재 상태
    public EnemyState state;

    //플레이어
    Transform player;

    //인지 범위
    public float findDistance = 8;

    //공격 범위
    public float attackDistance = 2;

    //공격 딜레이 시간
    public float attackDelayTime = 2;

    //현재 시간
    float currentTime = 0;

    //이동 속력
    float moveSpeed = 3;

    //HP UI
    public Slider hpUI;

    //현재 HP
    float currentHP = 0;

    //최대 HP
    float maxHP = 3;

    //피격 시간
    float damagedDelayTime = 2;

    //죽는 시간
    float dieDelayTime = 2;

    //네이게이션 에이전트 변수
    NavMeshAgent navi;

    //경로를 나타낼 라인 변수
    LineRenderer li;

    //초기 위치
    Vector3 originPos;

    //이동 범위
    float moveDistance = 20;

    //기다리는 시간
    float idleDealyTime = 2;

    //Animator 컴포넌트
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Player Transform을 찾기
        GameObject go = GameObject.Find("Player");
        player = go.transform;

        //초기 위치 설정
        originPos = transform.position;

        //현재 HP를 최대 HP로 셋팅
        currentHP = maxHP;

        //네비메시 에이전트 컴포넌트 불러오기
        navi = GetComponent<NavMeshAgent>();

        //라인렌더러 컴포넌트 불러오기
        li = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //0. 대기 상태
        if (state == EnemyState.Idle)
        {
            //대기 상태 할 일
            UpdateIdle();
        }

        //1. 이동 상태
        else if (state == EnemyState.Move)
        {
            //이동 상태 할 일
            UpdateMove();
        }

        //2. 공격 상태
        else if (state == EnemyState.Attack)
        {
            //공격 상태 할 일
            UpdateAttack();
        }

        //3. 피격 상태
        else if (state == EnemyState.Damaged)
        {
            //피격 상태 할 일
            //UpdateDamaged();
        }

        //4. 죽음 상태
        else if (state == EnemyState.Die)
        {
            //죽음 상태 할 일
            UpdateDie();
        }
        else if (state == EnemyState.Return)
        {
            UpdateReturn();
        }
        else if (state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
    }

    //상태를 변경할 때 한번 실행되는 함수
    void ChangeState(EnemyState s)
    {
        //현재 상태를 s 값으로 세팅
        state = s;

        //각 상태에 따른 초기화를 해줌
        if (state == EnemyState.Idle)
        {
            //네비 초기화
            ResetNavi();

            //Idle 애니메이션 실행
            anim.SetTrigger("Idle");
        }
        else if(state == EnemyState.Move)
        {
            //Move 애니메이션 실행
            anim.SetTrigger("Move");
        }
        else if(state==EnemyState.Return)
        {

        }
        else if (state == EnemyState.Attack)
        {
            //네비게이션 초기화
            ResetNavi();

            //현재시간을 공격딜레이 시간으로 세팅
            currentTime = attackDelayTime;
        }
        else if (state == EnemyState.Damaged)
        {
            //Damaged 애니메이션을 실행
            anim.SetTrigger("Damaged");

            //DamagedProcess 코루틴을 실행
            StartCoroutine(DamagedProcess());
        }
        else if (state == EnemyState.Die)
        {
            //Die 애니메이션 실행
            anim.SetTrigger("Die");
        }
        else if (state == EnemyState.Patrol)
        {
            //현재시간을 초기화
            currentTime = 0;

            //랜덤한 X, Z값을 구한다. (-20, 20)
            float randX = Random.Range(-20, 20);
            float randZ = Random.Range(-20, 20);

            //랜덤한 위치를 구함
            Vector3 pos = originPos;
            pos.x += randX;
            pos.y += randZ;

            //목적지를 해당 위치로 설정
            setDestination(pos);

            //Move 애니메이션 실행
            anim.SetTrigger("Move");
        }
    }
    
    void UpdatePatrol()
    {
        //만약에 목적지와 가까워지면
        if(navi.remainingDistance < 0.1f)
        {
            //상태를 Idle로 변경
            ChangeState(EnemyState.Idle);
        }
    }

    void UpdateIdle()
    {
        //1. 플레이어와의 거리
        float dist = Vector3.Distance(transform.position, player.position);

        //2. 만약에 플레이어와의 거리가 인지범위 안에 들어왔다면
        if (dist < findDistance)
        {
            //3. 상태를 Move로 전환
            ChangeState(EnemyState.Move);
        }
        else
        {
            //시간을 흐르게 함
            currentTime += Time.deltaTime;

            //만약 현재 시간이 Idle 시간보다 커지면 
            if(currentTime > idleDealyTime)
            {
                ChangeState(EnemyState.Patrol);
            }
        }
    }

    void UpdateMove()
    {
        //초기 위치와 거리를 구함
        float distByOrigin = Vector3.Distance(transform.position, originPos);

        //그 거리가 이동 반경보다 커지면
        if(distByOrigin > moveDistance)
        { 
            //상태를 Return으로 전환
            ChangeState(EnemyState.Return);

            print("상태전환 : Move -> Return");

            //함수 중단
            return;
        }
        
        //플레이어와의 거리를 구함
        float dist = Vector3.Distance(transform.position, player.position);

        //만약에 거리가 공격 범위보다 크다면
        if(dist > attackDistance)
        {
            /*
            //1. 플레이어를 향하는 방향을 구함
            Vector3 dir = player.position - transform.position;
            dir.Normalize();

            //2. 그 방향으로 움직임
            //p = p0 + vt
            transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
            */

            setDestination(player.position);
        }
        //그렇지 않으면
        else
        {
            ChangeState(EnemyState.Attack);
        }
    }

    public void OnDamaged()
    {
        //만약 현재 상태가 die라면 함수 탈출
        if (state == EnemyState.Die)
        {
            return;
        }

        //네비게이션 초기화
        ResetNavi();

        //모든 코루틴 종료
        StopAllCoroutines();

        //1. 자신의 HP를 깍는다.
        currentHP--;

        //HP 바를 갱신
        float ratio = currentHP / maxHP;
        hpUI.value = ratio;

        //2. 만약에 HP가 0보다 크다면
        if (currentHP > 0)
        {
            //3. 상태를 Damaged 상태로 전환
            ChangeState(EnemyState.Damaged);
        }
        //4. 그렇지 않으면  (HP <= 0)
        else
        {
            //5. Die 상태로 전환
            ChangeState(EnemyState.Die);
        }
    }

    void UpdateAttack()
    {
        //플레이어와 거리를 구함
        float dist = Vector3.Distance(transform.position, player.position);

        //만약에 공격 범위에 들어왔다면
        if (dist < attackDistance)
        {
            //현재 시간을 누적
            currentTime += Time.deltaTime;

            //공격 딜레이 시간이 지나면
            if (currentTime > attackDelayTime)
            {
                //플레이어를 공격
                //print("플레이어를 공격.");
                //PlayerMove 컴포넌트 불러오기
                PlayerMove pm = player.GetComponent<PlayerMove>();

                //가져온 컴포넌트의 OnDamaged 함수 실행
                pm.OnDamaged();

                //현재 시간을 초기화
                currentTime = 0;

                //Attack 애니메이션 실행
                anim.SetTrigger("Attack");
            }
        }
        //그렇지 않으면
        else
        {
            //상태를 Move로 전환
            ChangeState(EnemyState.Move);
        }
    }

    void UpdateDamaged()
    {
        //1. 시간을 흐르게 한다.
        currentTime += Time.deltaTime;

        //2. 피격 시간이 지나면
        if(currentTime > damagedDelayTime)
        {
            //3. Idle 상태로 전환
            state = EnemyState.Idle;
            print("상태 전환 : Damaged -> Idle");

            //모든 코루틴 종료
            StopAllCoroutines();

            //4. currentTime 초기화
            currentTime = 0;
        }
    }

    IEnumerator DamagedProcess()
    {
        //1 damagedDelayTime동안 기다림
        yield return new WaitForSeconds(damagedDelayTime);

        //2. Idel 상태로 전환
        ChangeState(EnemyState.Idle);
    }

    void UpdateDie()
    {
        //1. 시간을 흐르게 함
        currentTime += Time.deltaTime;

        //2. 만약에 현재 시간이 Die 시간보다 커지면
        if(currentTime > dieDelayTime)
        {
            //네비게이션 시스템 비활성화
            navi.enabled = false;

            //캡슐 콜라이더 비활성화
            GetComponent<CapsuleCollider>().enabled = false;

            //3. 아래로 움직이게 한다.
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            //4. 만약에 나의 위치값(y)dl -2보다 작아지면
            if(transform.position.y < -2)
            {
                //5. 파괴
                Destroy(gameObject);
            }
        }
    }

    void UpdateReturn()
    {
        //1. 초기 위치 - 나의 위치를 구함
        float dist = Vector3.Distance(originPos, transform.position);

        //2. 만약에 그거리가 0.5보다 작다면
        if(dist < 1.0f)
        {
            ChangeState(EnemyState.Idle);
        }
        //4. 그렇지 않으면
        else
        {
            /*
            //5. 초기 위치로 이동
            //5-1 초기위치로 향하는 방향 구함
            Vector3 dir = originPos - transform.position;
            dir.Normalize();

            //5-2 방향으로 계속 이동
            transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
            */

            //네비게이션에게 목적지를 처음 위치로 세팅
            setDestination(originPos);
        }
    }

    void setDestination(Vector3 pos)
    {
        //네비 목적지 설정
        navi.destination = pos;

        //목적지를 향하는 경로를 li을 이용
        li.positionCount = navi.path.corners.Length;
        li.SetPositions(navi.path.corners);
    }

    void ResetNavi()
    {
        //이동을 멈춤
        navi.isStopped = true;

        //길찾기 초기화
        navi.ResetPath();

        //라인을 초기화
        li.positionCount = 0;
    }
}
