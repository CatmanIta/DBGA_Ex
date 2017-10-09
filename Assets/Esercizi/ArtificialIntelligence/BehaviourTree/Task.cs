using System.Collections.Generic;

namespace AI.BehaviourTree
{

    public abstract class Task
    {
        public Blackboard blackboard;
        public List<Task> children = new List<Task>();

        public abstract bool Run();

    }

}