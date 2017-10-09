using UnityEngine;

public class MeleeStrategy : ICombatStrategy
{
    public void Attack()
    {
        Debug.Log("Attacking meelee");
    }

    public void Move()
    {
        Debug.Log("Move towards player");
    }
}
