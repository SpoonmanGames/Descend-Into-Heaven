using UnityEngine;
using System.Collections;

public class ShaderModificatorController : MonoBehaviour {

    public Renderer RenderCamera;
    public Material TargetMaterialRender;
	
	void Awake () {
        RenderCamera.material = TargetMaterialRender;
	}
}
