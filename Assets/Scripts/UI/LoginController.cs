public class LoginController : UIWindow
{
    public void OnWxLoginBtnClick()
    {
        UIManager.ShowUISync(EWndID.PveScene, null);
    }
}
