using UnityEngine;
using UnityEngine.EventSystems;

public class InfoCanvasController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;
    private bool inDetailView = false;
    private bool userIsPlacingSomething = false;

    void Start()
    {
        GameManager.Instance.UserPlacingShuttle += OnUserPlacingShuttle;
        GameManager.Instance.UserPlacingStructure += OnUserPlacingStructure;
    }
    
    private void OnUserPlacingStructure(bool placingStructure)
    {
        if (placingStructure)
        {
            if (inDetailView)
            {
                ToggleInfoCanvasView();
            }
            userIsPlacingSomething = true;
        }
        else
        {
            userIsPlacingSomething = false;
        }
    }

    private void OnUserPlacingShuttle(bool placingShuttle)
    {
        if (placingShuttle)
        {
            if (inDetailView)
            {
                ToggleInfoCanvasView();
            }
            userIsPlacingSomething = true;
        }
        else
        {
            userIsPlacingSomething = false;
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (userIsPlacingSomething == false)
        {
            ToggleInfoCanvasView();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (userIsPlacingSomething == false)
        {
            ToggleInfoCanvasView();   
        }
    }
}
