using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.AI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //������
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

    //���� ����
    public EnemyState state;

    //�÷��̾�
    Transform player;

    //���� ����
    public float findDistance = 8;

    //���� ����
    public float attackDistance = 2;

    //���� ������ �ð�
    public float attackDelayTime = 2;

    //���� �ð�
    float currentTime = 0;

    //�̵� �ӷ�
    float moveSpeed = 3;

    //HP UI
    public Slider hpUI;

    //���� HP
    float currentHP = 0;

    //�ִ� HP
    float maxHP = 3;

    //�ǰ� �ð�
    float damagedDelayTime = 2;

    //�״� �ð�
    float dieDelayTime = 2;

    //���̰��̼� ������Ʈ ����
    NavMeshAgent navi;

    //��θ� ��Ÿ�� ���� ����
    LineRenderer li;

    //�ʱ� ��ġ
    Vector3 originPos;

    //�̵� ����
    float moveDistance = 20;

    //��ٸ��� �ð�
    float idleDealyTime = 2;

    //Animator ������Ʈ
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Player Transform�� ã��
        GameObject go = GameObject.Find("Player");
        player = go.transform;

        //�ʱ� ��ġ ����
        originPos = transform.position;

        //���� HP�� �ִ� HP�� ����
        currentHP = maxHP;

        //�׺�޽� ������Ʈ ������Ʈ �ҷ�����
        navi = GetComponent<NavMeshAgent>();

        //���η����� ������Ʈ �ҷ�����
        li = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //0. ��� ����
        if (state == EnemyState.Idle)
        {
            //��� ���� �� ��
            UpdateIdle();
        }

        //1. �̵� ����
        else if (state == EnemyState.Move)
        {
            //�̵� ���� �� ��
            UpdateMove();
        }

        //2. ���� ����
        else if (state == EnemyState.Attack)
        {
            //���� ���� �� ��
            UpdateAttack();
        }

        //3. �ǰ� ����
        else if (state == EnemyState.Damaged)
        {
            //�ǰ� ���� �� ��
            //UpdateDamaged();
        }

        //4. ���� ����
        else if (state == EnemyState.Die)
        {
            //���� ���� �� ��
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

    //���¸� ������ �� �ѹ� ����Ǵ� �Լ�
    void ChangeState(EnemyState s)
    {
        //���� ���¸� s ������ ����
        state = s;

        //�� ���¿� ���� �ʱ�ȭ�� ����
        if (state == EnemyState.Idle)
        {
            //�׺� �ʱ�ȭ
            ResetNavi();

            //Idle �ִϸ��̼� ����
            anim.SetTrigger("Idle");
        }
        else if(state == EnemyState.Move)
        {
            //Move �ִϸ��̼� ����
            anim.SetTrigger("Move");
        }
        else if(state==EnemyState.Return)
        {

        }
        else if (state == EnemyState.Attack)
        {
            //�׺���̼� �ʱ�ȭ
            ResetNavi();

            //����ð��� ���ݵ����� �ð����� ����
            currentTime = attackDelayTime;
        }
        else if (state == EnemyState.Damaged)
        {
            //Damaged �ִϸ��̼��� ����
            anim.SetTrigger("Damaged");

            //DamagedProcess �ڷ�ƾ�� ����
            StartCoroutine(DamagedProcess());
        }
        else if (state == EnemyState.Die)
        {
            //Die �ִϸ��̼� ����
            anim.SetTrigger("Die");
        }
        else if (state == EnemyState.Patrol)
        {
            //����ð��� �ʱ�ȭ
            currentTime = 0;

            //������ X, Z���� ���Ѵ�. (-20, 20)
            float randX = Random.Range(-20, 20);
            float randZ = Random.Range(-20, 20);

            //������ ��ġ�� ����
            Vector3 pos = originPos;
            pos.x += randX;
            pos.y += randZ;

            //�������� �ش� ��ġ�� ����
            setDestination(pos);

            //Move �ִϸ��̼� ����
            anim.SetTrigger("Move");
        }
    }
    
    void UpdatePatrol()
    {
        //���࿡ �������� ���������
        if(navi.remainingDistance < 0.1f)
        {
            //���¸� Idle�� ����
            ChangeState(EnemyState.Idle);
        }
    }

    void UpdateIdle()
    {
        //1. �÷��̾���� �Ÿ�
        float dist = Vector3.Distance(transform.position, player.position);

        //2. ���࿡ �÷��̾���� �Ÿ��� �������� �ȿ� ���Դٸ�
        if (dist < findDistance)
        {
            //3. ���¸� Move�� ��ȯ
            ChangeState(EnemyState.Move);
        }
        else
        {
            //�ð��� �帣�� ��
            currentTime += Time.deltaTime;

            //���� ���� �ð��� Idle �ð����� Ŀ���� 
            if(currentTime > idleDealyTime)
            {
                ChangeState(EnemyState.Patrol);
            }
        }
    }

    void UpdateMove()
    {
        //�ʱ� ��ġ�� �Ÿ��� ����
        float distByOrigin = Vector3.Distance(transform.position, originPos);

        //�� �Ÿ��� �̵� �ݰ溸�� Ŀ����
        if(distByOrigin > moveDistance)
        { 
            //���¸� Return���� ��ȯ
            ChangeState(EnemyState.Return);

            print("������ȯ : Move -> Return");

            //�Լ� �ߴ�
            return;
        }
        
        //�÷��̾���� �Ÿ��� ����
        float dist = Vector3.Distance(transform.position, player.position);

        //���࿡ �Ÿ��� ���� �������� ũ�ٸ�
        if(dist > attackDistance)
        {
            /*
            //1. �÷��̾ ���ϴ� ������ ����
            Vector3 dir = player.position - transform.position;
            dir.Normalize();

            //2. �� �������� ������
            //p = p0 + vt
            transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
            */

            setDestination(player.position);
        }
        //�׷��� ������
        else
        {
            ChangeState(EnemyState.Attack);
        }
    }

    public void OnDamaged()
    {
        //���� ���� ���°� die��� �Լ� Ż��
        if (state == EnemyState.Die)
        {
            return;
        }

        //�׺���̼� �ʱ�ȭ
        ResetNavi();

        //��� �ڷ�ƾ ����
        StopAllCoroutines();

        //1. �ڽ��� HP�� ��´�.
        currentHP--;

        //HP �ٸ� ����
        float ratio = currentHP / maxHP;
        hpUI.value = ratio;

        //2. ���࿡ HP�� 0���� ũ�ٸ�
        if (currentHP > 0)
        {
            //3. ���¸� Damaged ���·� ��ȯ
            ChangeState(EnemyState.Damaged);
        }
        //4. �׷��� ������  (HP <= 0)
        else
        {
            //5. Die ���·� ��ȯ
            ChangeState(EnemyState.Die);
        }
    }

    void UpdateAttack()
    {
        //�÷��̾�� �Ÿ��� ����
        float dist = Vector3.Distance(transform.position, player.position);

        //���࿡ ���� ������ ���Դٸ�
        if (dist < attackDistance)
        {
            //���� �ð��� ����
            currentTime += Time.deltaTime;

            //���� ������ �ð��� ������
            if (currentTime > attackDelayTime)
            {
                //�÷��̾ ����
                //print("�÷��̾ ����.");
                //PlayerMove ������Ʈ �ҷ�����
                PlayerMove pm = player.GetComponent<PlayerMove>();

                //������ ������Ʈ�� OnDamaged �Լ� ����
                pm.OnDamaged();

                //���� �ð��� �ʱ�ȭ
                currentTime = 0;

                //Attack �ִϸ��̼� ����
                anim.SetTrigger("Attack");
            }
        }
        //�׷��� ������
        else
        {
            //���¸� Move�� ��ȯ
            ChangeState(EnemyState.Move);
        }
    }

    void UpdateDamaged()
    {
        //1. �ð��� �帣�� �Ѵ�.
        currentTime += Time.deltaTime;

        //2. �ǰ� �ð��� ������
        if(currentTime > damagedDelayTime)
        {
            //3. Idle ���·� ��ȯ
            state = EnemyState.Idle;
            print("���� ��ȯ : Damaged -> Idle");

            //��� �ڷ�ƾ ����
            StopAllCoroutines();

            //4. currentTime �ʱ�ȭ
            currentTime = 0;
        }
    }

    IEnumerator DamagedProcess()
    {
        //1 damagedDelayTime���� ��ٸ�
        yield return new WaitForSeconds(damagedDelayTime);

        //2. Idel ���·� ��ȯ
        ChangeState(EnemyState.Idle);
    }

    void UpdateDie()
    {
        //1. �ð��� �帣�� ��
        currentTime += Time.deltaTime;

        //2. ���࿡ ���� �ð��� Die �ð����� Ŀ����
        if(currentTime > dieDelayTime)
        {
            //�׺���̼� �ý��� ��Ȱ��ȭ
            navi.enabled = false;

            //ĸ�� �ݶ��̴� ��Ȱ��ȭ
            GetComponent<CapsuleCollider>().enabled = false;

            //3. �Ʒ��� �����̰� �Ѵ�.
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            //4. ���࿡ ���� ��ġ��(y)dl -2���� �۾�����
            if(transform.position.y < -2)
            {
                //5. �ı�
                Destroy(gameObject);
            }
        }
    }

    void UpdateReturn()
    {
        //1. �ʱ� ��ġ - ���� ��ġ�� ����
        float dist = Vector3.Distance(originPos, transform.position);

        //2. ���࿡ �װŸ��� 0.5���� �۴ٸ�
        if(dist < 1.0f)
        {
            ChangeState(EnemyState.Idle);
        }
        //4. �׷��� ������
        else
        {
            /*
            //5. �ʱ� ��ġ�� �̵�
            //5-1 �ʱ���ġ�� ���ϴ� ���� ����
            Vector3 dir = originPos - transform.position;
            dir.Normalize();

            //5-2 �������� ��� �̵�
            transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
            */

            //�׺���̼ǿ��� �������� ó�� ��ġ�� ����
            setDestination(originPos);
        }
    }

    void setDestination(Vector3 pos)
    {
        //�׺� ������ ����
        navi.destination = pos;

        //�������� ���ϴ� ��θ� li�� �̿�
        li.positionCount = navi.path.corners.Length;
        li.SetPositions(navi.path.corners);
    }

    void ResetNavi()
    {
        //�̵��� ����
        navi.isStopped = true;

        //��ã�� �ʱ�ȭ
        navi.ResetPath();

        //������ �ʱ�ȭ
        li.positionCount = 0;
    }
}
