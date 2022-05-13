using UnityEngine;

namespace Es.InkPainter.Sample
{
	[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
	public class CollisionPainterEnemy : MonoBehaviour
	{
		[SerializeField]
		private Brush brush = null;

		[SerializeField]
		private int wait = 3;

		private int waitCount;

		private Material material;
	
		void Start()
        {
			material = GetComponent<Renderer>().material;

		}

		public void Awake()
		{
			GetComponent<MeshRenderer>().material.color = brush.Color;
		}

		public void FixedUpdate()
		{
			++waitCount;
		}

		public void OnCollisionStay(Collision collision)
		{
			if(waitCount < wait)
				return;
			waitCount = 0;

			//myCode
			//Calculate Brush Scale
			GameObject targetObject = collision.gameObject;
			Mesh mesh = targetObject.transform.GetComponent<MeshFilter>().mesh;
			Bounds bounds = mesh.bounds;
			Vector3 origin = bounds.size;
			//判定の大きさを変えたかったら、0.2fのところを調整。[SerializeField]にしたら
			float scale = Mathf.Sqrt((0.17f / origin.x) * (0.17f / origin.y));
			if (scale > 1)
            {
				brush.Scale = 1.0f;
			}
            else
            {
				brush.Scale = scale;
			}

			//brush.Color = from M5 via Bluetooth
			brush.Color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
			material.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
			GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

			foreach (var p in collision.contacts)
			{
				var canvas = p.otherCollider.GetComponent<InkCanvas>();
				if(canvas != null)
					canvas.Paint(brush, p.point);
			}
		}
	}
}