using UnityEngine;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{
	public EWndID WndID
	{
		get
		{
			return m_WndID;
		}
	}

	public EUIState UIState
	{
		get
		{
			return m_eUIState;
		}
	}
	
	public Canvas UICanvas
	{
		get
		{
			return m_UICanvas;
		}
	}

	public CanvasScaler UIScaler
	{
		get
		{
			return m_UIScaler;
		}
	}

	[SerializeField] EWndID m_WndID = EWndID.None;
	[SerializeField] Canvas m_UICanvas;
	[SerializeField] CanvasScaler m_UIScaler;

	EUIState m_eUIState = EUIState.Ready;

	void Awake()
	{
		OnDoAwake();
		UIManager.RegisterUI( this );
	}

	public void PrepareShowUI(object uiData)
	{
		ChangeUIState( EUIState.Prepare );
		PrepareToShow( uiData );
	}

    public void ReadyShowUI(object uiData)
    {
        ChangeUIState(EUIState.Ready);
        ReadyToShow(uiData);
    }

    public void AlreadyHideUI()
    {
        ChangeUIState(EUIState.AlreadyHide);
        this.AlreadyHide();
    }

	protected void ChangeUIState(EUIState eUIState)
	{
		m_eUIState = eUIState;
	}

	protected virtual void OnDoAwake() {}
	protected virtual void PrepareToShow(object uiData) {}
    protected virtual void ReadyToShow(object uiData) {}
    protected virtual void AlreadyHide() {}
}
