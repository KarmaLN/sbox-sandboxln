using Sandbox;

[Library("weapon_pistol_m1911", Title = "M1911", Spawnable = true)]
partial class M1911 : Weapon
{
	public override string ViewModelPath => "models/weapons/m1911/v_m1911.vmdl";

	public override float PrimaryRate => 15.0f;
	public override float SecondaryRate => 1.0f;

	public TimeSince TimeSinceDischarge { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel("models/weapons/m1911/w_m1911.vmdl");
	}

	public override bool CanPrimaryAttack()
	{
		return base.CanPrimaryAttack() && Input.Pressed(InputButton.Attack1);
	}

	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;
		TimeSinceSecondaryAttack = 0;

		(Owner as AnimEntity)?.SetAnimParameter("b_attack", true);

		ShootEffects();
		PlaySound("berettam92-1");
		ShootBullet(0.05f, 1.5f, 9.0f, 3.0f);
	}

	private void Discharge()
	{
		if (TimeSinceDischarge < 0.5f)
			return;

		TimeSinceDischarge = 0;

		var muzzle = GetAttachment("muzzle") ?? default;
		var pos = muzzle.Position;
		var rot = muzzle.Rotation;

		ShootEffects();
		PlaySound("berettam92-1");
		ShootBullet(pos, rot.Forward, 0.05f, 1.5f, 9.0f, 3.0f);

		ApplyAbsoluteImpulse(rot.Backward * 200.0f);
	}

	protected override void OnPhysicsCollision(CollisionEventData eventData)
	{
		if (eventData.Speed > 500.0f)
		{
			Discharge();
		}
	}

	public override void SimulateAnimator(PawnAnimator anim)
	{
		anim.SetAnimParameter("holdtype", 1);
		anim.SetAnimParameter("aim_body_weight", 1.0f);
		anim.SetAnimParameter("holdtype_handedness", 0);
	}
}
