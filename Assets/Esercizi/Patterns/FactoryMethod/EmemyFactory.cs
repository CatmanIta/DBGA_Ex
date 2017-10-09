using UnityEngine;

namespace FactoryMethod { 

    public class EmemyFactory {

        // Method that returns the correct enemy
	    public Enemy GetEnemy() { 
	        if (Random.value > 0.5f)
            {
                return new MeleeEnemy();
            }
            else
            {
                return new RangedEnemy();
            }
	    }
	
    }

}