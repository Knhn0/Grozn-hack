var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//build
var app = builder.Build();

//swagger
app.UseSwagger();
app.UseSwaggerUI();

//start
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();