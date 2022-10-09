using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	// Token: 0x020000AB RID: 171
	public class CameraFollow : MonoBehaviour
	{
		// Token: 0x06000347 RID: 839 RVA: 0x00010CC5 File Offset: 0x0000EEC5
		private void Awake()
		{
			this.m_Player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010CDC File Offset: 0x0000EEDC
		private bool CheckXMargin()
		{
			return Mathf.Abs(base.transform.position.x - this.m_Player.position.x) > this.xMargin;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010D0C File Offset: 0x0000EF0C
		private bool CheckYMargin()
		{
			return Mathf.Abs(base.transform.position.y - this.m_Player.position.y) > this.yMargin;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010D3C File Offset: 0x0000EF3C
		private void Update()
		{
			this.TrackPlayer();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010D44 File Offset: 0x0000EF44
		private void TrackPlayer()
		{
			float num = base.transform.position.x;
			float num2 = base.transform.position.y;
			if (this.CheckXMargin())
			{
				num = Mathf.Lerp(base.transform.position.x, this.m_Player.position.x, this.xSmooth * Time.deltaTime);
			}
			if (this.CheckYMargin())
			{
				num2 = Mathf.Lerp(base.transform.position.y, this.m_Player.position.y, this.ySmooth * Time.deltaTime);
			}
			num = Mathf.Clamp(num, this.minXAndY.x, this.maxXAndY.x);
			num2 = Mathf.Clamp(num2, this.minXAndY.y, this.maxXAndY.y);
			base.transform.position = new Vector3(num, num2, base.transform.position.z);
		}

		// Token: 0x040003BC RID: 956
		public float xMargin = 1f;

		// Token: 0x040003BD RID: 957
		public float yMargin = 1f;

		// Token: 0x040003BE RID: 958
		public float xSmooth = 8f;

		// Token: 0x040003BF RID: 959
		public float ySmooth = 8f;

		// Token: 0x040003C0 RID: 960
		public Vector2 maxXAndY;

		// Token: 0x040003C1 RID: 961
		public Vector2 minXAndY;

		// Token: 0x040003C2 RID: 962
		private Transform m_Player;
	}
}
