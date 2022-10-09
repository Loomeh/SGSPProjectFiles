using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200006B RID: 107
	public class WaypointCircuit : MonoBehaviour
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008B39 File Offset: 0x00006D39
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00008B41 File Offset: 0x00006D41
		public float Length { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008B4A File Offset: 0x00006D4A
		public Transform[] Waypoints
		{
			get
			{
				return this.waypointList.items;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008B57 File Offset: 0x00006D57
		private void Awake()
		{
			if (this.Waypoints.Length > 1)
			{
				this.CachePositionsAndDistances();
			}
			this.numPoints = this.Waypoints.Length;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008B78 File Offset: 0x00006D78
		public WaypointCircuit.RoutePoint GetRoutePoint(float dist)
		{
			Vector3 routePosition = this.GetRoutePosition(dist);
			return new WaypointCircuit.RoutePoint(routePosition, (this.GetRoutePosition(dist + 0.1f) - routePosition).normalized);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public Vector3 GetRoutePosition(float dist)
		{
			int num = 0;
			if (this.Length == 0f)
			{
				this.Length = this.distances[this.distances.Length - 1];
			}
			dist = Mathf.Repeat(dist, this.Length);
			while (this.distances[num] < dist)
			{
				num++;
			}
			this.p1n = (num - 1 + this.numPoints) % this.numPoints;
			this.p2n = num;
			this.i = Mathf.InverseLerp(this.distances[this.p1n], this.distances[this.p2n], dist);
			if (this.smoothRoute)
			{
				this.p0n = (num - 2 + this.numPoints) % this.numPoints;
				this.p3n = (num + 1) % this.numPoints;
				this.p2n %= this.numPoints;
				this.P0 = this.points[this.p0n];
				this.P1 = this.points[this.p1n];
				this.P2 = this.points[this.p2n];
				this.P3 = this.points[this.p3n];
				return this.CatmullRom(this.P0, this.P1, this.P2, this.P3, this.i);
			}
			this.p1n = (num - 1 + this.numPoints) % this.numPoints;
			this.p2n = num;
			return Vector3.Lerp(this.points[this.p1n], this.points[this.p2n], this.i);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008D58 File Offset: 0x00006F58
		private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float i)
		{
			return 0.5f * (2f * p1 + (-p0 + p2) * i + (2f * p0 - 5f * p1 + 4f * p2 - p3) * i * i + (-p0 + 3f * p1 - 3f * p2 + p3) * i * i * i);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008E20 File Offset: 0x00007020
		private void CachePositionsAndDistances()
		{
			this.points = new Vector3[this.Waypoints.Length + 1];
			this.distances = new float[this.Waypoints.Length + 1];
			float num = 0f;
			for (int i = 0; i < this.points.Length; i++)
			{
				Transform transform = this.Waypoints[i % this.Waypoints.Length];
				Transform transform2 = this.Waypoints[(i + 1) % this.Waypoints.Length];
				if (transform != null && transform2 != null)
				{
					Vector3 position = transform.position;
					Vector3 position2 = transform2.position;
					this.points[i] = this.Waypoints[i % this.Waypoints.Length].position;
					this.distances[i] = num;
					num += (position - position2).magnitude;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008EFE File Offset: 0x000070FE
		private void OnDrawGizmos()
		{
			this.DrawGizmos(false);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008F07 File Offset: 0x00007107
		private void OnDrawGizmosSelected()
		{
			this.DrawGizmos(true);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008F10 File Offset: 0x00007110
		private void DrawGizmos(bool selected)
		{
			this.waypointList.circuit = this;
			if (this.Waypoints.Length > 1)
			{
				this.numPoints = this.Waypoints.Length;
				this.CachePositionsAndDistances();
				this.Length = this.distances[this.distances.Length - 1];
				Gizmos.color = (selected ? Color.yellow : new Color(1f, 1f, 0f, 0.5f));
				Vector3 from = this.Waypoints[0].position;
				if (this.smoothRoute)
				{
					for (float num = 0f; num < this.Length; num += this.Length / this.editorVisualisationSubsteps)
					{
						Vector3 routePosition = this.GetRoutePosition(num + 1f);
						Gizmos.DrawLine(from, routePosition);
						from = routePosition;
					}
					Gizmos.DrawLine(from, this.Waypoints[0].position);
					return;
				}
				for (int i = 0; i < this.Waypoints.Length; i++)
				{
					Vector3 position = this.Waypoints[(i + 1) % this.Waypoints.Length].position;
					Gizmos.DrawLine(from, position);
					from = position;
				}
			}
		}

		// Token: 0x040001D1 RID: 465
		public WaypointCircuit.WaypointList waypointList = new WaypointCircuit.WaypointList();

		// Token: 0x040001D2 RID: 466
		[SerializeField]
		private bool smoothRoute = true;

		// Token: 0x040001D3 RID: 467
		private int numPoints;

		// Token: 0x040001D4 RID: 468
		private Vector3[] points;

		// Token: 0x040001D5 RID: 469
		private float[] distances;

		// Token: 0x040001D6 RID: 470
		public float editorVisualisationSubsteps = 100f;

		// Token: 0x040001D8 RID: 472
		private int p0n;

		// Token: 0x040001D9 RID: 473
		private int p1n;

		// Token: 0x040001DA RID: 474
		private int p2n;

		// Token: 0x040001DB RID: 475
		private int p3n;

		// Token: 0x040001DC RID: 476
		private float i;

		// Token: 0x040001DD RID: 477
		private Vector3 P0;

		// Token: 0x040001DE RID: 478
		private Vector3 P1;

		// Token: 0x040001DF RID: 479
		private Vector3 P2;

		// Token: 0x040001E0 RID: 480
		private Vector3 P3;

		// Token: 0x020000D9 RID: 217
		[Serializable]
		public class WaypointList
		{
			// Token: 0x04000466 RID: 1126
			public WaypointCircuit circuit;

			// Token: 0x04000467 RID: 1127
			public Transform[] items = new Transform[0];
		}

		// Token: 0x020000DA RID: 218
		public struct RoutePoint
		{
			// Token: 0x06000412 RID: 1042 RVA: 0x00013022 File Offset: 0x00011222
			public RoutePoint(Vector3 position, Vector3 direction)
			{
				this.position = position;
				this.direction = direction;
			}

			// Token: 0x04000468 RID: 1128
			public Vector3 position;

			// Token: 0x04000469 RID: 1129
			public Vector3 direction;
		}
	}
}
