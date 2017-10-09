using UnityEngine;
using System.Collections;

namespace AI.BehaviourTree
{
    public class AIBot_BehaviourTree : MonoBehaviour
    {
        public int currentAmmo = 0;

        private Task rootTask;

        public void Awake()
        {
            Blackboard blackboard = GetComponent<Blackboard>();

            // Create the nodes
            var mainSelTask = new SelectorTask();
            mainSelTask.blackboard = blackboard;

            var seqAmmoTask = new SequenceTask();
            seqAmmoTask.blackboard = blackboard;

            var seqShootingTask = new SequenceTask();
            seqShootingTask.blackboard = blackboard;
            
            var movePlayerTask = new MoveToPlayerTask();
            movePlayerTask.blackboard = blackboard;

            var playerCloseTask = new PlayerIsCloseTask();
            playerCloseTask.threshold = 5;
            playerCloseTask.blackboard = blackboard;

            var moveAmmoTask = new MoveToAmmoTask();
            moveAmmoTask.blackboard = blackboard;

            var stayTask = new StayStillTask();
            stayTask.blackboard = blackboard;

            var hasAmmoTask = new HasAmmoTask();
            hasAmmoTask.blackboard = blackboard;

            var shootTask = new ShootTask();
            shootTask.blackboard = blackboard;

            var notTask = new NotTask();
            notTask.blackboard = blackboard;

            var forTask = new ForTask();
            forTask.nRepeat = 5;
            forTask.blackboard = blackboard;

            var waitTask = new WaitTask();
            waitTask.blackboard = blackboard;

            var playSfxTask = new PlaySfxTask();
            playSfxTask.blackboard = blackboard;
            playSfxTask.source = this.GetComponent<AudioSource>();

            // Connect the nodes to create the tree
            rootTask = mainSelTask;
            mainSelTask.children.Add(seqAmmoTask);
            mainSelTask.children.Add(seqShootingTask);
            mainSelTask.children.Add(movePlayerTask);

            seqAmmoTask.children.Add(notTask);
            notTask.children.Add(hasAmmoTask);
            seqAmmoTask.children.Add(moveAmmoTask);

            seqShootingTask.children.Add(playerCloseTask);
            seqShootingTask.children.Add(stayTask);
            seqShootingTask.children.Add(forTask);
            forTask.children.Add(waitTask);
            waitTask.children.Add(shootTask);
            seqShootingTask.children.Add(playSfxTask);
        }


        public void Start()
        {
            StartCoroutine(TickAICO());
        }


        public IEnumerator TickAICO()
        {
            while (true)
            {
                rootTask.Run();
                yield return null;
            }
        }


        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Ammo>())
            {
                Destroy(other.gameObject);
                this.currentAmmo += 10;
            }
        }

        public void Shoot()
        {
            if (this.currentAmmo > 0)
            {
                this.currentAmmo -= 1;
                Debug.Log("SHOOTING");
            }
        }

    }
}
