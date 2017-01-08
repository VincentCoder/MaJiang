using UnityEngine;

public class GameMain : MonoBehaviour
{
    private void Start()
    {
        UIManager.ShowUISync(EWndID.UI_Login, null);
    }
}
