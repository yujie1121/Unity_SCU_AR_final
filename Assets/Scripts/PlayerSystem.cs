using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Linq;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField, Header("攻擊前搖時間"), Range(0, 3)]
    private float timeBeforeAttack;
    [SerializeField, Header("攻擊時間"), Range(0, 3)]
    private float timeAttack;
    [SerializeField, Header("攻擊後搖時間"), Range(0, 3)]
    private float timeAfterAttack;
    [SerializeField, Header("玩家攻擊區域")]
    private GameObject playerAttackArea;

    private NavMeshAgent agent;
    private Animator player_ani;
    private string player_parMove = "移動數值";
    private string player_parAttack = "觸發攻擊";
    private bool isAttacking;
    private bool targetAcquired = false; 
    [SerializeField, Header("目標")]
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player_ani = GetComponent<Animator>();
        FindClosestEnemy();
    }

    private void Start()
    {
        FindClosestEnemy();
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        if (target == null && !targetAcquired)
        {
            FindClosestEnemy();
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            targetAcquired = true; 
        }

        if (target != null)
        {
            Move();
            Attack();
        }
    }

    private void Move()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            player_ani.SetFloat(player_parMove, agent.velocity.magnitude);
        }
    }

    private void Attack()
    {
        if (isAttacking) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isAttacking = true;
            player_ani.SetTrigger(player_parAttack);
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack()
    {
        // print("<color=#f63>攻擊前搖</color>");
        yield return new WaitForSeconds(timeBeforeAttack);
        // print("<color=#f63>攻擊</color>");
        playerAttackArea.SetActive(true);   // true 代表標示攻擊區域
        yield return new WaitForSeconds(timeAttack);
        // print("<color=#f63>攻擊後搖</color>");
        playerAttackArea.SetActive(false);  // false 代表隱藏攻擊區域 
        yield return new WaitForSeconds(timeAfterAttack);
        // print("<color=#f63>攻擊結束</color>");
        isAttacking = false;

        if (target == null || target.GetComponent<HpEnemy>().IsDead())
        {
            Destroy(gameObject);
        }
    }

    private void FindClosestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        Transform closestEnemy = enemies
            .Select(e => e.transform)
            .OrderBy(t => Vector3.Distance(transform.position, t.position))
            .FirstOrDefault();

        target = closestEnemy;
    }
}
