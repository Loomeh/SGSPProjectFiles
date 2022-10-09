using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000066 RID: 102
	public class SimpleActivatorMenu : MonoBehaviour
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00008524 File Offset: 0x00006724
		private void OnEnable()
		{
			this.m_CurrentActiveObject = 0;
			this.camSwitchButton.text = this.objects[this.m_CurrentActiveObject].name;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000854C File Offset: 0x0000674C
		public void NextCamera()
		{
			int num = (this.m_CurrentActiveObject + 1 >= this.objects.Length) ? 0 : (this.m_CurrentActiveObject + 1);
			for (int i = 0; i < this.objects.Length; i++)
			{
				this.objects[i].SetActive(i == num);
			}
			this.m_CurrentActiveObject = num;
			this.camSwitchButton.text = this.objects[this.m_CurrentActiveObject].name;
		}

		// Token: 0x040001BC RID: 444
		public Text camSwitchButton;

		// Token: 0x040001BD RID: 445
		public GameObject[] objects;

		// Token: 0x040001BE RID: 446
		private int m_CurrentActiveObject;
	}
}
