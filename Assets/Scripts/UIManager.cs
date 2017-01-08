using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static UIManager uimanager
	{
		get
		{
			return s_UIManager;
		}
	}

	public Camera UICamera
	{
		get
		{
			return m_UICamera;
		}
	}

	//[SerializeField] GameObject m_TestItemBag;
	[SerializeField] Camera m_UICamera;
	
	static UIManager s_UIManager = null;

	Dictionary<EWndID, UIWindow> m_AllUIWnd = new Dictionary<EWndID, UIWindow>();
	List<EWndID> m_CacheUIID = new List<EWndID>();
	List<EWndID> m_ShowUIID = new List<EWndID>();

	void Awake()
	{
		s_UIManager = this;
		DontDestroyOnLoad( this );
		//m_TestItemBag.SetActive(true);
	}

	public static Camera GetUICamera()
	{
		if(uimanager!=null)
		{
			return uimanager.m_UICamera;
		}
		else
		{
			return null;
		}
	}

	public static void RegisterUI(UIWindow uiWnd)
	{
		uimanager.RegisterWnd( uiWnd );
	}

	public static void ShowUISync(EWndID showID, object exData)
	{
		uimanager.StartCoroutine( uimanager.ShowWndAsync( showID, exData, null ) );
	}

	public static void ShowUISync(EWndID showID, object exData, EWndID hideID)
	{
		uimanager.StartCoroutine( uimanager.ShowWndAsync( showID, exData, new EWndID[] { hideID } ) );
	}

	public static void ShowUISync(EWndID showID, object exData, EWndID[] arHideID)
	{
		uimanager.StartCoroutine( uimanager.ShowWndAsync( showID, exData, arHideID ) );
	}

	public static IEnumerator ShowUIAsync(EWndID showID, object exData)
	{
		IEnumerator itor = uimanager.ShowWndAsync( showID, exData, null );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static IEnumerator ShowUIAsync(EWndID showID, object exData, EWndID hideID)
	{
		IEnumerator itor = uimanager.ShowWndAsync( showID, exData, new EWndID[]{ hideID } );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static IEnumerator ShowUIAsync(EWndID showID, object exData, EWndID[] arHideID)
	{
		IEnumerator itor = uimanager.ShowWndAsync( showID, exData, arHideID );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static void HideUI(EWndID hideID)
	{
		uimanager.HideWndSync( new EWndID[] { hideID } );
	}

	public static void HideUI(EWndID[] arHideID)
	{
		uimanager.HideWndSync( arHideID );
	}

	public static IEnumerator PrepareUIAsync(EWndID showID)
	{
		IEnumerator itor = uimanager.PrepareWndAsync( showID );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static IEnumerator SwitchUIAsync(EWndID showID, object exData)
	{
		IEnumerator itor = uimanager.SwitchWndAsync( showID, exData, null );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static IEnumerator SwitchUIAsync(EWndID showID, object exData, EWndID hideID)
	{
		IEnumerator itor = uimanager.SwitchWndAsync( showID, exData, new EWndID[]{ hideID } );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static IEnumerator SwitchUIAsync(EWndID showID, object exData, EWndID[] arHideID)
	{
		IEnumerator itor = uimanager.SwitchWndAsync( showID, exData, arHideID );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	public static void DestroyAllUI()
	{
		uimanager.DestroyAllWndSync();
	}

	void DestroyAllWndSync()
	{
		foreach ( EWndID hideID in m_ShowUIID )
		{
			UIWindow hideWnd = GetUIWnd( hideID );
			if ( hideWnd != null )
			{
				hideWnd.gameObject.SetActive( false );
                hideWnd.AlreadyHideUI();
			}
		}
		m_ShowUIID.Clear();

		foreach ( KeyValuePair<EWndID, UIWindow> kvp in m_AllUIWnd )
		{
			if ( kvp.Value != null && kvp.Value.gameObject )
			{
				GameObject.Destroy( kvp.Value.gameObject );
			}
		}
		m_CacheUIID.Clear();
		m_AllUIWnd.Clear();
	}

	void RegisterWnd(UIWindow uiWnd)
	{
		if ( uiWnd != null && uiWnd.WndID != EWndID.None )
		{
			AudioListener[] szAudioListener = uiWnd.GetComponentsInChildren<AudioListener>( true );
			for ( int i = 0; i < szAudioListener.Length; ++i )
			{
				AudioListener curAudioListener = szAudioListener[i];
				GameObject.Destroy( curAudioListener );

				Debug.LogError( "UI shouldn't have an audio listener on it: " + uiWnd.WndID );
			}

			//canvas setting
			Canvas uiCanvas = uiWnd.UICanvas;
			CanvasScaler uiScaler = uiWnd.UIScaler;
			if ( uiCanvas != null && uiScaler != null )
			{
				uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
				uiCanvas.worldCamera = m_UICamera;

				uiScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				uiScaler.referenceResolution = new Vector2( 1024, 512 );
				uiScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
			}
			else
			{
				Debug.LogError( "UI should have a canvas and a canvas scaler on it: " + uiWnd.WndID );
			}

			//fufeng todo: font setting
			//GlobalFunc.SetFont( uiWnd.gameObject );

			if ( m_AllUIWnd.ContainsKey( uiWnd.WndID ) )
			{
				Debug.LogError( "UI repeatedly registered: " + uiWnd.WndID );
				m_AllUIWnd[uiWnd.WndID] = uiWnd;
			}
			else
			{
				m_AllUIWnd.Add( uiWnd.WndID, uiWnd );
			}
		}
	}

	IEnumerator ShowWndAsync(EWndID showID, object exData, EWndID[] arHideID)
	{
		IEnumerator itor = PrepareWndAsync( showID );
		while( itor.MoveNext() )
		{
			yield return null;
		}
		
		itor = SwitchWndAsync( showID, exData, arHideID );
		while ( itor.MoveNext() )
		{
			yield return null;
		}
	}

	IEnumerator PrepareWndAsync(EWndID uiID )
	{
		if ( uiID != EWndID.None )
        {
			UIWindow uiWnd = GetUIWnd( uiID );
			if ( uiWnd == null )
            {
				if ( !IsUICaching( uiID ) )
                {
					m_CacheUIID.Add(uiID);

                    AsyncOperation asyncOp = SceneManager.LoadSceneAsync(uiID.ToString());
                    if (asyncOp != null)
                    {
                        while (!asyncOp.isDone)
                        {
                            yield return null;
                        }
                    }

                    this.m_CacheUIID.Remove(uiID);
					uiWnd = GetUIWnd( uiID );
					if ( uiWnd != null )
                    {
						uiWnd.gameObject.SetActive( false );
					}
					else
					{
						Debug.LogError( "UI prepare failed: " + uiID );
					}
				}
			}
		}
	}

	IEnumerator SwitchWndAsync(EWndID targetID, object uiData, EWndID[] arOriginID)
	{
	    if (!IsUICaching(targetID))
	    {
            HideWndSync(arOriginID);

            if (targetID != EWndID.None && !IsUIShowing(targetID))
            {
                UIWindow targetWnd = GetUIWnd(targetID);
                if (targetWnd != null)
                {
                    m_ShowUIID.Add(targetID);

                    targetWnd.PrepareShowUI(uiData);
                    targetWnd.gameObject.SetActive(true);
                    targetWnd.ReadyShowUI(uiData);

                    //fufeng todo: check state to show loading or communicating...

                    while (targetWnd.UIState != EUIState.Ready)
                    {
                        yield return null;
                    }
                }
                else
                {
                    Debug.LogError("UI should be prepared: " + targetID);
                }
            }
	    }
	}

	void HideWndSync(EWndID[] arHideID)
	{
		if ( arHideID != null )
		{
			for ( int i = 0; i < arHideID.Length; ++i )
			{
				EWndID hideID = arHideID[i];

				if ( hideID != EWndID.None && IsUIShowing( hideID ) )
				{
					m_ShowUIID.Remove( hideID );

					UIWindow hideWnd = GetUIWnd( hideID );
					if ( hideWnd != null )
					{
						hideWnd.gameObject.SetActive( false );
                        hideWnd.AlreadyHideUI();

						//fufeng todo: dynamic destroy ui wnd
					}
				}
			}
		}
	}

	public bool IsUICaching(EWndID uiID)
	{
		if ( uiID != EWndID.None && m_CacheUIID.Contains( uiID ) )
		{
			return true;
		}

		return false;
	}

	public bool IsUIShowing(EWndID uiID)
	{
		if ( uiID != EWndID.None && m_ShowUIID.Contains( uiID ) )
		{
			return true;
		}

		return false;
	}

	public UIWindow GetUIWnd(EWndID uiID)
	{
		if ( uiID != EWndID.None && m_AllUIWnd.ContainsKey( uiID ) )
		{
			return m_AllUIWnd[uiID];
		}

		return null;
	}

	public List<EWndID> ShowUIIDs
	{
		get
		{
			return m_ShowUIID;
		}
	}
}
