using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000017 RID: 23
public class EventSystemChecker : MonoBehaviour
{
	// Token: 0x0600004B RID: 75 RVA: 0x000039C6 File Offset: 0x00001BC6
	private void Awake()
	{
		if (!Object.FindObjectOfType<EventSystem>())
		{
			GameObject gameObject = new GameObject("EventSystem");
			gameObject.AddComponent<EventSystem>();
			gameObject.AddComponent<StandaloneInputModule>().forceModuleActive = true;
		}
	}
}
