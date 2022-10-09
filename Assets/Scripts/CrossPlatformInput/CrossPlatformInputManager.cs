using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput.PlatformSpecific;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x0200007B RID: 123
	public static class CrossPlatformInputManager
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000AB09 File Offset: 0x00008D09
		static CrossPlatformInputManager()
		{
			CrossPlatformInputManager.activeInput = CrossPlatformInputManager.s_HardwareInput;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AB29 File Offset: 0x00008D29
		public static void SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod activeInputMethod)
		{
			if (activeInputMethod == CrossPlatformInputManager.ActiveInputMethod.Hardware)
			{
				CrossPlatformInputManager.activeInput = CrossPlatformInputManager.s_HardwareInput;
				return;
			}
			if (activeInputMethod != CrossPlatformInputManager.ActiveInputMethod.Touch)
			{
				return;
			}
			CrossPlatformInputManager.activeInput = CrossPlatformInputManager.s_TouchInput;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AB48 File Offset: 0x00008D48
		public static bool AxisExists(string name)
		{
			return CrossPlatformInputManager.activeInput.AxisExists(name);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AB55 File Offset: 0x00008D55
		public static bool ButtonExists(string name)
		{
			return CrossPlatformInputManager.activeInput.ButtonExists(name);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AB62 File Offset: 0x00008D62
		public static void RegisterVirtualAxis(CrossPlatformInputManager.VirtualAxis axis)
		{
			CrossPlatformInputManager.activeInput.RegisterVirtualAxis(axis);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000AB6F File Offset: 0x00008D6F
		public static void RegisterVirtualButton(CrossPlatformInputManager.VirtualButton button)
		{
			CrossPlatformInputManager.activeInput.RegisterVirtualButton(button);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public static void UnRegisterVirtualAxis(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CrossPlatformInputManager.activeInput.UnRegisterVirtualAxis(name);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000AB97 File Offset: 0x00008D97
		public static void UnRegisterVirtualButton(string name)
		{
			CrossPlatformInputManager.activeInput.UnRegisterVirtualButton(name);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		public static CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
		{
			return CrossPlatformInputManager.activeInput.VirtualAxisReference(name);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		public static float GetAxis(string name)
		{
			return CrossPlatformInputManager.GetAxis(name, false);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000ABBA File Offset: 0x00008DBA
		public static float GetAxisRaw(string name)
		{
			return CrossPlatformInputManager.GetAxis(name, true);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000ABC3 File Offset: 0x00008DC3
		private static float GetAxis(string name, bool raw)
		{
			return CrossPlatformInputManager.activeInput.GetAxis(name, raw);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public static bool GetButton(string name)
		{
			return CrossPlatformInputManager.activeInput.GetButton(name);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000ABDE File Offset: 0x00008DDE
		public static bool GetButtonDown(string name)
		{
			return CrossPlatformInputManager.activeInput.GetButtonDown(name);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000ABEB File Offset: 0x00008DEB
		public static bool GetButtonUp(string name)
		{
			return CrossPlatformInputManager.activeInput.GetButtonUp(name);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		public static void SetButtonDown(string name)
		{
			CrossPlatformInputManager.activeInput.SetButtonDown(name);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AC05 File Offset: 0x00008E05
		public static void SetButtonUp(string name)
		{
			CrossPlatformInputManager.activeInput.SetButtonUp(name);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AC12 File Offset: 0x00008E12
		public static void SetAxisPositive(string name)
		{
			CrossPlatformInputManager.activeInput.SetAxisPositive(name);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AC1F File Offset: 0x00008E1F
		public static void SetAxisNegative(string name)
		{
			CrossPlatformInputManager.activeInput.SetAxisNegative(name);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public static void SetAxisZero(string name)
		{
			CrossPlatformInputManager.activeInput.SetAxisZero(name);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AC39 File Offset: 0x00008E39
		public static void SetAxis(string name, float value)
		{
			CrossPlatformInputManager.activeInput.SetAxis(name, value);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000AC47 File Offset: 0x00008E47
		public static Vector3 mousePosition
		{
			get
			{
				return CrossPlatformInputManager.activeInput.MousePosition();
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000AC53 File Offset: 0x00008E53
		public static void SetVirtualMousePositionX(float f)
		{
			CrossPlatformInputManager.activeInput.SetVirtualMousePositionX(f);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000AC60 File Offset: 0x00008E60
		public static void SetVirtualMousePositionY(float f)
		{
			CrossPlatformInputManager.activeInput.SetVirtualMousePositionY(f);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AC6D File Offset: 0x00008E6D
		public static void SetVirtualMousePositionZ(float f)
		{
			CrossPlatformInputManager.activeInput.SetVirtualMousePositionZ(f);
		}

		// Token: 0x04000228 RID: 552
		private static VirtualInput activeInput;

		// Token: 0x04000229 RID: 553
		private static VirtualInput s_TouchInput = new MobileInput();

		// Token: 0x0400022A RID: 554
		private static VirtualInput s_HardwareInput = new StandaloneInput();

		// Token: 0x020000E0 RID: 224
		public enum ActiveInputMethod
		{
			// Token: 0x0400047D RID: 1149
			Hardware,
			// Token: 0x0400047E RID: 1150
			Touch
		}

		// Token: 0x020000E1 RID: 225
		public class VirtualAxis
		{
			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001347C File Offset: 0x0001167C
			// (set) Token: 0x06000426 RID: 1062 RVA: 0x00013484 File Offset: 0x00011684
			public string name { get; private set; }

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001348D File Offset: 0x0001168D
			// (set) Token: 0x06000428 RID: 1064 RVA: 0x00013495 File Offset: 0x00011695
			public bool matchWithInputManager { get; private set; }

			// Token: 0x06000429 RID: 1065 RVA: 0x0001349E File Offset: 0x0001169E
			public VirtualAxis(string name) : this(name, true)
			{
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x000134A8 File Offset: 0x000116A8
			public VirtualAxis(string name, bool matchToInputSettings)
			{
				this.name = name;
				this.matchWithInputManager = matchToInputSettings;
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x000134BE File Offset: 0x000116BE
			public void Remove()
			{
				CrossPlatformInputManager.UnRegisterVirtualAxis(this.name);
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x000134CB File Offset: 0x000116CB
			public void Update(float value)
			{
				this.m_Value = value;
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x000134D4 File Offset: 0x000116D4
			public float GetValue
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x000134D4 File Offset: 0x000116D4
			public float GetValueRaw
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x04000480 RID: 1152
			private float m_Value;
		}

		// Token: 0x020000E2 RID: 226
		public class VirtualButton
		{
			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x000134DC File Offset: 0x000116DC
			// (set) Token: 0x06000430 RID: 1072 RVA: 0x000134E4 File Offset: 0x000116E4
			public string name { get; private set; }

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x000134ED File Offset: 0x000116ED
			// (set) Token: 0x06000432 RID: 1074 RVA: 0x000134F5 File Offset: 0x000116F5
			public bool matchWithInputManager { get; private set; }

			// Token: 0x06000433 RID: 1075 RVA: 0x000134FE File Offset: 0x000116FE
			public VirtualButton(string name) : this(name, true)
			{
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00013508 File Offset: 0x00011708
			public VirtualButton(string name, bool matchToInputSettings)
			{
				this.name = name;
				this.matchWithInputManager = matchToInputSettings;
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0001352E File Offset: 0x0001172E
			public void Pressed()
			{
				if (this.m_Pressed)
				{
					return;
				}
				this.m_Pressed = true;
				this.m_LastPressedFrame = Time.frameCount;
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0001354B File Offset: 0x0001174B
			public void Released()
			{
				this.m_Pressed = false;
				this.m_ReleasedFrame = Time.frameCount;
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x0001355F File Offset: 0x0001175F
			public void Remove()
			{
				CrossPlatformInputManager.UnRegisterVirtualButton(this.name);
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001356C File Offset: 0x0001176C
			public bool GetButton
			{
				get
				{
					return this.m_Pressed;
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x00013574 File Offset: 0x00011774
			public bool GetButtonDown
			{
				get
				{
					return this.m_LastPressedFrame - Time.frameCount == -1;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x00013585 File Offset: 0x00011785
			public bool GetButtonUp
			{
				get
				{
					return this.m_ReleasedFrame == Time.frameCount - 1;
				}
			}

			// Token: 0x04000484 RID: 1156
			private int m_LastPressedFrame = -5;

			// Token: 0x04000485 RID: 1157
			private int m_ReleasedFrame = -5;

			// Token: 0x04000486 RID: 1158
			private bool m_Pressed;
		}
	}
}
