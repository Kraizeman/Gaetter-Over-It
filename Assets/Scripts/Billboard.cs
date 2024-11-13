using UnityEngine;
using UnityEngine.Rendering;

public class Billboard : MonoBehaviour
{
    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnCamPreRender;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnCamPreRender;
    }

    private void OnCamPreRender(ScriptableRenderContext context, Camera cam)
    {
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        transform.rotation = Quaternion.LookRotation(camForward, Vector3.up);
    }
}
