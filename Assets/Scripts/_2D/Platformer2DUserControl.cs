using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	// Token: 0x020000AC RID: 172
	[RequireComponent(typeof(PlatformerCharacter2D))]
	public class Platformer2DUserControl : MonoBehaviour
	{
		// Token: 0x0600034D RID: 845 RVA: 0x00010E77 File Offset: 0x0000F077
		private void Awake()
		{
			this.m_Character = base.GetComponent<PlatformerCharacter2D>();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00010E85 File Offset: 0x0000F085
		private void Update()
		{
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		private void FixedUpdate()
		{
			bool key = Input.GetKey(KeyCode.LeftControl);
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			this.m_Character.Move(axis, key, this.m_Jump);
			this.m_Jump = false;
		}

		// Token: 0x040003C3 RID: 963
		private PlatformerCharacter2D m_Character;

		// Token: 0x040003C4 RID: 964
		private bool m_Jump;
	}
}
