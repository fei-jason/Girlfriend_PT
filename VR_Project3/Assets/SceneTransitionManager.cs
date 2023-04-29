using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public Slider slider;
    private bool check = false;
    
    public void Update() {
        if (slider.value > 0.6 && check == false) {
            check = true;
            Debug.Log("update");
            GoToScene(1);
        }
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        //Launch new scene
        SceneManager.LoadScene(sceneIndex);
    }
}
