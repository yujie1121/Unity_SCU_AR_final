using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemySystem : MonoBehaviour
{
    [SerializeField, Header("�ؼ�")]
    private Transform target;
    [SerializeField, Header("�����e�n�ɶ�"), Range(0, 3)]
    private float timeBeforeAttack;
    [SerializeField, Header("�����ɶ�"), Range(0, 3)]
    private float timeAttack;
    [SerializeField, Header("������n�ɶ�"), Range(0, 3)]
    private float timeAfterAttack;
    [SerializeField, Header("�ĤH�����ϰ�")]
    private GameObject enemyAttackArea;

    private NavMeshAgent agent;
    private Animator enemy_ani;
    private string enemy_parMove = "���ʼƭ�";
    private string enemy_parAttack = "Ĳ�o����";
    private bool isAttacking;
    private string houseName = "�Τl����";

    private void Awake()
    {
        target = GameObject.Find(houseName).transform;

        agent = GetComponent<NavMeshAgent>();
        enemy_ani = GetComponent<Animator>();
        agent.SetDestination(target.position);
    }

    private void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        agent.SetDestination(target.position);
        enemy_ani.SetFloat(enemy_parMove, agent.velocity.magnitude);
    }

    private void Attack()
    {
        if (isAttacking) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isAttacking = true;
            enemy_ani.SetTrigger(enemy_parAttack);
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        // print("<color=#f63>�����e�n</color>");
        yield return new WaitForSeconds(timeBeforeAttack);
        // print("<color=#f63>����</color>");
        enemyAttackArea.SetActive(true);   // true �N��Хܧ����ϰ�
        yield return new WaitForSeconds(timeAttack);
        // print("<color=#f63>������n</color>");
        enemyAttackArea.SetActive(false);  // false �N�����ç����ϰ� 
        yield return new WaitForSeconds(timeAfterAttack);
        // print("<color=#f63>��������</color>");
        isAttacking = false;
    }
}