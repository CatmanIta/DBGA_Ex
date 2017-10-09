using UnityEngine;
using System.Collections;
using System;

namespace AI.BehaviourTree
{

    public class PlaySfxTask : Task
    {
        public AudioSource source;

        public override bool Run()
        {
            source.Play();
            return true;
        }
    }

}