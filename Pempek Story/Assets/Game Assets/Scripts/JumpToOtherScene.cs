using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class JumpToOtherScene : MonoBehaviour {

	[SerializeField]
	List<KeyCode> keysJumpScene;

	[SerializeField]
	List<string> keysSceneName;

	[SerializeField]
	bool isHasTransitionSound;

	[SerializeField]
	bool isFlashScreen;

	void Start () {
		if (isFlashScreen) {
			StartCoroutine (goToSceneDelay ("loginGooglePlayScene"));
		}
	}

	void Update(){

		for (int i = 0; i < keysJumpScene.Count; i++) {
			if (Input.GetKeyDown (keysJumpScene [i])) {
				#region specialfor fairy tail //just for main menu
				if (keysJumpScene [i] == KeyCode.Return) {
					if (isHasTransitionSound)
						//soundManager.effectSoundPlay (8);
					StartCoroutine (goToSceneDelay ("modechoosen"));
				} else {
					goToScene (keysSceneName [i]);
				}
				#endregion
			}
		}
			
		if (Input.GetKeyDown ("joystick button 11")) {
			if (isHasTransitionSound)
				//soundManager.effectSoundPlay (8);
			StartCoroutine (goToSceneDelay ("modechoosen"));
		}
	}

	IEnumerator goToSceneDelay(string scene){
		yield return new WaitForSeconds (4f);

		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(scene);
		#else
			Application.LoadLevel(scene);
		#endif
	}

    public static void quickGoToScene(string scene)
    {
		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(scene);
		#else
        	Application.LoadLevel(scene);
		#endif
	}

    public void goToScene(string scene)
	{
		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(scene);
		#else
        	Application.LoadLevel(scene);
		#endif
	}

    public void goToScene()
	{
		#if UNITY_5_5_OR_NEWER
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		#else
        	Application.LoadLevel(Application.loadedLevelName);
		#endif
	}

    public void goCloseApplication()
	{
		Application.Quit ();
	}
}