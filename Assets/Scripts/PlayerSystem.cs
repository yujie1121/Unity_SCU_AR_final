using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Linq;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField, Header("�����e�n�ɶ�"), Range(0, 3)]
    private float timeBeforeAttack;
    [SerializeField, Header("�����ɶ�"), Range(0, 3)]
    private float timeAttack;
    [SerializeField, Header("������n�ɶ�"), Range(0, 3)]
    private float timeAfterAttack;
    [SerializeField, Header("���a�����ϰ�")]
    private GameObject playerAttackArea;

    private NavMeshAgent agent;
    private Animator player_ani;
    private string player_parMove = "���ʼƭ�";
    private string player_parAttack = "Ĳ�o����";
    private bool isAttacking;
    private bool targetAcquired = false; 
    [SerializeField, Header("�ؼ�")]
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
        // print("<color=#f63>�����e�n</color>");
        yield return new WaitForSeconds(timeBeforeAttack);
        // print("<color=#f63>����</color>");
        playerAttackArea.SetActive(true);   // true �N��Хܧ����ϰ�
        yield return new WaitForSeconds(timeAttack);
        // print("<color=#f63>������n</color>");
        playerAttackArea.SetActive(false);  // false �N�����ç����ϰ� 
        yield return new WaitForSeconds(timeAfterAttack);
        // print("<color=#f63>��������</color>");
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
