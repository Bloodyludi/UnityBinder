using UnityEngine;
using System.Collections;

namespace DIContainer.Framework.Extensions
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
