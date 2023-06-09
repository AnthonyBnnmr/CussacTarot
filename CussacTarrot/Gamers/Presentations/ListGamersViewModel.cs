﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using CussacTarot.Gamers.Domains.Messages;
using CussacTarot.Models;
using CussacTarot.Core.Repositories;
using CussacTarot.Gamers.Domains;

namespace CussacTarot.Gamers.Presentations;

public class ListGamersViewModel : ObservableRecipient
{
    private IRepository<int, Gamer> _GamersRepository;

    private IRelayCommand<GamerViewModel> _RemoveCommand;
    public IRelayCommand<GamerViewModel> RemoveCommand => _RemoveCommand ??= new RelayCommand<GamerViewModel>((gamer) =>
    {
        _GamersRepository.Remove(gamer.ToModel());
        _Gamers.Remove(gamer);
    }, e =>
    {
        return e != null;
    });

    private IRelayCommand<GamerViewModel> _CreateCommand;
    public IRelayCommand<GamerViewModel> CreateCommand => _CreateCommand ??= new RelayCommand<GamerViewModel>((gamer) =>
    {
        if (gamer == null)
        {
            gamer = new GamerViewModel(null, CreateCommand, RemoveCommand);
        }
        Messenger.Send(new CreateOrUpdateGamerMessage(gamer));
    });

    private readonly ObservableCollection<GamerViewModel> _Gamers;

    public ObservableCollection<GamerViewModel> Gamers => _Gamers;
    
    public ListGamersViewModel()
    {
    }

    public ListGamersViewModel(IRepository<int, Gamer> gamersRepository)
    {
        _GamersRepository = gamersRepository ?? throw new ArgumentNullException(nameof(gamersRepository));
        Messenger.Register(this, (MessageHandler<object, FinishEditableGamerMessage>)((recipient, message) =>
        {
            InitListGamers();
        }));
        _Gamers = new();
        InitListGamers();
    }
    
    private void InitListGamers()
    {
        IEnumerable<int> gamerChecked = _Gamers.Where(e => e.Checked).Select(e => e.Id).ToList();
        _Gamers.Clear();
        foreach (GamerViewModel gamerViewModel in _GamersRepository.GetAll().Select(g => new GamerViewModel(g, CreateCommand, RemoveCommand)))
        {
            if (gamerChecked.Contains(gamerViewModel.Id))
            {
                gamerViewModel.Checked = true;
            }

            _Gamers.Add(gamerViewModel);
        }
    }
}

