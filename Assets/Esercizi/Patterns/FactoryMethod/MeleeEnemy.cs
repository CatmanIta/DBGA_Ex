using System;
using UnityEngine;

namespace FactoryMethod {

    public class MeleeEnemy : Enemy {

        public override void Attack()
        {
            Debug.Log("Melee attacK");
        }

    }

}
