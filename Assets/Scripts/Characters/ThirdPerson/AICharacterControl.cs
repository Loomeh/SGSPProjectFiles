using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	// Token: 0x0200009B RID: 155
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class AICharacterControl : MonoBehaviour
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000E3F9 File Offset: 0x0000C5F9
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000E401 File Offset: 0x0000C601
		public NavMeshAgent agent { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000E40A File Offset: 0x0000C60A
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000E412 File Offset: 0x0000C612
		public ThirdPersonCharacter character { get; private set; }

		// Token: 0x060002EC RID: 748 RVA: 0x0000E41B File Offset: 0x0000C61B
		private void Start()
		{
			this.agent = base.GetComponentInChildren<NavMeshAgent>();
			this.character = base.GetComponent<ThirdPersonCharacter>();
			this.agent.updateRotation = false;
			this.agent.updatePosition = true;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E450 File Offset: 0x0000C650
		private void Update()
		{
			if (this.target != null)
			{
				this.agent.SetDestination(this.target.position);
			}
			if (this.agent.remainingDistance > this.agent.stoppingDistance)
			{
				this.character.Move(this.agent.desiredVelocity, false, false);
				return;
			}
			this.character.Move(Vector3.zero, false, false);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		public void SetTarget(Transform target)
		{
			this.target = target;
		}

		// Token: 0x0400031D RID: 797
		public Transform target;
	}
}
