public enum EUIState
{
	Ready,			//ready to show

    Prepare,     //prepare for UI
	Loading,		//load resource
	Waiting,		//wait server msg
    AlreadyHide, //after hide ui
}

public enum EWndID
{
	None,						

	Login			=1,		//sorting layer: default		order layer: 0
    PveScene              =2,     //sorting layer: default        order layer: 0
}
