using UnityEngine;
using UnityEngine.UI;

public class HpHouse : HpSystem
{
    [SerializeField, Header("�Ϥ����")]
    private Image imgHp;

    private float hpMax;

    private string enemyAttackAreaName = "�ĤH�����ϰ�";

    private CanvasManager canvasManager;

    private void Awake()
    {
        hpMax = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == enemyAttackAreaName)
        {
            Damage(50);
        }
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        imgHp.fillAmount = hp / hpMax;
    }

    protected override void Dead()
    {
        base.Dead();
        GameObject canvasController = GameObject.Find("�e�����");
        canvasManager = canvasController.GetComponent<CanvasManager>();
        canvasManager.ShowDefeatCanvas();
    }
}
