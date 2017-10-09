using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ObjectPool
{

    public class ObjectPool<T> where T : MonoBehaviour
    {
        // Objects in the pool
        List<T> availableObjects;

        public GameObject prefab;

        // Initialise the objct pool with MAX objects
        public void Initialise(int max)
        {
            availableObjects = new List<T>();
            for (int i = 0; i < max; i++)
            {
                var go = GameObject.Instantiate(prefab) as GameObject;
                go.SetActive(false);
                availableObjects.Add(go.GetComponent<T>());
            }
        }

        // Returns an available object
        public T GetNewObject()
        {
            // Check that we have bullets available
            if (availableObjects.Count == 0)
            {
                Debug.LogWarning("Reached max objects!");
                return null;
            }

            var chosenobject = availableObjects[0];
            availableObjects.RemoveAt(0);

            chosenobject.gameObject.SetActive(true);

            return chosenobject;
        }

        public void Release(T o)
        {
            availableObjects.Add(o);
            o.gameObject.SetActive(false);
        }

    }

}
