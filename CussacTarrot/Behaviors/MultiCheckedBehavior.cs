using CussacTarot.Gamers.Domains;
using CussacTarot.Gamers.Presentations;
using System.Collections;
using System.ComponentModel;

namespace CussacTarot.Behaviors;


public static class MultiCheckedBehavior
{
    public static readonly BindableProperty CheckedItemsProperty =
        BindableProperty.CreateAttached("CheckedItems", typeof(IList), typeof(MultiCheckedBehavior), null, propertyChanged: OnCheckedItemsChanged);

    public static IList GetCheckedItems(BindableObject view)
    {
        return (IList) view.GetValue(CheckedItemsProperty);
    }

    public static void SetCheckedItems(BindableObject view, IList value)
    {
        view.SetValue(CheckedItemsProperty, value);
    }

    private static bool _First = true;

    private static void OnCheckedItemsChanged(BindableObject view, object oldValue, object newValue)
    {
        ListGamersView? listGamers = view as ListGamersView;
        if (listGamers != null && listGamers.BindingContext is ListGamersViewModel viewModel)
        {
            IList checkedItems = GetCheckedItems(view);

            PropertyChangedEventHandler propertyChangedGame = (sender, args) =>
            {
                if (sender is GamerViewModel gamerViewModel)
                {
                    if (gamerViewModel.Checked && !checkedItems.Contains(gamerViewModel))
                    {
                        checkedItems.Add(gamerViewModel);
                    }
                    else if (!gamerViewModel.Checked && checkedItems.Contains(gamerViewModel))
                    {
                        checkedItems.Remove(gamerViewModel);
                    }
                }
            };

            if (_First)
            {
                checkedItems.Clear();
                foreach (GamerViewModel gamerViewModel in viewModel.Gamers)
                {
                    gamerViewModel.PropertyChanged += propertyChangedGame;
                }
                _First = false;
            }
            else
            {
                viewModel.Gamers.CollectionChanged += (sender, arg) =>
                {
                    if (arg.OldItems != null)
                    {
                        foreach (GamerViewModel oldGamer in arg.OldItems.OfType<GamerViewModel>())
                        {
                            oldGamer.PropertyChanged -= propertyChangedGame;
                        }
                    }

                    checkedItems.Clear();
                    foreach (GamerViewModel gamerViewModel in viewModel.Gamers)
                    {
                        if (gamerViewModel.Checked)
                        {
                            checkedItems.Add(gamerViewModel);
                        }

                        gamerViewModel.PropertyChanged += propertyChangedGame;
                    }
                };
            }
        };
    }
}

