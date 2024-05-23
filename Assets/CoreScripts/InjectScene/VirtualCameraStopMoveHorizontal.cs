
using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class VirtualCameraStopMoveHorizontal : CinemachineExtension
{
	public float xStatic = 10;

	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase vcam,
		CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
	{
		if (stage == CinemachineCore.Stage.Body)
		{
			var pos = state.RawPosition;
			pos.x = xStatic;
			state.RawPosition = pos;
		}
	}
}
