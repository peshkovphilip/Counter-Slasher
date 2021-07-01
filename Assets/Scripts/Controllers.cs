using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers : IStarter, IDestroyer
{
    private readonly List<IStarter> controllerStarters;
    private readonly List<IDestroyer> controllerDestroyers;

    internal Controllers()
    {
        controllerStarters = new List<IStarter>();
        controllerDestroyers = new List<IDestroyer>();
    }

    internal Controllers Add(IController controller)
    {
        if (controller is IStarter controllerStarter)
        {
            controllerStarters.Add(controllerStarter);
        }
        if (controller is IDestroyer controllerDestroyer)
        {
            controllerDestroyers.Add(controllerDestroyer);
        }
        return this;
    }

    public void Starter()
    {
        foreach(IStarter starter in controllerStarters)
        {
            starter.Starter();
        }
    }

    public void Destroyer()
    {
        foreach (IDestroyer destroyer in controllerDestroyers)
        {
            destroyer.Destroyer();
        }
    }
}
