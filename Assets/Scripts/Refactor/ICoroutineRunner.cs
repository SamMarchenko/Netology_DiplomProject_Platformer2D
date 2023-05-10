using System.Collections;
using UnityEngine;

namespace Refactor
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}