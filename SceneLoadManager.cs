using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance;

    public static Transform CurrentLevelAllObjectsTransform;
    public static List<Transform> CurrentLevelPlayerSpawnPointTransforms;

    public static bool Loading = false;

    public Material DefaultSkyboxMaterial;
    public Material MenuSkyboxMaterial;

    public static int CurrentSceneIndex { get; private set; } = 3;

    bool gameStart;

    private void TryLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            CurrentSceneIndex = data.sceneIndex;
        }
    }

    private void Awake()
    {
        if (!gameStart)
        {
            TryLoad();
            Instance = this;

            SceneManager.LoadSceneAsync(CurrentSceneIndex, LoadSceneMode.Additive);

            gameStart = true;
        }
    }

    private void Start()
    {
        if (CurrentSceneIndex != 3)
            AllBaseObjectsGet.Instance.SetActive(false);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(Load(sceneIndex));
    }

    private AsyncOperation UnloadCurrentScene()
    {
        return SceneManager.UnloadSceneAsync(CurrentSceneIndex);
    }

    IEnumerator Load(int sceneIndex)
    {
        if (!Loading)
        {
            // Set Loading variable
            Loading = true;

            // Force hold objects in hands as scene transitions
            XRObjectsGet.LeftController.ForceHoldIfObjectHeld();
            XRObjectsGet.RightController.ForceHoldIfObjectHeld();

            // Fade screen out and wait
            ScreenFade.Instance.FadeOut();
            yield return new WaitForSeconds(ScreenFade.Instance.fadeDuration);

            // If not going to base, disable base objects
            if (sceneIndex != 3)
                AllBaseObjectsGet.Instance.SetActive(false);

            // Unload and load async
            AsyncOperation asyncUnload = UnloadCurrentScene();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            CurrentSceneIndex = sceneIndex;

            // Wait for both operations to be done
            while (!asyncUnload.isDone || !asyncLoad.isDone)
            {
                yield return null;
            }

            // If we have entered the base, enable base objects
            if (sceneIndex == 3)
                AllBaseObjectsGet.Instance.SetActive(true);

            // Get spawn point of player in scene
            Transform spawnPoint = CurrentLevelPlayerSpawnPointTransforms[RandomNumGen.Instance.Next(0, CurrentLevelPlayerSpawnPointTransforms.Count)];
            XRObjectsGet.XRPlayer.transform.position = spawnPoint.position;
            XRObjectsGet.XRPlayer.transform.GetChild(0).position = spawnPoint.position;

            // Wait some additional time
            yield return new WaitForSeconds(ScreenFade.Instance.fadeDuration);
            Loading = false;

            // Fade in the screen
            ScreenFade.Instance.FadeIn();
            yield return new WaitForSeconds(ScreenFade.Instance.fadeDuration);

            // Enable controller grip controls again
            XRObjectsGet.LeftController.DropForceHold();
            XRObjectsGet.RightController.DropForceHold();
        }
    }
}
