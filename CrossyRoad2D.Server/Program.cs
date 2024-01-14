using CrossyRoad2D.Hubs;
using CrossyRoad2D.Server.HubServiceProxies;
using CrossyRoad2D.Server.Services.Hub;
using CrossyRoad2D.Server.Services.Hub.Interfaces;
using CrossyRoad2D.Server.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR().AddJsonProtocol();

builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.Decorate<IChatService, ChatServiceProxy>();

builder.Services.AddSingleton<IConnectionService, ConnectionService>();
builder.Services.Decorate<IConnectionService, ConnectionServiceProxy>();

builder.Services.AddSingleton<IPlayerService, PlayerService>();
builder.Services.Decorate<IPlayerService, PlayerServiceProxy>();

var app = builder.Build();

app.MapHub<GameHub>("gamehub");

app.Run();
