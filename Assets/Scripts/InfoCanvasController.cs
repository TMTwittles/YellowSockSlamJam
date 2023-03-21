using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvasController : MonoBehaviour
{
    [SerializeField] private Button expandButton;
    [SerializeField] private Animator animator;
    private bool inDetailView = false;
    
    public void OnEnable()
    {
        expandButton.onClick.AddListener(ToggleInfoCanvasView);
    }

    public void ToggleInfoCanvasView()
    {
        if (inDetailView && animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationKeys.InfoCanvasDetailViewState))
        {
            animator.SetTrigger(AnimationKeys.InfoCanvasMinimiseTrigger);
            inDetailView = !inDetailView;
        }
        else if (inDetailView == false && animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationKeys.InfoCanvasMinimalViewState))
        {
            animator.SetTrigger(AnimationKeys.InfoCanvasExpandTrigger);
            inDetailView = !inDetailView;
        }
    }
}
