using UnityEngine;

public class DontDestoyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
