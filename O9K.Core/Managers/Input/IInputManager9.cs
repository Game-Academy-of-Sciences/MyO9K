using System;
using O9K.Core.Managers.Input.EventArgs;

namespace O9K.Core.Managers.Input
{
	// Token: 0x02000061 RID: 97
	public interface IInputManager9
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000309 RID: 777
		// (remove) Token: 0x0600030A RID: 778
		event EventHandler<KeyEventArgs> KeyDown;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600030B RID: 779
		// (remove) Token: 0x0600030C RID: 780
		event EventHandler<KeyEventArgs> KeyUp;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600030D RID: 781
		// (remove) Token: 0x0600030E RID: 782
		event EventHandler<MouseEventArgs> MouseKeyDown;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600030F RID: 783
		// (remove) Token: 0x06000310 RID: 784
		event EventHandler<MouseEventArgs> MouseKeyUp;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000311 RID: 785
		// (remove) Token: 0x06000312 RID: 786
		event EventHandler<MouseMoveEventArgs> MouseMove;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000313 RID: 787
		// (remove) Token: 0x06000314 RID: 788
		event EventHandler<MouseWheelEventArgs> MouseWheel;
	}
}
