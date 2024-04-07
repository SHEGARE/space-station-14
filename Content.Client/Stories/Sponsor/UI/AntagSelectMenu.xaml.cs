﻿using Content.Client.UserInterface.Controls;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Stories.Sponsor.UI;

[GenerateTypedNameReferences]
public sealed partial class AntagSelectMenu : FancyWindow
{
    private AntagSelectBoundUserInterface Owner { get; set; }
    public AntagSelectMenu(AntagSelectBoundUserInterface owner)
    {
        RobustXamlLoader.Load(this);

        Owner = owner;

        AntagSelectButton.OnItemSelected += args =>
        {
            var metadata = AntagSelectButton.GetItemMetadata(args.Id);
            if (metadata != null && metadata is string cast)
            {
                Owner.AntagSelected(cast);
            }
        };

        Request.OnPressed += (_) => Owner.PickAntag(Owner.CurrentAntag);
    }
    public void UpdateAntagSelect(HashSet<string> antags, string currentAntag)
    {
        AntagSelectButton.Clear();

        if (antags.Count == 0)
        {
            var name = currentAntag;
            if (Loc.TryGetString($"antag-{currentAntag}", out var locName))
            {
                name = locName;
            }
            AntagSelectButton.AddItem(name);
            AntagSelectButton.SetItemMetadata(AntagSelectButton.ItemCount - 1, currentAntag);
        }
        else
        {
            foreach (var antag in antags)
            {
                var name = antag;
                if (Loc.TryGetString($"antag-{antag}", out var locName))
                {
                    name = locName;
                }
                AntagSelectButton.AddItem(name);
                AntagSelectButton.SetItemMetadata(AntagSelectButton.ItemCount - 1, antag);
                if (antag == currentAntag)
                {
                    AntagSelectButton.Select(AntagSelectButton.ItemCount - 1);
                }
            }
        }
    }
    public override void Close()
    {
        base.Close();
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}

