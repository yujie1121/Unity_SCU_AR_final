using UnityEngine;
using UnityEngine.UI;

public class HpHouse : HpSystem
{
    [SerializeField, Header("圖片血條")]
    private Image imgHp;

    private float hpMax;
    // private bool stop = false;

    private string enemyAttackAreaName = "敵人攻擊區域";

    private CanvasManager canvasManager;

    private void Awake()
    {
        hpMax = hp;
    }
    /*
    private void Start()
    {
        ClearTimer clearTimer = FindObjectOfType<ClearTimer>();
        if (clearTimer != null)
        {
            clearTimer.OnVictory.AddListener(StopHpCalculation);
        }
    }*/

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
        ClearTimer timer = FindObjectOfType<ClearTimer>();
        timer.StopTimer();
        GameObject canvasController = GameObject.Find("畫布控制器");
        canvasManager = canvasController.GetComponent<CanvasManager>();
        canvasManager.ShowDefeatCanvas();
    }

    /*
    private void StopHpCalculation()
    {
        stop = true;
    }
    */
}
