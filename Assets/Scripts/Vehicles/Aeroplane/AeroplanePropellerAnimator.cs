using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000094 RID: 148
	public class AeroplanePropellerAnimator : MonoBehaviour
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000DC24 File Offset: 0x0000BE24
		private void Awake()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			this.m_PropellorModelRenderer = this.m_PropellorModel.GetComponent<Renderer>();
			this.m_PropellorBlurRenderer = this.m_PropellorBlur.GetComponent<Renderer>();
			this.m_PropellorBlur.parent = this.m_PropellorModel;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000DC70 File Offset: 0x0000BE70
		private void Update()
		{
			this.m_PropellorModel.Rotate(0f, this.m_MaxRpm * this.m_Plane.Throttle * Time.deltaTime * 60f, 0f);
			int num = 0;
			if (this.m_Plane.Throttle > this.m_ThrottleBlurStart)
			{
				num = Mathf.FloorToInt(Mathf.InverseLerp(this.m_ThrottleBlurStart, this.m_ThrottleBlurEnd, this.m_Plane.Throttle) * (float)(this.m_PropellorBlurTextures.Length - 1));
			}
			if (num != this.m_PropellorBlurState)
			{
				this.m_PropellorBlurState = num;
				if (this.m_PropellorBlurState == 0)
				{
					this.m_PropellorModelRenderer.enabled = true;
					this.m_PropellorBlurRenderer.enabled = false;
					return;
				}
				this.m_PropellorModelRenderer.enabled = false;
				this.m_PropellorBlurRenderer.enabled = true;
				this.m_PropellorBlurRenderer.material.mainTexture = this.m_PropellorBlurTextures[this.m_PropellorBlurState];
			}
		}

		// Token: 0x040002F0 RID: 752
		[SerializeField]
		private Transform m_PropellorModel;

		// Token: 0x040002F1 RID: 753
		[SerializeField]
		private Transform m_PropellorBlur;

		// Token: 0x040002F2 RID: 754
		[SerializeField]
		private Texture2D[] m_PropellorBlurTextures;

		// Token: 0x040002F3 RID: 755
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ThrottleBlurStart = 0.25f;

		// Token: 0x040002F4 RID: 756
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ThrottleBlurEnd = 0.5f;

		// Token: 0x040002F5 RID: 757
		[SerializeField]
		private float m_MaxRpm = 2000f;

		// Token: 0x040002F6 RID: 758
		private AeroplaneController m_Plane;

		// Token: 0x040002F7 RID: 759
		private int m_PropellorBlurState = -1;

		// Token: 0x040002F8 RID: 760
		private const float k_RpmToDps = 60f;

		// Token: 0x040002F9 RID: 761
		private Renderer m_PropellorModelRenderer;

		// Token: 0x040002FA RID: 762
		private Renderer m_PropellorBlurRenderer;
	}
}
