using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
	[System.Serializable]
	public class SceneField
	{
		[SerializeField]
		private Object sceneAsset = null;
		[SerializeField]
		private string sceneName = "";

		public string SceneName => sceneName; 

		public static implicit operator string(SceneField sceneField)
		{
			return sceneField.sceneName;
		}
	}
}
