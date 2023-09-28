// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace TriviaQuizKit
{
	/// <summary>
	/// Object pooling is a common game development technique that helps reduce the amount of garbage generated
	/// at runtime when creating and destroying a lot of objects.
	///
	/// You can find an official tutorial from Unity about object pooling here:
	/// https://unity3d.com/learn/tutorials/topics/scripting/object-pooling
	/// </summary>
    public class ObjectPool : MonoBehaviour
    {
        public GameObject Prefab;
        public int InitialSize;

        private readonly Stack<GameObject> instances = new Stack<GameObject>();

        private void Awake()
        {
            Assert.IsNotNull(Prefab);
        }

        private void Start()
        {
            for (var i = 0; i < InitialSize; i++)
            {
                var obj = CreateInstance();
                obj.SetActive(false);
                instances.Push(obj);
            }
        }

        public GameObject GetObject()
        {
            var obj = instances.Count > 0 ? instances.Pop() : CreateInstance();
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            var pooledObject = obj.GetComponent<PooledObject>();
            Assert.IsNotNull(pooledObject);
            Assert.IsTrue(pooledObject.Pool == this);

            obj.SetActive(false);
            instances.Push(obj);
        }

        public void Reset()
        {
            var objectsToReturn = new List<GameObject>();
            foreach (var instance in transform.GetComponentsInChildren<PooledObject>())
            {
                if (instance.gameObject.activeSelf)
                {
                    objectsToReturn.Add(instance.gameObject);
                }
            }
            foreach (var instance in objectsToReturn)
            {
                ReturnObject(instance);
            }
        }

        private GameObject CreateInstance()
        {
            var obj = Instantiate(Prefab);
            var pooledObject = obj.AddComponent<PooledObject>();
            pooledObject.Pool = this;
            obj.transform.SetParent(transform);
            return obj;
        }
    }

    /// <summary>
    /// Utility class to identify the pool of a pooled object.
    /// </summary>
    public class PooledObject : MonoBehaviour
    {
        public ObjectPool Pool;
    }
}
