using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000059 RID: 89
	public class AutoMobileShaderSwitch : MonoBehaviour
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000255D File Offset: 0x0000075D
		private void OnEnable()
		{
		}

		// Token: 0x0400017A RID: 378
		[SerializeField]
		private AutoMobileShaderSwitch.ReplacementList m_ReplacementList;

		// Token: 0x020000C9 RID: 201
		[Serializable]
		public class ReplacementDefinition
		{
			// Token: 0x0400042F RID: 1071
			public Shader original;

			// Token: 0x04000430 RID: 1072
			public Shader replacement;
		}

		// Token: 0x020000CA RID: 202
		[Serializable]
		public class ReplacementList
		{
			// Token: 0x04000431 RID: 1073
			public AutoMobileShaderSwitch.ReplacementDefinition[] items = new AutoMobileShaderSwitch.ReplacementDefinition[0];
		}
	}
}
