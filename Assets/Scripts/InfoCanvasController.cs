using UnityEngine;
using UnityEngine.EventSystems;

public class InfoCanvasController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;
    private bool inDetailView = false;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleInfoCanvasView();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleInfoCanvasView();
    }
}
