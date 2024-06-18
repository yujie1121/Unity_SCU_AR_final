using UnityEngine;
using UnityEngine.UI;

public class HpEnemy : HpSystem
{
    [SerializeField, Header("�Ϥ����")]
    private Image imgEnemyHp;

    private float hpMax;

    private string playerAttackAreaName = "���a�����ϰ�";

    private void Awake()
    {
        hpMax = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == playerAttackAreaName)
        {
            Damage(50);
        }
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        imgEnemyHp.fillAmount = hp / hpMax;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }
}
