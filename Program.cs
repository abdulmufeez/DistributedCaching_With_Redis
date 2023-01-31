var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding redis service
builder.Services.AddStackExchangeRedisCache(options =>
{
    // connecting with redis through connectionString
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
    // this will be usefull when several app using same redis, this will make seperate container of redis for every app
    options.InstanceName = "RedisPractice_";
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
