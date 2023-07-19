namespace Artifactan.Jobs;

public interface IArtifactJob
{

    public Task UploadArtifact();

}

public class ArtifactJob : IArtifactJob
{
    public Task UploadArtifact()
    {
        Console.WriteLine("Fire And Forget");
        return Task.CompletedTask;
    }
}