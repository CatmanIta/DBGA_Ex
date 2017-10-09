using System;
using UnityEngine;

namespace FactoryMethod
{

    public class RangedEnemy : Enemy
    {
        public override void Attack()
        {
            Debug.Log("Ranged attack");
        }

    }

}