using System;
using UnityEngine;

public class patch_Spear
{
	public static void Patch()
	{
		On.Spear.ChangeMode += Spear_ChangeMode;
	}

	private static void Spear_ChangeMode(On.Spear.orig_ChangeMode orig, Spear self, Weapon.Mode newMode)
	{
		if (self.mode == Weapon.Mode.StuckInWall && newMode != Weapon.Mode.StuckInWall)
		{
			if (self.abstractSpear.stuckInWallCycles >= 0)
			{
				for (int i = -1; i < 3; i++)
				{
					self.room.GetTile(self.stuckInWall.Value + new Vector2(20f * (float)i, 0f)).horizontalBeam = false;
				}
			}
			else
			{
				for (int j = -1; j < 3; j++)
				{
					self.room.GetTile(self.stuckInWall.Value + new Vector2(0f, 20f * (float)j)).verticalBeam = false;
				}
			}
			self.stuckInWall = null;
			self.abstractSpear.stuckInWallCycles = 0;
			self.addPoles = false;
		}
		orig.Invoke(self, newMode);
	}

}