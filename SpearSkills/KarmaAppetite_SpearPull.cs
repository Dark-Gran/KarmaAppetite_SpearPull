using System;
using Partiality.Modloader;

public class KarmaAppetite_SpearPull : PartialityMod
{

    public KarmaAppetite_SpearPull()
    {
        instance = this;
        this.ModID = "KarmaAppetite_SpearPull";
        this.Version = "0.1";
        this.author = "DarkGran";
    }

    public static KarmaAppetite_SpearPull instance;

    public override void OnEnable()
    {
        base.OnEnable();
        patch_Player.Patch();
        patch_Spear.Patch();
    }

}