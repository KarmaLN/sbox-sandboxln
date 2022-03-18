using Sandbox;
using System;

[Library( "vehicle_dodge_challenger", Title = "Dodge Challenger", Spawnable = true )]
public class vehicle_dodge_challenger : CarEntity
{
	public override string ModelPath => "models/vehicles/dodge_challenger_2015/dodge_challenger_2015.vmdl";


	public override Vector3 SeatPosition => base.SeatPosition + Vector3.Left * 18 + Vector3.Down * 5 + Vector3.Forward * -10;

	public override Vector3 FrontAxlePosition => base.FrontAxlePosition + Vector3.Forward * 28f + Vector3.Down * 0;
	public override Vector3 RearAxlePosition => base.RearAxlePosition + Vector3.Forward * -20f + Vector3.Down * 0;
}


