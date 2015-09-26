using UnityEngine;
using System.Collections;

namespace Container.Framework.Extensions
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }

    public class CoroutineRunnerBehaviour : MonoBehaviour, ICoroutineRunner
    {

    }

    public class CoroutineRunner : ICoroutineRunner
    {
        private readonly IGameObjectFactory gameObjectFactory;
        private readonly ICoroutineRunner coroutineRunnerBehaviour;

        public CoroutineRunner(IGameObjectFactory gameObjectFactory)
        {
            this.gameObjectFactory = gameObjectFactory;

            var go = this.gameObjectFactory.Create("CoroutineRunnerPrefab");
            coroutineRunnerBehaviour = go.GetComponent<ICoroutineRunner>();
        }

        Coroutine StartCoroutine(IEnumerator routine)
        {
            return coroutineRunnerBehaviour.StartCoroutine(routine);
        }
    }
}

