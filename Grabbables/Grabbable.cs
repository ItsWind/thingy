using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : MonoBehaviour
{
    public XRDirectInteractor InteractorHolding { get; set; } = null;
    public XRGrabInteractable GrabObj { get; set; } = null;
    public SpriteRenderer TriggerSpriteRenderer { get; private set; }

    private void setSpriteRotationToPlayer()
    {
        Camera plyCam = XRObjectsGet.XRPlayer.Camera;

        Vector3 plyPos = plyCam.transform.position;
        Vector3 thisPos = TriggerSpriteRenderer.transform.position;

        Vector3 lookPos = plyPos - thisPos;

        TriggerSpriteRenderer.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    public void SetSprite(Sprite spr)
    {
        if (spr == null)
        {
            TriggerSpriteRenderer.enabled = false;
            TriggerSpriteRenderer.sprite = null;
            return;
        }

        TriggerSpriteRenderer.enabled = true;
        TriggerSpriteRenderer.sprite = spr;

        setSpriteRotationToPlayer();
    }

    public virtual void OnActivate() { }

    public virtual void DoAwake() { }
    private void Awake()
    {
        GrabObj = GetComponent<XRGrabInteractable>();
        GrabObj.retainTransformParent = false;

        GameObject obj = new GameObject("TriggerSpriteRendererObj", typeof(SpriteRenderer));
        obj.transform.SetParent(transform, false);
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        TriggerSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        SetSprite(null);

        DoAwake();
    }

    private void OnEnable()
    {
        GrabObj.selectEntered.AddListener((SelectEnterEventArgs args) =>
        {
            InteractorHolding = args.interactorObject as XRDirectInteractor;
            if (!Backpack.Instance.BackpackObjs.Contains(GrabObj.transform))
                transform.SetParent(XRObjectsGet.XRPlayer.transform, true);
        });
        GrabObj.selectExited.AddListener((SelectExitEventArgs args) =>
        {
            InteractorHolding = null;
            if (!Backpack.Instance.BackpackObjs.Contains(GrabObj.transform))
                transform.SetParent(SceneLoadManager.CurrentLevelAllObjectsTransform, true);
        });
    }

    public void TryDelete()
    {
        if (InteractorHolding != null && InteractorHolding.isPerformingManualInteraction)
            InteractorHolding.EndManualInteraction();
        
        Destroy(gameObject);
    }
}
