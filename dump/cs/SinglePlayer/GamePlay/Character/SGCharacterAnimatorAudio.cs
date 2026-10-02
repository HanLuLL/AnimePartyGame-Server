using FairyGUI;
using UnityEngine;

namespace SinglePlayer.GamePlay.Character;

public class SGCharacterAnimatorAudio : MonoBehaviour
{
	public void PlayAnimationSFX(int audioEventID)
	{
		Stage.inst.PlayOneShotSound(audioEventID);
	}
}
