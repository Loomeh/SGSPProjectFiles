using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
	// Token: 0x020000AE RID: 174
	public class Restarter : MonoBehaviour
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00011184 File Offset: 0x0000F384
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == "Player")
			{
				SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
			}
		}
	}
}
