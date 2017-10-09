using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.PCG
{

    public class DungeonGenerator : MonoBehaviour
    {
        public GameObject cellPrefab;
        public DungeonCell[,] cells;

        public int dungeonSize = 121;


        public enum Direction
        {
            N, E, S, W
        }

        void Start()
        {
            //   int seed = System.DateTime.Now.Millisecond;
            //   Random.InitState(seed);
            //Debug.Log(seed);

            long seed = System.DateTime.Now.Ticks;
            Debug.Log(seed);

            //Debug.Log(Random.seed);

            cells = new DungeonCell[dungeonSize, dungeonSize];

            for (int i = 0; i < dungeonSize; i++)
            {
                for (int j = 0; j < dungeonSize; j++)
                {
                    var go = Instantiate(cellPrefab);
                    go.transform.position = new Vector3(i, j);
                    cells[i, j] = go.GetComponent<DungeonCell>();
                    cells[i, j].i = i;
                    cells[i, j].j = j;
                }
            }

            StartCoroutine(AlgorithmCO());
        }

        IEnumerator AlgorithmCO()
        {
            // Initialise the dungeon as full of walls
            for (int i = 0; i < dungeonSize; i++)
            {
                for (int j = 0; j < dungeonSize; j++)
                {
                    cells[i, j].SetWall();
                }
            }

            // Choose a start cell
            int start_i = 0;// dungeonSize / 2;
            int start_j = 0;//dungeonSize / 2;
            cells[start_i, start_j].SetEmpty();

            yield return null;

            Stack<DungeonCell> stack = new Stack<DungeonCell>();
            stack.Push(cells[start_i,start_j]);
            while (stack.Count > 0)
            {
                // Peek at the top cell on the stack
                DungeonCell currentCell = stack.Peek();

                // Check the possible directions
                List<Direction> possibleDirections = GetPossibleDirections(currentCell.i, currentCell.j);

                if (possibleDirections.Count == 0)
                {
                    // No more directions from here, let's remove this from the stack
                    stack.Pop();
                } 
                else
                {
                    // Get a random possible direction
                    int index = Random.Range(0, possibleDirections.Count);
                    Direction dir = possibleDirections[index];

                    // We dig to the new cell
                    var newCell = Dig(currentCell, dir);

                    // We add it to the stack
                    stack.Push(newCell);
                }

                yield return new WaitForSeconds(0.0001f);
            }

        }

        DungeonCell Dig(DungeonCell fromCell, Direction dir)
        {
            DungeonCell toCell = null;
            DungeonCell midCell = null;
            switch (dir)
            {
                case Direction.N:
                    toCell = cells[fromCell.i, fromCell.j + 2];
                    midCell = cells[fromCell.i, fromCell.j + 1];
                    break;
                case Direction.E:
                    toCell = cells[fromCell.i+2, fromCell.j];
                    midCell = cells[fromCell.i + 1, fromCell.j];
                    break;
                case Direction.S:
                    toCell = cells[fromCell.i, fromCell.j - 2];
                    midCell = cells[fromCell.i, fromCell.j - 1];
                    break;
                case Direction.W:
                    toCell = cells[fromCell.i-2, fromCell.j];
                    midCell = cells[fromCell.i - 1, fromCell.j];
                    break;
            }
            toCell.SetEmpty();
            midCell.SetEmpty();
            return toCell;
        }


        List<Direction> GetPossibleDirections(int i, int j)
        {
            List<Direction> list = new List<Direction>();

            // N
            if (j+2 < dungeonSize && cells[i,j+2].isWall)
            {
                list.Add(Direction.N);
            }

            // E 
            if (i+2 < dungeonSize && cells[i+2,j].isWall)
            {
                list.Add(Direction.E);
            }

            // S
            if (j-2 >= 0 && cells[i,j-2].isWall)
            {
                list.Add(Direction.S);
            }

            // W
            if (i-2 >= 0 && cells[i-2, j].isWall)
            {
                list.Add(Direction.W);
            }

            return list;
        }

    }

}