using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    ICombatStrategy strategy;

    void Awake()
    {
        strategy = new MeleeStrategy();
    }

	void Update () {
        strategy.Move();
        strategy.Attack();
	}
}
