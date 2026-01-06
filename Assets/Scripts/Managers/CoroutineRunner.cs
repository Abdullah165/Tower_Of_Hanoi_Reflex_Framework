using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if(instance == null)
            {
                var go = new GameObject("CoroutineRunner");
                instance = go.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public Coroutine Run(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }
}
