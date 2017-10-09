using UnityEngine;
using System.Collections;

namespace ObjectPool
{

    public class TestObjectPool : MonoBehaviour
    {
        ObjectPool<Bullet> pool = new ObjectPool<Bullet>();

        public GameObject bulletPrefab;

        void Start()
        {
            pool.prefab = bulletPrefab;
            pool.Initialise(10);

            Bullet b1 = pool.GetNewObject();
            Bullet b2 = pool.GetNewObject();

            // ... use the Bullet

            pool.Release(b1);
           // pool.Release(b2);
        }

        void Update()
        {
           //Bullet b = pool.GetNewObject();
        }
    }
}