using PiTung;
using PiTung.Components;
using System;
using UnityEngine;

public class OversizeButtons : Mod
{
    public override string Name => "Oversize Buttons";
    public override string PackageName => "me.jimmy.GiantButtons";
    public override string Author => "Iamsodarncool";
    public override Version ModVersion => new Version("1.0");


    public override void BeforePatch()
    {
        ComponentRegistry.CreateNew("LargePanelButton", "Large Panel Button", CreatePanelButtonOfSize(3, 2));
        ComponentRegistry.CreateNew("GiantPanelButton", "Giant Panel Button", CreatePanelButtonOfSize(3, 3));
        ComponentRegistry.CreateNew("EnormousPanelButton", "Enormous Panel Button", CreatePanelButtonOfSize(15, 2));
        
        // the world is not ready
        // ComponentRegistry.CreateNew("CollosalPanelButton", "Collosal Panel Button", CreatePanelButtonOfSize(51, 51));
        // ComponentRegistry.CreateNew("TitanicPanelButton", "Titanic Panel Button", CreatePanelButtonOfSize(401, 601));
    }

    private static CustomBuilder CreatePanelButtonOfSize(int x, int z)
    {
        return PrefabBuilder
            .Custom(() =>
            {
                var obj = UnityEngine.Object.Instantiate(References.Prefabs.PanelButton);
                obj.transform.GetChild(0).localScale = new Vector3(x - 0.3f, 0.2f, z - 0.3f); // this is the collider of the button that you have to click on to press it
                obj.transform.GetChild(1).localScale = new Vector3(x - 0.3f, 0.2f, z - 0.3f); // this is the actual button itself that moves when you interact with it
                obj.transform.GetChild(2).localScale = new Vector3(x, 0.33333f, z); // this is the base of the button, the white part right below the brown part
                obj.transform.GetChild(4).localScale = new Vector3(x - 0.35f, 0.833333f, z - 0.35f); // this is the back panel that the output sits on top of. Dimensions carefully chosen so that mounts can fit between them

                // if it is an even number wide, we have to shift everything in the component so that it still lines up with the grid
                if (x % 2 == 0)
                {
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).transform.localPosition += new Vector3(0.5f, 0, 0);
                    }
                }
                // ditto with height
                if (z % 2 == 0)
                {
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        obj.transform.GetChild(i).transform.localPosition += new Vector3(0, 0, 0.5f);
                    }
                }

                return obj;
            });
    }
}