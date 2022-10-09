using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000097 RID: 151
	[RequireComponent(typeof(ParticleSystem))]
	public class JetParticleEffect : MonoBehaviour
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		private void Start()
		{
			this.m_Jet = this.FindAeroplaneParent();
			this.m_System = base.GetComponent<ParticleSystem>();
			this.m_OriginalLifetime = this.m_System.main.startLifetime.constant;
			this.m_OriginalStartSize = this.m_System.main.startSize.constant;
			this.m_OriginalStartColor = this.m_System.main.startColor.color;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E02C File Offset: 0x0000C22C
		private void Update()
		{
			ParticleSystem.MainModule main = this.m_System.main;
			main.startLifetime = Mathf.Lerp(0f, this.m_OriginalLifetime, this.m_Jet.Throttle);
			main.startSize = Mathf.Lerp(this.m_OriginalStartSize * 0.3f, this.m_OriginalStartSize, this.m_Jet.Throttle);
			main.startColor = Color.Lerp(this.minColour, this.m_OriginalStartColor, this.m_Jet.Throttle);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		private AeroplaneController FindAeroplaneParent()
		{
			Transform transform = base.transform;
			while (transform != null)
			{
				AeroplaneController component = transform.GetComponent<AeroplaneController>();
				if (!(component == null))
				{
					return component;
				}
				transform = transform.parent;
			}
			throw new Exception(" AeroplaneContoller not found in object hierarchy");
		}

		// Token: 0x04000304 RID: 772
		public Color minColour;

		// Token: 0x04000305 RID: 773
		private AeroplaneController m_Jet;

		// Token: 0x04000306 RID: 774
		private ParticleSystem m_System;

		// Token: 0x04000307 RID: 775
		private float m_OriginalStartSize;

		// Token: 0x04000308 RID: 776
		private float m_OriginalLifetime;

		// Token: 0x04000309 RID: 777
		private Color m_OriginalStartColor;
	}
}
