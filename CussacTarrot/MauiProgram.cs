﻿using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.DependencyInjection;
using CussacTarot.Core.Domains;
using CussacTarot.Core.Repositories;
using CussacTarot.Core.Services;
using CussacTarot.Gamers.Presentations;
using CussacTarot.GameSheets.Presentations;
using CussacTarot.Models;
using CussacTarot.Ranking.Presentations;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using ServiceStack.Data;
using System.Globalization;
using UraniumUI;

namespace CussacTarot;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {


        CultureInfo cultureFr = CultureInfo.GetCultureInfo("fr-FR");
        CultureInfo.DefaultThreadCurrentCulture = cultureFr;
        CultureInfo.DefaultThreadCurrentUICulture = cultureFr;
        Thread.CurrentThread.CurrentCulture = cultureFr;
        Thread.CurrentThread.CurrentUICulture = cultureFr;

        ServiceCollection services = new();
        services
            .AddSingleton(LaunchBddService.CreateBdd())
            .AddTransient<RankingService>()
            .AddTransient<ILaunchGameService, ChooseLaunchGameService>()
            .AddTransient<ILaunchGameService<Gamer>, LaunchFirstGameService>()
            .AddTransient<ILaunchGameService<ScoreByGamer>, LaunchSecondGameService>()
            .AddTransient<ILaunchGameService, ChooseLaunchGameService>()
            .AddTransient<IRepository<int, Gamer>>((s) => new SqlLiteRepository<int, Gamer>(s.GetRequiredService<IDbConnectionFactory>()))
            .AddTransient<IRepository<int, GameSheet>>((s) => new SqlLiteRepository<int, GameSheet>(s.GetRequiredService<IDbConnectionFactory>()))
            .AddTransient<GamerPageViewModel>()
            .AddTransient<GameSheetPageViewModel>()
            .AddTransient<RankingPageViewModel>()
            .AddTransient<ListGamersViewModel>()
            .AddTransient<RankingViewModel>()
            .AddTransient<EditGamerViewModel>()
            .AddTransient<ListGameSheetsViewModel>()
            .AddTransient<EditGameSheetViewModel>()
            .AddTransient<AppShellViewModel>();

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUI()            
            .ConfigureMauiHandlers(handlers =>
            {                
                handlers.AddInputKitHandlers();
            })
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFontAwesomeIconFonts();
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
