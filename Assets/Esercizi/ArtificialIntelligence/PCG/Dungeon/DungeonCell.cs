using UnityEngine;
using System.Collections;

namespace AI.PCG
{
    public class DungeonCell : MonoBehaviour
    {
        public bool isWall;
        public int i;
        public int j;

        public void SetWall()
        {
            isWall = true;
            this.GetComponent<MeshRenderer>().material.color = Color.gray;
        }

        public void SetEmpty()
        {
            isWall = false;
            this.GetComponent<MeshRenderer>().material.color = Color.black;
        }

    }

}