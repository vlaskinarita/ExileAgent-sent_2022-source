using System;

namespace PoEv2
{
	public enum GameProcessState
	{
		NotRunning,
		Running,
		Patching,
		FixingConfig,
		Login,
		CharacterSelect,
		Online,
		Loading
	}
}
