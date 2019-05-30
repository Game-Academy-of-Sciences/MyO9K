using System;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Prediction
{
	// Token: 0x0200000E RID: 14
	public interface IPredictionManager9
	{
		// Token: 0x06000043 RID: 67
		PredictionOutput9 GetPrediction(PredictionInput9 input);

		// Token: 0x06000044 RID: 68
		PredictionOutput9 GetSimplePrediction(PredictionInput9 input);
	}
}
