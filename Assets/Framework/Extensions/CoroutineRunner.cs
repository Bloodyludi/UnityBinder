using UnityEngine;
using System.Collections;

namespace DIContainer.Framework.Extensions
{
    public class CoroutineRunner : ICoroutineRunner
    {
        private readonly ICoroutineRunner coroutineRunnerBehaviour;

        public CoroutineRunner(IGameObjectFactory gameObjectFactory)
        {
            var go = gameObjectFactory.Create("CoroutineRunnerPrefab");
            coroutineRunnerBehaviour = go.GetComponent<ICoroutineRunner>();
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return coroutineRunnerBehaviour.StartCoroutine(routine);
        }
    }
}

