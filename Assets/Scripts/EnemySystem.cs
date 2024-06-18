using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemySystem : MonoBehaviour
{
    [SerializeField, Header("目標")]
    private Transform target;
    [SerializeField, Header("攻擊前搖時間"), Range(0, 3)]
    private float timeBeforeAttack;
    [SerializeField, Header("攻擊時間"), Range(0, 3)]
    private float timeAttack;
    [SerializeField, Header("攻擊後搖時間"), Range(0, 3)]
    private float timeAfterAttack;
    [SerializeField, Header("敵人攻擊區域")]
    private GameObject enemyAttackArea;

    private NavMeshAgent agent;
    private Animator enemy_ani;
    private string enemy_parMove = "移動數值";
    private string enemy_parAttack = "觸發攻擊";
    private bool isAttacking;
    private string houseName = "屋子物件";

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
        // print("<color=#f63>攻擊前搖</color>");
        yield return new WaitForSeconds(timeBeforeAttack);
        // print("<color=#f63>攻擊</color>");
        enemyAttackArea.SetActive(true);   // true 代表標示攻擊區域
        yield return new WaitForSeconds(timeAttack);
        // print("<color=#f63>攻擊後搖</color>");
        enemyAttackArea.SetActive(false);  // false 代表隱藏攻擊區域 
        yield return new WaitForSeconds(timeAfterAttack);
        // print("<color=#f63>攻擊結束</color>");
        isAttacking = false;
    }
}