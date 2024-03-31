using Microsoft.AspNetCore.Authentication.Negotiate;
using StackOverflowTagsAPI.Services;
using StackOverflowTagsAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = options.DefaultPolicy;
});

// Dodawanie serwisów
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<ITagService, TagService>();
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddHostedService<StartupService>();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
