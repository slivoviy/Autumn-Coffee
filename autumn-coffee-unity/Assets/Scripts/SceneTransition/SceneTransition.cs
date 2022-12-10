using UnityEngine;
using Ruinum.Core;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SceneTransition : BaseSingleton<SceneTransition>, IExecute
{
    [SerializeField] private TMP_Text LoadingPercentage;

    public GameObject SwithDayPanel;

    private static bool shouldPlayOpeningAnimation = false;

    private Animator animator;
    private AsyncOperation loadingSceneOperation;


    private void Start()
    {
        GameManager.Singleton.AddExecuteObject(this);
        animator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            animator.SetTrigger("sceneOpening");

            shouldPlayOpeningAnimation = false;
        }
    }

    public void SwitchToScene(string sceneName = "Gameplay_Core")
    {

        Singleton.animator.SetTrigger("sceneClosing");

        SceneManager.LoadScene(sceneName);

        // Singleton.loadingSceneOperation.allowSceneActivation = false;
    }

    public void Execute()
    {   
        if (loadingSceneOperation != null)
        {
            LoadingPercentage.text = "Loading... " + Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
        }
    }

    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }

    public void DayChange()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(SwithDayPanel.GetComponent<Image>().DOFade(1,7));
        SwithDayPanel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().DOFade(1,5);
        TimerManager.Singleton.StartTimer(7, () => SwitchToScene());
    }

    private void OnDestroy()
    {
        GameManager.Singleton.RemoveExecuteObject(this);
    }
}
