using UnityEngine;

public class HpSystem : MonoBehaviour
{
    [SerializeField, Header("血量"), Range(0, 800)]
    protected float hp;

    protected virtual void Damage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        print($"<color=#f63>{name} 受傷，血量: {hp}</color>");
        if (hp <= 0) Dead();
    }

    protected virtual void Dead()
    {
        print($"<color=#f33>{name} 死亡</color>");
    }
}
