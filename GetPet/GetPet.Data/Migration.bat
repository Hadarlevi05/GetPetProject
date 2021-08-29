set arg1=%1
dotnet ef migrations add %arg1% --context GetPetDbContext