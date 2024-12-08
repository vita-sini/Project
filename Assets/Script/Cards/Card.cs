using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Card
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public abstract void Execute();
}

public class WindCard : Card
{
    private readonly Effects _effects;

    public WindCard(Effects effects)
    {
        Name = "Ветер";
        Description = "Сильный ветер может опрокинуть башню";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.TriggerWind();
    }
}

public class EarthquakeCard : Card
{
    private readonly Effects _effects;

    public EarthquakeCard(Effects effects)
    {
        Name = "Землетрясение";
        Description = "Земля дрожит!";
        _effects = effects;
    }

    public override void Execute()
    {
        _effects.TriggerEarthquake();
    }
}
