using Sandbox;
using System;

[Library( "vehicle_dodge_viper", Title = "Dodge Viper", Spawnable = true )]
public partial class vehicle_dodge_viper : CarEntity
{
	public override string ModelPath => "models/vehicles/dodge_viper/dodge_viper.vmdl";

	public override Vector3 SeatPosition => base.SeatPosition + Vector3.Left * 20 + Vector3.Down * 10 + Vector3.Forward * -30;

	public override Vector3 FrontAxlePosition => base.FrontAxlePosition + Vector3.Forward * 24f + Vector3.Down * 0;
	public override Vector3 RearAxlePosition => base.RearAxlePosition + Vector3.Forward * -16f + Vector3.Down * 0;

}
