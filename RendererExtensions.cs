using UnityEngine;

public static class RendererExtensions
{
	private static Vector3[] vertsOf(Renderer rend)
    {
		GameObject obj = rend.gameObject;
		Bounds b = rend.bounds;
		Vector3[] verts = new Vector3[3];
		verts[0] = b.center;
		verts[1] = b.center + new Vector3(0, (obj.transform.localScale.y*20), 0) * 0.5f;
		verts[2] = b.center + new Vector3(0, (obj.transform.localScale.y * 35), 0) * 0.5f;
		/*verts[1] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
		verts[2] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
		verts[3] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
		verts[4] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
		verts[5] = b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f;
		verts[6] = b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f;
		verts[7] = b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f;
		verts[8] = b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f;*/

		return verts;
	}

	public static bool IsVisibleFrom(this Renderer renderer, Camera camera, int layerMasks)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		bool isInCameraView = GeometryUtility.TestPlanesAABB(planes, renderer.bounds);

		if (isInCameraView)
		{
			Vector3 cameraPos = camera.transform.position;
			RaycastHit hit;
			Vector3[] verts = vertsOf(renderer);
			foreach (Vector3 point in verts)
			{
				bool hitSomething = Physics.Linecast(cameraPos, point, out hit, layerMasks);
				//Debug.DrawLine(cameraPos, point, Color.red, 0.1f);
				if (!hitSomething)
				{
					return true;
				}
			}
		}
		return false;
	}
}
