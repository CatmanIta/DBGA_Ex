using UnityEngine;

public class RangedStrategy : ICombatStrategy
{
    public void Attack()
    {
        Debug.Log("Attacking ranged");
    }

    public void Move()
    {
        Debug.Log("Move away for player");
    }
}
