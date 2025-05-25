var builder = WebApplication.CreateBuilder(args);

// Add Services here.

var app = builder.Build();

//Configure HTTP Pipeline.

app.Run();
