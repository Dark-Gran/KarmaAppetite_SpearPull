using System;

public class patch_Player
{
    public static void Patch()
    {
		
		On.Player.CanIPickThisUp += Player_CanIPickThisUp;
	}

	private static bool Player_CanIPickThisUp(On.Player.orig_CanIPickThisUp orig, Player self, PhysicalObject obj)
	{
		if (self.Grabability(obj) == Player.ObjectGrabability.CantGrab)
		{
			return false;
		}
		if (obj is Spear)
		{

			if ((obj as Spear).mode == Weapon.Mode.OnBack)
			{
				return false;
			}
			if (((obj as Spear).mode == Weapon.Mode.Free || (obj as Spear).mode == Weapon.Mode.StuckInCreature || (obj as Spear).mode == Weapon.Mode.StuckInWall) && self.CanPutSpearToBack)
			{
				return true;
			}
		}
		int num = (int)self.Grabability(obj);
		if (num == 2)
		{
			for (int i = 0; i < 2; i++)
			{
				if (self.grasps[i] != null && self.Grabability(self.grasps[i].grabbed) > Player.ObjectGrabability.OneHand)
				{
					return false;
				}
			}
		}
		if (obj is Weapon)
		{
			if ((obj as Weapon).mode == Weapon.Mode.Thrown)
			{
				return false;
			}
			if ((obj as Weapon).forbiddenToPlayer > 0)
			{
				return false;
			}
		}
		int num2 = 0;
		for (int j = 0; j < 2; j++)
		{
			if (self.grasps[j] != null)
			{
				if (self.grasps[j].grabbed == obj)
				{
					return false;
				}
				if (self.Grabability(self.grasps[j].grabbed) > Player.ObjectGrabability.OneHand)
				{
					num2++;
				}
			}
		}
		return num2 != 2 && (num2 <= 0 || num <= 2);
	}

}