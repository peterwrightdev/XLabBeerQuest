using LeedsBeerQuest.Repositories;
using LeedsBeerQuest.Services;
using LeedsBeerQuest.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReviewRepository>(sp => new ReviewRepository(builder.Configuration.GetValue<string>("DataSourceFilePath")));
builder.Services.AddScoped<IValidateReviews, ReviewValidator>();
builder.Services.AddScoped<IReviewService, ReviewService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
