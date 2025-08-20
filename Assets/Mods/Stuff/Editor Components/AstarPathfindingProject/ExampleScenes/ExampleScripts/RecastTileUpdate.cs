using UnityEngine;

namespace Pathfinding {
	/// <summary>
	/// Updates the recast tile(s) it is in at start, needs RecastTileUpdateHandler.
	///
	/// If there is a collider attached to the same GameObject, the bounds
	/// of that collider will be used for updating, otherwise
	/// only the position of the object will be used.
	///
	/// Note: This class needs a RecastTileUpdateHandler somewhere in the scene.
	/// See the documentation for that class, it contains more information.
	///
	/// Note: This does not use navmesh cutting. If you only ever add
	/// obstacles, but never add any new walkable surfaces then you might
	/// want to use navmesh cutting instead. See navmeshcutting (view in online documentation for working links).
	///
	/// See: RecastTileUpdateHandler
	/// </summary>
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdate")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_recast_tile_update.php")]
	public class RecastTileUpdate : MonoBehaviour {
		public static event System.Action<Bounds> OnNeedUpdates;
		public bool startUpdate = true;
		void Start () {
			if (startUpdate == true)
			{

				ScheduleUpdate();
			} 
			print("recast update!" + transform.name);
		}

		public void NavUpdate(Collider collider)
        {
			

			if (collider != null)
			{
				if (OnNeedUpdates != null)
				{
					OnNeedUpdates(collider.bounds);
					//OnNeedUpdates(new Bounds(transform.position, Vector3.zero));
				}
			}
			else
			{
				if (OnNeedUpdates != null)
				{
					OnNeedUpdates(new Bounds(transform.position, Vector3.zero));
				}
			}
		}
		void OnDestroy () {
			if (startUpdate == true)
			{
				ScheduleUpdate();
			}
		}

		/// <summary>Schedule a tile update for all tiles that contain this object</summary>
		public void ScheduleUpdate () {
			var collider = GetComponent<Collider>();

			if (collider != null) {
				if (OnNeedUpdates != null) {
					OnNeedUpdates(collider.bounds);
				//OnNeedUpdates(new Bounds(transform.position, Vector3.zero));
				}
			} else {
				if (OnNeedUpdates != null) {
					OnNeedUpdates(new Bounds(transform.position, Vector3.zero));
				}
			}
		}
	}
}
